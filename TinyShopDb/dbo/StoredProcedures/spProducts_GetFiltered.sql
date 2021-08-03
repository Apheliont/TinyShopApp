/*
	@OrderBy is C# enum which has a representation in number:
	1 = ProductName
	2 = Price
	3 = Rating

	@SortOrder C# enum which has a representation in number:
	1 = DESC
	2 = ASC
*/

CREATE PROCEDURE [dbo].[spProducts_GetFiltered]
	@CategoryId int = NULL,
	@RowsPerPage int,
	@PageNumber int,
	@OrderBy INT = 1,
	@SortOrder INT = 1,
	@MinPrice MONEY = NULL,
	@MaxPrice MONEY = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF @RowsPerPage < 1
		SET @RowsPerPage = 1
	IF @PageNumber < 1
		SET @PageNumber = 1


	SELECT 
		 p.Id
		,p.ProductName
		,p.Description
		,p.Price

	FROM Products p
	INNER JOIN (
		SELECT ProductId
		FROM ProductCategories
		WHERE @CategoryId IS NULL OR CategoryId = @CategoryId) pc
	ON p.Id = pc.ProductId
	WHERE
			(@MinPrice IS NULL OR p.Price >= @MinPrice)
		AND (@MaxPrice IS NULL OR p.Price <= @MaxPrice)
	ORDER BY
		CASE WHEN @OrderBy = 2 AND @SortOrder = 2 THEN Price END ,
		CASE WHEN @OrderBy = 2 AND @SortOrder = 1 THEN Price END DESC,
		CASE WHEN @OrderBy = 1 AND @SortOrder = 2 THEN ProductName END ,
		CASE WHEN @OrderBy = 1 AND @SortOrder = 1 THEN ProductName END DESC
	OFFSET (@RowsPerPage * (@PageNumber - 1)) ROWS FETCH NEXT @RowsPerPage ROWS ONLY
END
