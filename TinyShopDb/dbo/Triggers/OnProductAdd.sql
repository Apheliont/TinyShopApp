CREATE TRIGGER [OnProductAdd]
	ON [dbo].[ProductCategories]
	INSTEAD OF INSERT
	AS
	BEGIN
		SET NOCOUNT ON

		INSERT INTO ProductCategories(ProductId, CategoryId)
		(SELECT i.ProductId, i.CategoryId
		FROM inserted i
		WHERE NOT EXISTS(SELECT cs.CategoryId FROM CategorySubcategories cs WHERE i.CategoryId = cs.CategoryId))
	END
