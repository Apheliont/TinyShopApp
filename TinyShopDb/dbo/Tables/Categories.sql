﻿CREATE TABLE [dbo].[Categories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CategoryName] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(2000) NOT NULL
)
