CREATE FULLTEXT INDEX
	ON [dbo].[Products](ProductName language 1033, [Description] language 1033)
	KEY INDEX PK_ProductTable ON ([ProductCatalog])
	WITH CHANGE_TRACKING AUTO, STOPLIST = ProductStoplist
