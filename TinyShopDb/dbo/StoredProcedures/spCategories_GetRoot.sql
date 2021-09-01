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
	,i.Id		AS 'Image.Id'
	,i.Caption  AS 'Image.Caption'
	,i.IsMain   AS 'Image.IsMain'
	,i.UriSizeS AS 'Image.UriSizeS'
	,i.UriSizeM AS 'Image.UriSizeM'
	,i.UriSizeL AS 'Image.UriSizeL'
	FROM Categories c
	LEFT JOIN Images i
	ON i.Id = c.ImageId
	WHERE c.Id NOT IN
	(SELECT cs.SubcategoryId FROM CategorySubcategories cs)
	FOR JSON PATH
END
