﻿CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductName] NVARCHAR(200) NOT NULL, 
    [Description] NVARCHAR(2000) NOT NULL, 
    [Price] MONEY NOT NULL
)
