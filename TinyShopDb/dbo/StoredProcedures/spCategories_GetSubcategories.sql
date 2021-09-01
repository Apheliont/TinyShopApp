CREATE PROCEDURE [dbo].[spCategories_GetSubcategories]
	@CategoryId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT cs.SubcategoryId AS [Id]
	,c.CategoryName
	,c.[Description]
	,(SELECT CASE WHEN EXISTS
	 (SELECT * FROM CategorySubcategories csr WHERE cs.SubcategoryId = csr.CategoryId)
		THEN CAST(1 AS BIT)
		ELSE CAST (0 AS BIT) END) AS [IsParent]
	,i.Id		AS 'Image.Id'
	,i.Caption  AS 'Image.Caption'
	,i.UriSizeS AS 'Image.UriSizeS'
	,i.UriSizeM AS 'Image.UriSizeM'
	,i.UriSizeL AS 'Image.UriSizeL'
	,i.IsMain   AS 'Image.IsMain'
	FROM CategorySubcategories cs
	INNER JOIN Categories c
	ON cs.SubcategoryId = c.Id
	LEFT JOIN Images i
	ON i.Id = c.ImageId
	WHERE cs.CategoryId = @CategoryId
	FOR JSON PATH
END
