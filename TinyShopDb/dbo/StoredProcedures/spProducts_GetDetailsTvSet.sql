CREATE PROCEDURE [dbo].[spProducts_GetDetailsTvSet]
	@ProductId INT,
	@JsonResult nvarchar(max) OUTPUT
AS
BEGIN
SET NOCOUNT ON
	IF EXISTS(SELECT * FROM TvSetDetails p WHERE p.ProductId = @ProductId)
	BEGIN
		SELECT @JsonResult = CAST((SELECT
				p.Warranty					AS 'Manufacturer.Warranty',
				p.MadeIn					AS 'Manufacturer.MadeIn',
				p.[Type]					AS 'GeneralParameters.Type',
				p.Model						AS 'GeneralParameters.Model',
				p.Voltage					AS 'GeneralParameters.Voltage',
				p.YearOfManufacture			AS 'GeneralParameters.YearOfManufacture',
				p.ScreenSize				AS 'Screen.ScreenSize',
				p.ScreenResolution			AS 'Screen.ScreenResolution',
				p.ScreenFormat				AS 'Screen.ScreenFormat',
				p.RefreshRate				AS 'Matrix.RefreshRate',
				p.MatrixType				AS 'Matrix.MatrixType',
				p.SmartTvOs					AS 'SmartTv.SmartTvOs',
				p.Wifi						AS 'SmartTv.Wifi',
				p.SoundPower				AS 'SoundSystem.SoundPower',
				p.Subwoofer					AS 'SoundSystem.Subwoofer',
				p.ExternalStoragePlay		AS 'Multimedia.ExternalStoragePlay',
				p.ExternalStorageInterface	AS 'Multimedia.ExternalStorageInterface',
				p.VideoCodecs				AS 'Multimedia.VideoCodecs',
				p.HdmiPorts					AS 'ExternalInterfaces.HdmiPorts',
				p.HdmiVersion				AS 'ExternalInterfaces.HdmiVersion',
				p.HeadphoneOut				AS 'ExternalInterfaces.HeadphoneOut',
				p.Height					AS 'Dimensions.Height',
				p.Width						AS 'Dimensions.Width',
				p.Depth						AS 'Dimensions.Depth',
				p.[Weight]					AS 'Dimensions.Weight'
			FROM TvSetDetails p
			WHERE p.ProductId = @ProductId
			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NVARCHAR(max))
	END
END
RETURN

