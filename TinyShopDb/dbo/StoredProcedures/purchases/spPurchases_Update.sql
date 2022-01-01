CREATE PROCEDURE [dbo].[spPurchases_Update]
	@UserId NVARCHAR(450),
	@PurchaseId INT,
	@Quantity INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CartId INT = (SELECT CartId FROM UserCarts WHERE UserId = @UserId)
	IF EXISTS (SELECT Id FROM CartPurchases WHERE CartId = @CartId AND PurchaseId = @PurchaseId)
		BEGIN
			UPDATE Purchases SET Quantity = @Quantity WHERE Id = @PurchaseId

			UPDATE Carts
			SET ItemQuantity = (SELECT SUM(Quantity)
								FROM Purchases p
								INNER JOIN CartPurchases cp
								ON p.Id = cp.PurchaseId
								WHERE cp.CartId = @CartId)
			,UpdatedAt = GETUTCDATE()
			WHERE Id = @CartId
		END
END

