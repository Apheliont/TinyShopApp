CREATE PROCEDURE [dbo].[spImages_Add]
	@Caption NVARCHAR(300),
	@Size CHAR(1) = 'M',
	@Uri NVARCHAR(300),
	@IsMain BIT = 0,
	@ProductId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Images(Caption, Size, Uri, IsMain)
	VALUES (@Caption, @Size, @Uri, @IsMain)
	IF @ProductId IS NOT NULL
		BEGIN
			INSERT INTO ProductImages(ImageId, ProductId)
			VALUES (SCOPE_IDENTITY(), @ProductId)
		END
END
