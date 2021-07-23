CREATE TRIGGER [OnNewUserCreate]
	ON [dbo].[AspNetUsers]
	AFTER INSERT
	AS
	BEGIN
		SET NOCOUNT ON;
		INSERT INTO [TinyShopData].[dbo].[Cart](UserId) VALUES(inserted.id);
	END
