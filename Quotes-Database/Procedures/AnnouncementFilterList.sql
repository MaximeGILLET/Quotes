CREATE PROCEDURE [dbo].[AnnouncementFilterList]
	@text nvarchar(32) = null,
	@userName varchar(32) = null,	
	@from datetime2 = '',
	@to datetime2 = null,
	@maxRecords int = 1000
AS
	SELECT TOP (@maxRecords) AnnId,AnnTitle,AnnCreationDate , Usr.UserName FROM dbo.Announcement 
	LEFT JOIN dbo.AspNetUsers Usr ON  AnnCreatedById = Usr.Id
	WHERE (@text IS NULL OR CHARINDEX(@text, AnnTitle) > 0)
	AND (@userName IS NULL OR Usr.UserName = @userName)
	AND AnnCreationDate >= @from
	AND AnnCreationDate <= ISNULL(@to,SYSUTCDATETIME())

