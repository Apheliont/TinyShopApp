CREATE PROCEDURE [dbo].[spProducts_GetManyByIds]
	@Ids NVARCHAR(1000)
AS
BEGIN
	SET NOCOUNT ON;

	;with ids (id) as
	(
    SELECT value FROM STRING_SPLIT(@Ids,',')
	)

	SELECT 
		 p.Id
		,p.ProductName
		,p.[Description]
		,p.Price
		,Images.Id
		,Images.Caption
		,Images.IsMain
		,Images.UriSizeS
		,Images.UriSizeM
		,Images.UriSizeL
		FROM Products p
		INNER JOIN ids
		ON p.Id = ids.id
		LEFT JOIN ProductImages pis
		ON pis.ProductId = p.Id
		LEFT JOIN Images AS Images
		ON Images.Id = pis.ImageId
		FOR JSON AUTO
END

