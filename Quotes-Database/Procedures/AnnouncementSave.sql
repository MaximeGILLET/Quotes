CREATE PROCEDURE [dbo].[AnnouncementSave]
	@AnnId int = null,
	@UsrId int,
	@rawHtml nvarchar(MAX) = null,
	@title varchar(256) = null,
	@status varchar(10) = null
AS
BEGIN	


	IF(@AnnId IS NOT NULL)
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM dbo.Announcement where AnnId = @AnnId)
		BEGIN
			RAISERROR('The item you are trying to update does not exist in database',16,1);
			RETURN;
		END
		UPDATE ann SET AnnRawHtmlBody = ISNULL(@rawHtml,AnnRawHtmlBody),  AnnTitle = ISNULL(@title,AnnTitle), AnnStatus = ISNULL(ans.AnsId,AnnStatus),AnnUpdateDate = SYSUTCDATETIME(), AnnModifiedById = @UsrId 
		FROM dbo.Announcement ann LEFT JOIN dbo.AnnouncementStatus  ans ON ans.AnsLabel = @status
		WHERE AnnId = @AnnId
	END
	ELSE
	BEGIN
		INSERT INTO dbo.Announcement(AnnTitle,AnnRawHtmlBody,AnnStatus,AnnCreationDate,AnnCreatedById) VALUES (@title,@rawHtml,1,SYSUTCDATETIME(),@UsrId);
	END
END