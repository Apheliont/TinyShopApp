CREATE PROCEDURE [dbo].[spPurchases_Add]
	@UserId NVARCHAR(450),
	@ProductId INT,
	@Quantity INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CartId INT = (SELECT CartId FROM UserCarts WHERE UserId = @UserId)

	DECLARE @PurchaseId INT = (SELECT TOP (1) p.Id
		FROM CartPurchases cp
		INNER JOIN Purchases p
		ON p.Id = cp.PurchaseId
		WHERE cp.CartId = @CartId AND p.ProductId = @ProductId)


	IF (@PurchaseId IS NOT NULL)
		UPDATE Purchases SET Quantity = (Quantity + 1) WHERE ProductId = @ProductId AND Id = @PurchaseId
	ELSE
		BEGIN
			INSERT INTO Purchases(ProductId, Quantity)
			VALUES (@ProductId, @Quantity);

			INSERT INTO CartPurchases(CartId, PurchaseId) 
			VALUES (@CartId, (SELECT SCOPE_IDENTITY()));
		END
	UPDATE Carts SET ItemQuantity = (ItemQuantity + @Quantity), UpdatedAt = GETUTCDATE() WHERE Id = @CartId
END
