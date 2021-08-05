CREATE TABLE [dbo].[Images]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Caption] NVARCHAR(300) NOT NULL DEFAULT 'Image',
	[Size] CHAR(1) NOT NULL DEFAULT 'M', 	-- T - tiny, S - small, M - medium, L - large
	[Uri] NVARCHAR(300) NOT NULL,
	[IsMain] BIT NOT NULL DEFAULT 0
)
