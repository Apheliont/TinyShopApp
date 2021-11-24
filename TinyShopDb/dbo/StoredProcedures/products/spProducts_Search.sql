CREATE PROCEDURE [dbo].[spProducts_Search]
	@SearchSentence NVARCHAR(500),
	@NumberOfRecords INT = 10
AS
BEGIN
SET NOCOUNT ON
SELECT JSON_QUERY(
	(SELECT
		 p.Id
		,p.ProductName
		,p.[Description]
		,p.Price
		,(
			SELECT i.Id, i.Caption, i.UriSizeS, i.UriSizeM, i.UriSizeL
			FROM ProductImages pis
			LEFT JOIN Images AS i
			ON i.Id = pis.ImageId
			WHERE pis.ProductId = p.Id
			FOR JSON PATH
			  ) AS Images
		FROM Products p
		INNER JOIN CONTAINSTABLE (Products, *, @SearchSentence, @NumberOfRecords) AS rt
		ON p.Id = rt.[Key]
		ORDER BY rt.Rank DESC
		FOR JSON AUTO))
END