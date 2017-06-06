CREATE PROCEDURE [dbo].[MailRecipientSave]
	@MailRecipientId int null,
	@IsDeleted bit null
AS
	UPDATE dbo.MailRecipient 
	SET 
	MarIsDeleted = ISNULL(@IsDeleted,MarIsDeleted),
	MarReceptionDate = CASE WHEN MarReceptionDate IS NULL THEN SYSDATETIME() ELSE MarReceptionDate END	
	WHERE MarId = @MailRecipientId
