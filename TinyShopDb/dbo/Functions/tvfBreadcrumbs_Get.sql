CREATE FUNCTION [dbo].[tvfBreadcrumbs_Get]
(
	@Id INT,
	@IsProduct BIT
)
RETURNS @Parents TABLE
(
	Id INT NOT NULL IDENTITY(1,1),
	ItemId INT,
	ItemName NVARCHAR(100)
)
AS
BEGIN
	DECLARE @CurrentId INT
	SET @CurrentId = @Id

	IF @IsProduct = 1
		BEGIN
			SET @CurrentId = (SELECT TOP(1) pc.CategoryId FROM ProductCategories pc WHERE @Id = pc.ProductId)

			INSERT INTO @Parents(ItemId, ItemName)
			SELECT @Id, p.ProductName
			FROM Products p
			WHERE p.Id = @Id
		END

	WHILE (EXISTS(SELECT TOP(1) cs.CategoryId FROM CategorySubcategories cs WHERE @CurrentId = cs.SubcategoryId))
		BEGIN
			INSERT INTO @Parents(ItemId, ItemName)
			SELECT c.Id, c.CategoryName
			FROM Categories c
			WHERE c.Id = @CurrentId

			SET @CurrentId = (SELECT TOP(1) cs.CategoryId FROM CategorySubcategories cs WHERE @CurrentId = cs.SubcategoryId)
		END
		-- Last insert that prevent loosing one item 
		INSERT INTO @Parents(ItemId, ItemName)
		SELECT c.Id, c.CategoryName
		FROM Categories c
		WHERE c.Id = @CurrentId
	RETURN
END
