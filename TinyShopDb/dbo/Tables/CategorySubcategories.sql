CREATE TABLE [dbo].[CategorySubcategories]
(
	[CategoryId] INT NULL,
	[SubcategoryId] INT NULL,
	CONSTRAINT FK_CategorySubcategories_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
	CONSTRAINT FK_CategorySubcategories_Subcategories FOREIGN KEY (SubcategoryId) REFERENCES Categories(Id)
)
