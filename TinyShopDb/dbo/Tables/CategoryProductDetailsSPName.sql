CREATE TABLE [dbo].[CategoryProductDetailsSPName]
(
	[CategoryId] INT NOT NULL,
	[ProductDetailsSPName] VARCHAR(100) NOT NULL,
	CONSTRAINT [FK_CategoryProductDetailsSPName_Categories] FOREIGN KEY ([CategoryId]) REFERENCES Categories(Id) ON DELETE CASCADE
)
