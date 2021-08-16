CREATE PROCEDURE [dbo].[spProducts_GetOneDetailed]
	@ProductId INT
AS
BEGIN
	SET NOCOUNT ON;
	
		SELECT 
		   p.Id
		  ,p.ProductName
		  ,p.[Description]
		  ,p.Price
		  ,i.Id
		  ,i.Caption
		  ,i.IsMain
		  ,i.UriSizeS
		  ,i.UriSizeM
		  ,i.UriSizeL

	FROM Products p
	LEFT JOIN ProductImages pis
	ON pis.ProductId = p.Id
	LEFT JOIN Images i
	ON i.Id = pis.ImageId
	WHERE p.Id = @ProductId
END
