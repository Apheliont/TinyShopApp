CREATE TRIGGER [OnCategoryDelete]
	ON Categories
	INSTEAD OF DELETE
	AS
	BEGIN
		SET NOCOUNT ON;

		DELETE FROM CategorySubcategories
		WHERE CategoryId IN (SELECT Id FROM deleted) OR SubcategoryId IN (SELECT Id FROM deleted)

		DELETE Categories
		FROM deleted
		WHERE Categories.Id IN (SELECT Id FROM deleted)
	END
