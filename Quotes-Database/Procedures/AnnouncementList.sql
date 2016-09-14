CREATE PROCEDURE [dbo].[AnnouncementList]
AS
	SELECT AnnId,AnnTitle,AnnRawHtmlBody,AnnStatus,AnnCreationDate, ISNULL(Username,'Unknown') AS Author,AnnModifiedById,AnnUpdateDate 
	FROM dbo.Announcement ann 
	LEFT JOIN dbo.AspNetUsers usr ON usr.Id = ann.AnnCreatedById
	ORDER BY AnnCreationDate DESC
