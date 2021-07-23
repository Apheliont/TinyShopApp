CREATE TABLE [dbo].[Purchases]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProductId] INT NULL,
    [Quantity] INT NOT NULL DEFAULT 1, 
    [Price] MONEY NOT NULL, 
    [PurchaseDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(), 
    [CheckoutDate] DATETIME2 NULL, 
    CONSTRAINT [FK_Purchases_Products] FOREIGN KEY ([ProductId]) REFERENCES Products(Id),

)
