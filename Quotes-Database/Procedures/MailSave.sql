CREATE PROCEDURE [dbo].[MailSave]
	@UserId int,
	@Content nvarchar(512),
	@Label nvarchar(56),
	@Object nvarchar(255),
	@MailId int null,
	@MailParentId int null,
	@RecipientListId dbo.[ListId] readonly
AS
	SET NOCOUNT ON; 
	-- Update Mail
	IF @MailId IS NOT NULL
	BEGIN
		UPDATE dbo.Mail SET MaiContent = @Content, MaiLabel = @Label , MaiObject = @Object, MaiParentId = @MailParentId
		WHERE MaiId = @MailId AND MaiSenderId = @UserId;

		-- SQL Merging Recipients TODO
	END
	ELSE -- New Mail
	BEGIN
		INSERT INTO dbo.Mail
		VALUES (@UserId,@Object,@Label,@Content,SYSDATETIME(),@MailParentId,NULL,NULL);

		-- SQL Merging Recipients TODO
	END


