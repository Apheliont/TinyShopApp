﻿CREATE TABLE [dbo].[ProductRatings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductId] INT NOT NULL,
	[UserId] NVARCHAR(450) NULL,
	[Rating] TINYINT NOT NULL DEFAULT 0,
	[CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	CONSTRAINT FK_ProductRatings_Products FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
	CONSTRAINT FK_ProductRatings_AspNetUsers FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE SET NULL,
	CONSTRAINT UQ_ProductRatings_UserProduct UNIQUE(ProductId, UserId)
)
