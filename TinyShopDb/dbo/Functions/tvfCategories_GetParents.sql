CREATE FUNCTION [dbo].[tvfCategories_GetParents]
(
	@Id INT,
	@IsProduct BIT
)
RETURNS @Parents TABLE
(
	Id INT,
	CategoryName NVARCHAR(100)
)
AS
BEGIN
	DECLARE @CurrentId INT
	SET @CurrentId = @Id

	IF @IsProduct = 1
		SET @CurrentId = (SELECT TOP(1) pc.CategoryId FROM ProductCategories pc WHERE @Id = pc.ProductId)

	WHILE (EXISTS(SELECT TOP(1) cs.CategoryId FROM CategorySubcategories cs WHERE @CurrentId = cs.SubcategoryId))
		BEGIN
			INSERT INTO @Parents(Id, CategoryName)
			SELECT c.Id, c.CategoryName
			FROM Categories c
			WHERE c.Id = @CurrentId

			SET @CurrentId = (SELECT TOP(1) cs.CategoryId FROM CategorySubcategories cs WHERE @CurrentId = cs.SubcategoryId)
		END
		-- Last insert that prevent loosing one parent 
		INSERT INTO @Parents(Id, CategoryName)
		SELECT c.Id, c.CategoryName
		FROM Categories c
		WHERE c.Id = @CurrentId
	RETURN
END
