CREATE PROCEDURE [dbo].[spPurchases_Delete]
	@UserId NVARCHAR(450),
	@PurchaseId INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @CartId INT = (SELECT CartId FROM UserCarts WHERE UserId = @UserId)

	IF EXISTS (SELECT Id FROM CartPurchases WHERE CartId = @CartId AND PurchaseId = @PurchaseId)
		BEGIN
			DECLARE @Quantity INT = (SELECT Quantity FROM Purchases WHERE Id = @PurchaseId)
				DELETE FROM Purchases WHERE Id = @PurchaseId



				UPDATE Carts
				SET ItemQuantity = IIF(ItemQuantity - @Quantity >= 0, ItemQuantity - @Quantity, 0)
				,UpdatedAt = GETUTCDATE()
				WHERE Id = @CartId
		END
END
