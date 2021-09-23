CREATE PROCEDURE [dbo].[spProduct_GetDetailsMetadataTvSet]
	@JsonResult nvarchar(max) OUTPUT
AS
BEGIN
SET NOCOUNT ON
	BEGIN
		SELECT @JsonResult = (SELECT
				'TvSetFilterModel'		AS 'DetailsFilterModelName'
				,'YearOfManufacture.Names' = 	(JSON_QUERY((SELECT [dbo].sfnToRawJsonArray(
											(SELECT DISTINCT YearOfManufacture
											FROM TvSetDetails
											ORDER BY YearOfManufacture DESC
											FOR JSON AUTO), 'YearOfManufacture'))))
				,MIN(p.ScreenSize)				AS 'ScreenSize.LowerBound'
				,MAX(p.ScreenSize)				AS 'ScreenSize.UpperBound'
				,'inch'							AS 'ScreenSize.Measurement'
				,'ScreenResolution.Names' =	(JSON_QUERY((SELECT [dbo].sfnToRawJsonArray(
											(SELECT DISTINCT ScreenResolution
											 FROM TvSetDetails FOR JSON AUTO),
											 'ScreenResolution'))))
				,MIN(p.RefreshRate)				AS 'RefreshRate.LowerBound'
				,MAX(p.RefreshRate)				AS 'RefreshRate.UpperBound'
				,'hz'							AS 'RefreshRate.Measurement'
				,'MatrixType.Names' =			(JSON_QUERY((SELECT [dbo].sfnToRawJsonArray(
											(SELECT DISTINCT MatrixType
											FROM TvSetDetails
											ORDER BY MatrixType DESC
											FOR JSON AUTO),'MatrixType'))))
				,MIN(p.Height)					AS 'Height.LowerBound'
				,MAX(p.Height)					AS 'Height.UpperBound'
				,'cm'							AS 'Height.Measurement'
				,MIN(p.Width)					AS 'Width.LowerBound'
				,MAX(p.Width)					AS 'Width.UpperBound'
				,'cm'							AS 'Width.Measurement'
				,MIN(p.Depth)					AS 'Depth.LowerBound'
				,MAX(p.Depth)					AS 'Depth.UpperBound'
				,'cm'							AS 'Depth.Measurement'
				,MIN(p.[Weight])				AS 'Weight.LowerBound'
				,MAX(p.[Weight])				AS 'Weight.UpperBound'
				,'kg'							AS 'Weight.Measurement'
			FROM TvSetDetails p

			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
	END
END
RETURN


