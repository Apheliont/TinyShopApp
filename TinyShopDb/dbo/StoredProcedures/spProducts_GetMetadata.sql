CREATE PROCEDURE [dbo].[spProducts_GetMetadata]
	@CategoryId INT = NULL,
	@MinPrice MONEY = NULL,
	@MaxPrice MONEY = NULL,
	@MinRating TINYINT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT p.Price
	,ISNULL(pr.Rating, 0) AS [Rating]
	INTO #AllProductsTable
	FROM Products p
	INNER JOIN (
		SELECT ProductId
		FROM ProductCategories
		WHERE @CategoryId IS NULL OR CategoryId = @CategoryId) pc
	ON p.Id = pc.ProductId
	LEFT JOIN (
	SELECT ProductId
	,AVG(Rating) AS [Rating]
	FROM ProductRatings
	GROUP BY ProductId
	) pr
	ON pr.ProductId = p.Id

	SELECT
		(
		SELECT COUNT(*)
		FROM #AllProductsTable
		WHERE 
				(@MinPrice IS NULL OR Price >= @MinPrice)
			AND (@MaxPrice IS NULL OR Price <= @MaxPrice)
			AND (@MinRating IS NULL OR Rating >= @MinRating)
		) AS [FoundRecords],
		MIN(Price) AS [MinPrice],
		MAX(Price) AS [MaxPrice]
	FROM #AllProductsTable
END

		
