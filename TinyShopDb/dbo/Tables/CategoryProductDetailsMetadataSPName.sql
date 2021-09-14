CREATE TABLE [dbo].[CategoryProductDetailsMetadataSPName]
(
	[CategoryId] INT NOT NULL,
	[ProductDetailsMetadataSPName] VARCHAR(100) NOT NULL,
	CONSTRAINT [FK_CategoryProductDetailsMetadataSPName_Categories] FOREIGN KEY ([CategoryId]) REFERENCES Categories(Id) ON DELETE CASCADE
)
