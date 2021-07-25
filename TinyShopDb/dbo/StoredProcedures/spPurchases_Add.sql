CREATE PROCEDURE [dbo].[spPurchases_Add]
	@UserId NVARCHAR(450),
	@ProductId INT,
	@Quantity INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @CartId INT = (SELECT CartId FROM UserCarts WHERE UserId = @UserId)
	DECLARE @Price MONEY = (SELECT Price FROM Products WHERE Id = @ProductId)

	IF EXISTS (SELECT * FROM Purchases WHERE ProductId = @ProductId)
		UPDATE Purchases SET Quantity = (Quantity + 1) WHERE ProductId = @ProductId
	ELSE
		BEGIN
			INSERT INTO Purchases(ProductId, Quantity, Price)
			VALUES (@ProductId, @Quantity, @Price);

			INSERT INTO CartPurchases(CartId, PurchaseId) 
			VALUES (@CartId, (SELECT SCOPE_IDENTITY()));
		END
END
