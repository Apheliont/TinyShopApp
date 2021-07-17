CREATE PROCEDURE [dbo].[spProducts_GetRangeByCategory]
	@CategoryId int,
	@From int = 0,
	@To int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT p.Id, p.ProductName, p.Description, p.Price
	FROM Products p
	INNER JOIN (SELECT ProductId FROM ProductCategories WHERE CategoryId = @CategoryId) pc
	ON p.Id = pc.ProductId
	ORDER BY p.ProductName
	OFFSET @From ROWS FETCH NEXT @To ROWS ONLY
END
