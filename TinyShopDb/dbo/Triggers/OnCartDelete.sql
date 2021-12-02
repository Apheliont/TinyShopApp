CREATE TRIGGER [OnCartDelete]
	ON Carts
	INSTEAD OF DELETE
	AS
	BEGIN
		SET NOCOUNT ON;

		DELETE FROM CartPurchases
		WHERE CartId IN (SELECT Id FROM deleted)

		DELETE Carts
		FROM deleted
		WHERE Carts.Id IN (SELECT Id FROM deleted)
	END
