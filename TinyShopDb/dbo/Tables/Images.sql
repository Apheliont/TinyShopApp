﻿CREATE TABLE [dbo].[Images]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Caption] NVARCHAR(300) NOT NULL DEFAULT 'Image',
	[UriSizeS] NVARCHAR(300) NULL,
	[UriSizeM] NVARCHAR(300) NOT NULL,
	[UriSizeL] NVARCHAR(300) NULL,
	[IsMain] BIT NOT NULL DEFAULT 0
)