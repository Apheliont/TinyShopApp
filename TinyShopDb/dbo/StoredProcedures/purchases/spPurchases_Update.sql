CREATE PROCEDURE [dbo].[spPurchases_Update]
	@UserId NVARCHAR(450),
	@PurchaseId INT,
	@Quantity INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CartId INT = (SELECT CartId FROM UserCarts WHERE UserId = @UserId)
	DECLARE @PreviousQuantity INT = (SELECT Quantity FROM Purchases WHERE Id = @PurchaseId)

	UPDATE Purchases SET Quantity = @Quantity WHERE Id = @PurchaseId

	UPDATE Carts
	SET ItemQuantity = (@Quantity - @PreviousQuantity)
	,UpdatedAt = GETUTCDATE()
	WHERE Id = @CartId
END
