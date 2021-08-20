CREATE FUNCTION [dbo].[tvfProducts_GetMetadata]
(
	@CategoryId INT = NULL,
	@MinPrice MONEY = NULL,
	@MaxPrice MONEY = NULL,
	@MinRating TINYINT = 0
)
RETURNS @returntable TABLE
(
	[FoundRecords] INT NULL,
	[MinPrice] MONEY NULL,
	[MaxPrice] MONEY NULL
)
AS
BEGIN
	DECLARE @TmpTable AS Table (Price MONEY, Rating TINYINT)

	INSERT @TmpTable
	SELECT p.Price
	,ISNULL(pr.Rating, 0) AS [Rating]
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
	WHERE @CategoryId IS NULL OR CategoryId = @CategoryId

	INSERT @returntable
	SELECT(
		SELECT COUNT(*)
		FROM @TmpTable
		WHERE 
				(@MinPrice IS NULL OR Price >= @MinPrice)
			AND (@MaxPrice IS NULL OR Price <= @MaxPrice)
			AND (ISNULL(Rating, 0) >= @MinRating)
		) AS [FoundRecords],
	MIN(Price) AS [MinPrice],
	MAX(Price) AS [MaxPrice]
	FROM @TmpTable
	RETURN
END
