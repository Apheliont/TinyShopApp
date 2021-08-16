CREATE TABLE [dbo].[CategoryImage]
(
	[CategoryId] INT NOT NULL PRIMARY KEY,
	[ImageId] INT NULL,
	CONSTRAINT FK_CategoryImage_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE,
	CONSTRAINT FK_CategoryImage_Images FOREIGN KEY (ImageId) REFERENCES Images(Id) ON DELETE SET NULL,
	CONSTRAINT UK_CategoryImage UNIQUE([CategoryId])
)
