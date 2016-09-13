CREATE PROCEDURE [dbo].[AnnouncementSave]
	@UsrId int,
	@rawHtml nvarchar(MAX),
	@title varchar(256),
	@AnnId int null
AS
BEGIN	
	IF(@AnnId IS NOT NULL)
	BEGIN
		UPDATE dbo.Announcement SET AnnRawHtmlBody = @rawHtml,  AnnTitle = @title, AnnUpdateDate = SYSUTCDATETIME(), AnnModifiedById = @UsrId WHERE AnnId = @AnnId
	END
	ELSE
	BEGIN
		INSERT INTO dbo.Announcement(AnnTitle,AnnRawHtmlBody,AnnStatus,AnnCreationDate,AnnModifiedById) VALUES (@title,@rawHtml,0,SYSUTCDATETIME(),@UsrId);
	END
END