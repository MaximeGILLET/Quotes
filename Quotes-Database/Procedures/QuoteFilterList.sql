CREATE PROCEDURE [dbo].[QuoteFilterList]
	@text nvarchar(32) = null,
	@userName varchar(32) = null,	
	@from datetime2 = '',
	@to datetime2 = null,
	@maxRecords int = 1000
AS
	SELECT TOP (@maxRecords) QuoId,QuoUsrId,QuoText,QuoDate , Usr.UserName FROM dbo.Quote 
	INNER JOIN dbo.AspNetUsers Usr ON Usr.UserName = @userName 
	WHERE (@text IS NULL OR CHARINDEX(@text, QuoText) > 0)
	AND QuoDate >= @from
	AND QuoDate <= ISNULL(@to,SYSUTCDATETIME())

