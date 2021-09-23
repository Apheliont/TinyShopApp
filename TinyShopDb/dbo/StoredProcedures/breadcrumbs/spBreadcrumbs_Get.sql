CREATE PROCEDURE [dbo].[spBreadcrumbs_Get]
	@Id INT,
	@IsProduct BIT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT p.ItemId AS Id, p.ItemName FROM [dbo].[tvfBreadcrumbs_Get](@Id, @IsProduct) p
	ORDER BY p.Id DESC
	FOR JSON AUTO
END
