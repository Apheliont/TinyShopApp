CREATE PROCEDURE [dbo].[spPurchases_GetItemsCountInCart]
	@UserId NVARCHAR(450)
AS
BEGIN
	SET NOCOUNT ON

	SELECT SUM(p.Quantity)
	FROM	CartPurchases cp
			INNER JOIN Purchases p
			ON p.Id = cp.PurchaseId
	WHERE	cp.CartId = (
			SELECT CartId
			FROM UserCarts c
			WHERE c.UserId = @UserId
			) AND p.CheckoutDate IS NULL
END
