CREATE PROCEDURE [dbo].[spPurchases_Delete]
	@UserId NVARCHAR(450),
	@PurchaseId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Quantity INT = (SELECT Quantity FROM Purchases WHERE Id = @PurchaseId)

	DELETE FROM Purchases WHERE Id = @PurchaseId

	DECLARE @CartId INT = (SELECT CartId FROM UserCarts WHERE UserId = @UserId)

	UPDATE Carts
	SET ItemQuantity = IIF(ItemQuantity - @Quantity >= 0, ItemQuantity - @Quantity, 0)
	,UpdatedAt = GETUTCDATE()
	WHERE Id = @CartId
END
