CREATE PROCEDURE [dbo].[spImages_Add]
	@Caption NVARCHAR(300),
	@UriSizeS NVARCHAR(300) = NULL,
	@UriSizeM NVARCHAR(300),
	@UriSizeL NVARCHAR(300) = NULL,
	@IsMain BIT = 0,
	@ProductId INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Images(Caption, UriSizeS, UriSizeM, UriSizeL, IsMain)
	VALUES (@Caption, @UriSizeS, @UriSizeM, @UriSizeL, @IsMain)
	INSERT INTO ProductImages(ImageId, ProductId)
	VALUES (SCOPE_IDENTITY(), @ProductId)

END
