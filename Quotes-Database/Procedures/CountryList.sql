CREATE PROCEDURE [dbo].[CountryList]
AS
	SELECT CtyId , CtyRef AS RefCountry,CtyLabel AS LibCountry FROM dbo.Country ORDER BY 2

