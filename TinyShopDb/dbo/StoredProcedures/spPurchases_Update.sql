CREATE PROCEDURE [dbo].[spPurchases_Update]
	@PurchaseId INT,
	@Quantity INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Purchases
	SET Quantity = @Quantity
	WHERE Id = @PurchaseId
END
