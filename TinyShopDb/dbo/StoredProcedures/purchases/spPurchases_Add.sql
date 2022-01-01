CREATE PROCEDURE [dbo].[spPurchases_Add]
	@UserId NVARCHAR(450),
	@ProductId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CartId INT = (SELECT CartId FROM UserCarts WHERE UserId = @UserId)

	DECLARE @PurchaseId INT = (SELECT TOP (1) p.Id
		FROM CartPurchases cp
		INNER JOIN Purchases p
		ON p.Id = cp.PurchaseId
		WHERE cp.CartId = @CartId AND p.ProductId = @ProductId)


	IF (@PurchaseId IS NULL)
		BEGIN
			INSERT INTO Purchases(ProductId, Quantity)
			VALUES (@ProductId, 1);

			SET @PurchaseId = (SELECT SCOPE_IDENTITY())

			INSERT INTO CartPurchases(CartId, PurchaseId) 
			VALUES (@CartId, @PurchaseId);

			UPDATE Carts SET ItemQuantity = (ItemQuantity + 1), UpdatedAt = GETUTCDATE() WHERE Id = @CartId
		END
	SELECT @PurchaseId
END
