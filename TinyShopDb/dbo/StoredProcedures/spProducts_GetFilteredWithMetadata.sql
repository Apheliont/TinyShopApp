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
	@CategoryId int = NULL,
	@RowsPerPage int,
	@PageNumber int,
	@OrderBy INT = 1,
	@SortOrder INT = 1,
	@MinPrice MONEY = NULL,
	@MaxPrice MONEY = NULL,
	@MinRating TINYINT = 0
AS
BEGIN
	SET NOCOUNT ON;

	IF @RowsPerPage < 1
		SET @RowsPerPage = 1
	IF @PageNumber < 1
		SET @PageNumber = 1

SELECT

		JSON_QUERY(
			(SELECT [FoundRecords], [MinPrice], [MaxPrice]
			 FROM [dbo].[tvfProducts_GetMetadata](@CategoryId, @MinPrice, @MaxPrice, @MinRating)
			 FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER)
		) AS [Metadata],

		JSON_QUERY(
					(SELECT
					 p.Id
					,p.ProductName
					,p.[Description]
					,p.Price 
					,ISNULL(pr.Rating, 0) AS [Rating]
					,
					(
						SELECT i.Id, i.Caption, i.UriSizeS, i.UriSizeM, i.UriSizeL
						FROM ProductImages pis
						LEFT JOIN Images AS i
			            ON i.Id = pis.ImageId
						WHERE pis.ProductId = p.Id
						FOR JSON PATH
					) AS Images
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
					WHERE
							(@CategoryId IS NULL OR CategoryId = @CategoryId)
						AND	(@MinPrice IS NULL OR p.Price >= @MinPrice)
						AND (@MaxPrice IS NULL OR p.Price <= @MaxPrice)
						AND (ISNULL(pr.Rating, 0) >= @MinRating)
					ORDER BY
						CASE WHEN @OrderBy = 3 AND @SortOrder = 2 THEN Rating END ,
						CASE WHEN @OrderBy = 3 AND @SortOrder = 1 THEN Rating END DESC,
						CASE WHEN @OrderBy = 2 AND @SortOrder = 2 THEN Price END ,
						CASE WHEN @OrderBy = 2 AND @SortOrder = 1 THEN Price END DESC,
						CASE WHEN @OrderBy = 1 AND @SortOrder = 2 THEN ProductName END ,
						CASE WHEN @OrderBy = 1 AND @SortOrder = 1 THEN ProductName END DESC
					OFFSET (@RowsPerPage * (@PageNumber - 1)) ROWS FETCH NEXT @RowsPerPage ROWS ONLY
					FOR JSON AUTO
					)) AS [Products]

		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
END
