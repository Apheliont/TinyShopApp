CREATE TABLE [dbo].[ProductCategories]
(
    [ProductId] INT NULL, 
    [CategoryId] INT NULL, 
    CONSTRAINT [FK_ProductCategories_Products] FOREIGN KEY (ProductId) REFERENCES Products(Id), 
    CONSTRAINT [FK_ProductCategories_Categories] FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
)
