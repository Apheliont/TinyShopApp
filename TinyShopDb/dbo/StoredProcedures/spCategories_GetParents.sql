CREATE PROCEDURE [dbo].[spCategories_GetParents]
	@Id INT,
	@IsProduct BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT p.Id, p.CategoryName FROM [dbo].[tvfCategories_GetParents](@Id, @IsProduct) p
END
