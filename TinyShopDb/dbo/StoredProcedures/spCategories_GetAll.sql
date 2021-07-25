CREATE PROCEDURE [dbo].[spCategories_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT c.Id, c.CategoryName, c.Description
	FROM Categories c;
END
