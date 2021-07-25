CREATE TABLE [dbo].[CartPurchases]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CartId] INT NOT NULL, 
    [PurchaseId] INT NOT NULL, 
    CONSTRAINT [FK_CartPurchases_Carts] FOREIGN KEY ([CartId]) REFERENCES Carts(Id), 
    CONSTRAINT [FK_CartPurchases_Purchases] FOREIGN KEY ([PurchaseId]) REFERENCES Purchases(Id) ON DELETE CASCADE,

)
