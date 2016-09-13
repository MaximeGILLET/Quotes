CREATE PROCEDURE [dbo].[AnnouncementList]
AS
	SELECT AnnId,AnnTitle,AnnRawHtmlBody,AnnStatus,AnnCreationDate,AnnModifiedById,AnnUpdateDate FROM Announcement ORDER BY AnnCreationDate
