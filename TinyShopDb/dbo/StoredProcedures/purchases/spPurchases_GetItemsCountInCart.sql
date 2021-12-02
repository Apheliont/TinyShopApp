CREATE PROCEDURE [dbo].[spPurchases_GetItemsCountInCart]
	@UserId NVARCHAR(450)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @CartId INT = (SELECT CartId FROM UserCarts WHERE UserId = @UserId)

	SELECT ItemQuantity
	FROM Carts
	WHERE Id = @CartId
END
