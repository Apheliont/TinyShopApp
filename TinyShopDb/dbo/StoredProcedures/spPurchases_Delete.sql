CREATE PROCEDURE [dbo].[spPurchases_Delete]
	@PurchaseId INT
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM Purchases WHERE Id = @PurchaseId
END
