/*
	@OrderBy is C# enum which has a representation in number:
	1 = ProductName
	2 = Price
	3 = Rating

	@SortOrder C# enum which has a representation in number:
	1 = DESC
	2 = ASC
*/

CREATE PROCEDURE [dbo].[spProducts_GetFilteredWithMetadata]
	@CategoryId INT = NULL,
	@RowsPerPage INT,
	@PageNumber INT,
	@OrderBy INT = 1,
	@SortOrder INT = 1,
	@JsonFilterData NVARCHAR(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @DynamicFilter VARCHAR(MAX)
	DECLARE @OrderAndFetch VARCHAR(2000)
	DECLARE @sqlCommand NVARCHAR(2000)
	DECLARE @MetadataJson NVARCHAR(MAX)
	DECLARE @ProductsJson NVARCHAR(MAX)
	DECLARE @ProductDetailsMetadataSPName VARCHAR(100)
	DECLARE @ProductDetailsTableName VARCHAR(100)
	DECLARE @FoundRecords INT

	IF @RowsPerPage < 1
		SET @RowsPerPage = 1
	IF @PageNumber < 1
		SET @PageNumber = 1


	SET @OrderAndFetch = '
	ORDER BY
		CASE WHEN @OrderBy = 3 AND @SortOrder = 2 THEN Rating END ,
		CASE WHEN @OrderBy = 3 AND @SortOrder = 1 THEN Rating END DESC,
		CASE WHEN @OrderBy = 2 AND @SortOrder = 2 THEN Price END ,
		CASE WHEN @OrderBy = 2 AND @SortOrder = 1 THEN Price END DESC,
		CASE WHEN @OrderBy = 1 AND @SortOrder = 2 THEN ProductName END ,
		CASE WHEN @OrderBy = 1 AND @SortOrder = 1 THEN ProductName END DESC
	OFFSET (@RowsPerPage * (@PageNumber - 1)) ROWS FETCH NEXT @RowsPerPage ROWS ONLY'

	-- check if there is details info in json string format
	IF @JsonFilterData IS NOT NULL
	BEGIN
		IF (ISJSON(@JsonFilterData) > 0)
		BEGIN
			DECLARE @getjson CURSOR
			DECLARE @jvalue NVARCHAR(400)
			DECLARE @jkey NVARCHAR(200)
			DECLARE @jtype TINYINT

			SET @getjson = CURSOR FOR
			SELECT [key], [value], [type]
			FROM OPENJSON(@JsonFilterData)

			OPEN @getjson
			FETCH NEXT
			FROM @getjson INTO @jkey, @jvalue, @jtype
			WHILE @@FETCH_STATUS = 0
			BEGIN
				IF (LEN(@DynamicFilter) > 1)
					SET @DynamicFilter = CONCAT(@DynamicFilter, ' AND ')
				IF (@jtype = 1)
					SET @DynamicFilter = CONCAT(@DynamicFilter, @jkey, ' = ', @jvalue)
				IF (@jtype = 2)
					BEGIN
						IF LOWER(@jkey) = 'rating'
							SET @DynamicFilter = CONCAT(@DynamicFilter, @jkey, ' >= ', ISNULL(@jvalue, 0))
					END
				IF (@jtype = 3)
					SET @DynamicFilter = CONCAT(@DynamicFilter, @jkey, ' = ', CAST(@jvalue AS BIT))
				IF (@jtype = 4)
					SET @DynamicFilter = CONCAT(@DynamicFilter, @jkey, ' IN ', (SELECT REPLACE(REPLACE(REPLACE(@jvalue, '[', '('), ']', ')'), '"', '''')))
				IF (@jtype = 5)
					BEGIN
						IF (JSON_VALUE(@jvalue, '$.From') IS NOT NULL)
							BEGIN
								SET @DynamicFilter = CONCAT(@DynamicFilter, @jkey, ' >= ', JSON_VALUE(@jvalue, '$.From'))
								IF (JSON_VALUE(@jvalue, '$.To') IS NOT NULL)
									SET @DynamicFilter = CONCAT(@DynamicFilter, ' AND ', @jkey, ' <= ', JSON_VALUE(@jvalue, '$.To'))
							END
						ELSE IF (JSON_VALUE(@jvalue, '$.To') IS NOT NULL)
							SET @DynamicFilter = CONCAT(@DynamicFilter, @jkey, ' <= ', JSON_VALUE(@jvalue, '$.To'))
					END
				FETCH NEXT
				FROM @getjson INTO @jkey, @jvalue, @jtype
			END

			CLOSE @getjson
			DEALLOCATE @getjson
		END
-- There is not else statement. Be aware!
	END

		-- we also need to ensure that there is apropriate details for specific category id
	SET @ProductDetailsTableName = (
		SELECT cpn.ProductDetailsTableName
		FROM CategoryProductDetails cpn
		WHERE cpn.CategoryId = @CategoryId)

	SET @ProductDetailsMetadataSPName = (
		SELECT cpd.ProductDetailsMetadataSPName
		FROM CategoryProductDetails cpd
		WHERE cpd.CategoryId = @CategoryId)


-- Find all products available for specific category
	SELECT
	 p.Id
	,p.ProductName
	,p.[Description]
	,p.Price 
	,ISNULL(pr.Rating, 0) AS [Rating]
	INTO #AllProductsTable -- insert into temp table
	FROM Products p
	INNER JOIN ProductCategories pc
	ON p.Id = pc.ProductId
	LEFT JOIN (
		SELECT ProductId
		,AVG(Rating) AS [Rating]
		FROM ProductRatings
		GROUP BY ProductId
	) pr
	ON pr.ProductId = p.Id
	WHERE (@CategoryId IS NULL OR CategoryId = @CategoryId)


	SET @MetadataJson = (SELECT 
			  CAST(FLOOR(MIN(Price)) AS INT) AS 'Price.LowerBound'
			 ,CAST(CEILING(MAX(Price)) AS INT) AS 'Price.UpperBound'
			 ,'Rub' AS 'Price.Measurement'
			 ,COUNT(*) AS 'FoundRecords'
			 FROM #AllProductsTable
			 FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)

-- If we succesfully parsed JSON and formed proper dynamic WHERE clause
	IF (LEN(@DynamicFilter) > 0)
		BEGIN
			DECLARE @count INT
			-- Dynamically form statement with two outputs
			-- We need to get all found records as INT and Products array as JSON
			IF (@ProductDetailsTableName IS NOT NULL)
				BEGIN
					-- first query; fill in temp table
					SET @sqlCommand = 'SELECT apt.* INTO #DetailedProductsTable FROM #AllProductsTable apt LEFT JOIN '
						+ @ProductDetailsTableName
						+ ' pdt'
						+ ' ON apt.Id = pdt.ProductId WHERE'
						+ ' ' + @DynamicFilter			
				END
			ELSE
				BEGIN
					-- first query; fill in temp table
					SET @sqlCommand = 'SELECT apt.* INTO #DetailedProductsTable FROM #AllProductsTable apt '
						+ ' WHERE ' + @DynamicFilter
				END
			-- second query; getting count
			SET @sqlCommand = @sqlCommand + ' SELECT @count=COUNT(*) FROM #DetailedProductsTable'
			-- third query; getting product array as json
			+ ' SET @ProductsJson = (SELECT dpt.*
			, (
				SELECT i.Id, i.Caption, i.UriSizeS, i.UriSizeM, i.UriSizeL
				FROM ProductImages pis
				LEFT JOIN Images AS i
				ON i.Id = pis.ImageId
				WHERE pis.ProductId = dpt.Id
				FOR JSON PATH
			  ) AS Images
				FROM #DetailedProductsTable dpt'
			+ ' ' + @OrderAndFetch
			+ ' FOR JSON AUTO)'

			EXECUTE sp_executesql @sqlCommand,
			N'@count INT OUTPUT, @ProductsJson NVARCHAR(MAX) OUTPUT,
			@OrderBy INT, @SortOrder INT, @RowsPerPage INT, @PageNumber INT',
			@count=@count OUTPUT,
			@ProductsJson=@ProductsJson OUTPUT,
			@OrderBy=@OrderBy,
			@SortOrder=@SortOrder,
			@RowsPerPage=@RowsPerPage,
			@PageNumber=@PageNumber

			-- We need to modify 'FoundRecords' in Metadata part accordingly to accuared info
			SET @MetadataJson = JSON_MODIFY(@MetadataJson, '$.FoundRecords', @count)
		END
	ELSE
		BEGIN
			SET @sqlCommand = 'SET @ProductsJson = (SELECT apt.*
			, (
				SELECT i.Id, i.Caption, i.UriSizeS, i.UriSizeM, i.UriSizeL
				FROM ProductImages pis
				LEFT JOIN Images AS i
				ON i.Id = pis.ImageId
				WHERE pis.ProductId = apt.Id
				FOR JSON PATH
			  ) AS Images
			  FROM #AllProductsTable apt'
			+ ' ' + @OrderAndFetch
			+ ' FOR JSON AUTO)'
			EXECUTE sp_executesql @sqlCommand,
			N'@ProductsJson NVARCHAR(MAX) OUTPUT,
			@OrderBy INT, @SortOrder INT, @RowsPerPage INT, @PageNumber INT',
			@ProductsJson=@ProductsJson OUTPUT,
			@OrderBy=@OrderBy,
			@SortOrder=@SortOrder,
			@RowsPerPage=@RowsPerPage,
			@PageNumber=@PageNumber
		END


	IF @ProductDetailsMetadataSPName IS NOT NULL
		BEGIN
			DECLARE @JsonResult NVARCHAR(MAX)
			EXEC @ProductDetailsMetadataSPName @JsonResult = @JsonResult OUTPUT

			SELECT 
				JSON_QUERY(JSON_MODIFY(@MetadataJson, '$.Details', JSON_QUERY(@JsonResult))) AS [Metadata]
			   ,JSON_QUERY(@ProductsJson) AS [Products]
			    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		END
	ELSE
		BEGIN
			SELECT 
				JSON_QUERY(@MetadataJson) AS [Metadata]
			   ,JSON_QUERY(@ProductsJson) AS [Products]
			    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
		END

END