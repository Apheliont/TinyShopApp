CREATE TRIGGER [OnProductDelete]
	ON Products
	INSTEAD OF DELETE
	AS
	BEGIN
		SET NOCOUNT ON;

		DELETE FROM ProductCategories
		WHERE ProductId IN (SELECT Id FROM deleted)

		DELETE Products
		FROM deleted
		WHERE Products.Id IN (SELECT Id FROM deleted)
	END