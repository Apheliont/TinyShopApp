CREATE TABLE [dbo].[ProductCategories]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ProductId] INT NULL, 
    [CategoryId] INT NULL, 
    CONSTRAINT [FK_ProductCategories_Products] FOREIGN KEY (ProductId) REFERENCES Products(Id), 
    CONSTRAINT [FK_ProductCategories_Categories] FOREIGN KEY (CategoryId) REFERENCES Categories(Id)

)
