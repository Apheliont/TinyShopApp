CREATE FUNCTION [dbo].[sfnToRawJsonArray]
(
	@json NVARCHAR(MAX),
	@key NVARCHAR(400)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @new NVARCHAR(MAX) = REPLACE(@json, CONCAT('},{"', @key,'":'),',')
       RETURN '[' + SUBSTRING(@new, 1 + (LEN(@key)+5), LEN(@new) -2 - (LEN(@key)+5)) + ']'
END
