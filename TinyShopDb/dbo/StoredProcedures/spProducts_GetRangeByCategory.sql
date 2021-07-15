CREATE PROCEDURE [dbo].[spProducts_GetRangeByCategory]
	@Id int,
	@From int = 0,
	@To int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT p.Id, p.ProductName, p.Description, p.Price
	FROM Products p
	WHERE Id = (SELECT ProductId FROM ProductCategories WHERE CategoryId = @Id)
	ORDER BY p.ProductName
	OFFSET @From ROWS FETCH NEXT @To ROWS ONLY
END
