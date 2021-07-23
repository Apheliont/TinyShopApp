CREATE PROCEDURE [dbo].[spCarts_AddPurchase]
	@UserId NVARCHAR(450),
	@ProductId int,
	@Quantity int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Purchases(ProductId, Quantity, Price)
	VALUES (@ProductId, @Quantity, (SELECT Price FROM Products WHERE Id = @ProductId));

	INSERT INTO CartPurchases(CartId, PurchaseId) 
	VALUES ((SELECT Id FROM Carts WHERE UserId = @UserId), (SELECT SCOPE_IDENTITY()));
END
