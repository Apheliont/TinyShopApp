CREATE TABLE [dbo].[ProductCategories]
(
    [ProductId] INT NULL, 
    [CategoryId] INT NULL, 
    CONSTRAINT [FK_ProductCategories_Products] FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE NO ACTION, 
    CONSTRAINT [FK_ProductCategories_Categories] FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE SET NULL,
)
