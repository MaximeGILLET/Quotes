CREATE PROCEDURE [dbo].[AnnouncementList]
	@status varchar(32) = null
AS
	SELECT AnnId,AnnTitle,AnnRawHtmlBody,ans.AnsLabel as [Status],AnnCreationDate, ISNULL(Username,'Unknown') AS Author,AnnModifiedById,AnnUpdateDate 
	FROM dbo.Announcement ann 
	INNER JOIN dbo.AnnouncementStatus ans ON ans.AnsId = ann.AnnStatus
	LEFT JOIN dbo.AspNetUsers usr ON usr.Id = ann.AnnCreatedById
	WHERE (@status IS NULL OR @status = ans.AnsLabel)
	ORDER BY AnnCreationDate DESC
