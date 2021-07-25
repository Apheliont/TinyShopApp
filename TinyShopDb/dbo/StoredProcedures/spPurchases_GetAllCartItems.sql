CREATE PROCEDURE [dbo].[spPurchases_GetAllCartItems]
	@UserId NVARCHAR(450)
AS
BEGIN
SET NOCOUNT ON;


SELECT	 p.Id
		,pr.ProductName
		,p.Price
		,p.Quantity
FROM	CartPurchases cp
		INNER JOIN Purchases p
		ON p.Id = cp.PurchaseId
		INNER JOIN Products pr
		ON pr.Id = p.ProductId
WHERE	cp.CartId = (
		SELECT CartId
		FROM UserCarts c
		WHERE c.UserId = @UserId
		) AND p.CheckoutDate IS NULL
END
