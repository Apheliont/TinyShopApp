CREATE PROCEDURE [dbo].[spCategories_GetSubcategories]
	@CategoryId INT
AS
BEGIN
	SELECT cs.SubcategoryId AS [Id]
	,c.CategoryName
	,c.[Description]
	,(SELECT CASE WHEN EXISTS
	 (SELECT * FROM CategorySubcategories csr WHERE cs.SubcategoryId = csr.CategoryId)
		THEN CAST(1 AS BIT)
		ELSE CAST (0 AS BIT) END) AS [IsParent]
	,i.Id
	,i.Caption
	,i.Uri
	,i.IsMain
	FROM CategorySubcategories cs
	INNER JOIN Categories c
	ON cs.SubcategoryId = c.Id
	LEFT JOIN CategoryImage ci
	ON ci.CategoryId = cs.SubcategoryId
	LEFT JOIN Images i
	ON i.Id = ci.ImageId
	WHERE cs.CategoryId = @CategoryId
END
