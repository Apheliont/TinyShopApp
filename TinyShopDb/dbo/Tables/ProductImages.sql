CREATE TABLE [dbo].[ProductImages]
(
	[ProductId] INT NOT NULL,
	[ImageId] INT NOT NULL,
	CONSTRAINT [FK_ProductImages_Products] FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
	CONSTRAINT [FK_ProductImages_Images] FOREIGN KEY (ImageId) REFERENCES Images(Id) ON DELETE CASCADE
)
