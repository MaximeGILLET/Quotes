CREATE PROCEDURE [dbo].[AnnouncementDelete]
	@AnnId int
AS
	DELETE dbo.Announcement WHERE AnnId = @AnnId