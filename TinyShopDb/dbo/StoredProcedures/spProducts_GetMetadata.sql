CREATE PROCEDURE [dbo].[spProducts_GetMetadata]
	@CategoryId INT = NULL,
	@MinPrice MONEY = NULL,
	@MaxPrice MONEY = NULL
AS
BEGIN
	SELECT p.Price
	INTO #AllProductsTable
	FROM Products p
	INNER JOIN (
		SELECT ProductId
		FROM ProductCategories
		WHERE @CategoryId IS NULL OR CategoryId = @CategoryId) pc
	ON p.Id = pc.ProductId


	SELECT
		(
		SELECT COUNT(*)
		FROM #AllProductsTable
		WHERE 
				(@MinPrice IS NULL OR Price >= @MinPrice)
			AND (@MaxPrice IS NULL OR Price <= @MaxPrice)
		) AS [FoundRecords],
		MIN(Price) AS [MinPrice],
		MAX(Price) AS [MaxPrice]
	FROM #AllProductsTable
END

		
