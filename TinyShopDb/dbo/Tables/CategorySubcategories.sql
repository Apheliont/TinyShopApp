CREATE TABLE [dbo].[CategorySubcategories]
(
	[CategoryId] INT NOT NULL,
	[SubcategoryId] INT NOT NULL,
	CONSTRAINT FK_CategorySubcategories_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE,
	CONSTRAINT FK_CategorySubcategories_Subcategories FOREIGN KEY (SubcategoryId) REFERENCES Categories(Id) ON DELETE NO ACTION
)
