CREATE PROCEDURE [dbo].[spCategories_GetRoot]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT c.Id
	,c.CategoryName
	,c.[Description]
	,(SELECT CASE WHEN EXISTS
		(SELECT * FROM CategorySubcategories cs WHERE c.Id = cs.CategoryId)
		 THEN CAST(1 AS BIT)
		 ELSE CAST (0 AS BIT) END) AS [IsParent]
	,i.Id
	,i.Caption
	,i.IsMain
	,i.Uri
	FROM Categories c
	LEFT JOIN CategoryImage ci
	ON c.Id = ci.CategoryId
	LEFT JOIN Images i
	ON ci.ImageId = i.Id
	WHERE c.Id NOT IN
	(SELECT cs.SubcategoryId FROM CategorySubcategories cs)
END
