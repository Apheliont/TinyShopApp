CREATE TABLE [dbo].[CategoryProductDetails]
(
	[CategoryId] INT NOT NULL,
	[ProductDetailsSPName] VARCHAR(100) NOT NULL,
	[ProductDetailsTableName] VARCHAR(100) NOT NULL, 
    [ProductDetailsMetadataSPName] VARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_CategoryProductDetails_Categories] FOREIGN KEY ([CategoryId]) REFERENCES Categories(Id) ON DELETE CASCADE
)
