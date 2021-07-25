CREATE PROCEDURE [dbo].[spProducts_GetFilteredWithMetadata]
	@CategoryId int = NULL,
	@RowsPerPage int,
	@PageNumber int,
	@OrderBy VARCHAR(50) = 'ProductName',
	@OrderType VARCHAR(4) = 'DESC',
	@MinPrice MONEY = NULL,
	@MaxPrice MONEY = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT p.Id, p.ProductName, p.Description, p.Price, Count(*) OVER () AS FoundRecords
	FROM Products p
	INNER JOIN (
		SELECT ProductId
		FROM ProductCategories
		WHERE @CategoryId IS NULL OR CategoryId = @CategoryId
			) pc
	ON p.Id = pc.ProductId
	WHERE
			(@MinPrice IS NULL OR p.Price >= @MinPrice)
		AND (@MaxPrice IS NULL OR p.Price <= @MaxPrice)
	ORDER BY
		CASE WHEN @OrderBy = 'Price' AND @OrderType ='ASC' THEN Price END ,
		CASE WHEN @OrderBy = 'Price' AND @OrderType ='DESC' THEN Price END DESC,
		CASE WHEN @OrderBy = 'ProductName' AND @OrderType ='ASC' THEN ProductName END ,
		CASE WHEN @OrderBy = 'ProductName' AND @OrderType ='DESC' THEN ProductName END DESC
	OFFSET (@RowsPerPage * (@PageNumber - 1)) ROWS FETCH NEXT @RowsPerPage ROWS ONLY
END
