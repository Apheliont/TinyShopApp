CREATE PROCEDURE [dbo].[spProducts_GetOneDetailed]
	@ProductId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @GetProductMinimalInfo NVARCHAR(MAX)
	DECLARE @ProductDetailsSPName VARCHAR(100)

	SET @ProductDetailsSPName = (
		SELECT cpn.ProductDetailsSPName
		FROM ProductCategories pc
		INNER JOIN CategoryProductDetails cpn
		ON cpn.CategoryId = pc.CategoryId
		WHERE pc.ProductId = @ProductId)


	SET @GetProductMinimalInfo = (SELECT 
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
		LEFT JOIN ProductImages pis
		ON pis.ProductId = p.Id
		LEFT JOIN Images AS Images
		ON Images.Id = pis.ImageId
		WHERE p.Id = @ProductId
		FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER)


		IF (@ProductDetailsSPName IS NOT NULL)
			BEGIN
				DECLARE @JsonResult NVARCHAR(MAX)
				EXEC @ProductDetailsSPName @ProductId, @JsonResult = @JsonResult OUTPUT
				SELECT
					JSON_QUERY(@GetProductMinimalInfo) AS [ProductInfo],
					JSON_QUERY(@JsonResult) AS [ProductDetails]
					FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
			END
		ELSE
			BEGIN
				SELECT JSON_QUERY(@GetProductMinimalInfo) AS [ProductInfo]
				FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
			END
END
