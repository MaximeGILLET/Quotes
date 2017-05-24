CREATE PROCEDURE [dbo].[MailSave]
	@UserId int,
	@Content nvarchar(512),
	@Label nvarchar(56),
	@Object nvarchar(255),
	@MailId int null,
	@MailParentId int null,
	@MailSendDate datetime2(7) null,
	@RecipientListId dbo.[ListId] readonly
AS
	SET NOCOUNT ON; 

	-- Create temp table representing potential new recipient data
	SELECT
	@MailId MailId,
	Id
	INTO #Recipient
	FROM @RecipientListId

	-- Update Mail
	IF @MailId IS NOT NULL
	BEGIN
		BEGIN TRAN;
		UPDATE dbo.Mail SET MaiContent = @Content, MaiLabel = @Label , MaiObject = @Object, MaiParentId = @MailParentId,
		MaiSentDate = @MailSendDate
		WHERE MaiId = @MailId AND MaiSenderId = @UserId;

		-- SQL Merging Recipients
		MERGE dbo.MailRecipient AS T
		USING #Recipient AS S
		ON (T.MarMaiId = S.MailId AND T.MarRecipientId = S.Id)
		WHEN NOT MATCHED BY TARGET
			THEN INSERT(MarMaiId,MarRecipientId) VALUES(S.MailId, S.Id)
		WHEN NOT MATCHED BY SOURCE
			THEN DELETE
		OUTPUT $action, Inserted.*, Deleted.*;
		ROLLBACK TRAN;

	END
	ELSE -- New Mail
	BEGIN
		BEGIN TRAN;
		INSERT INTO dbo.Mail
		VALUES (@UserId,@Object,@Label,@Content,SYSDATETIME(),@MailParentId,NULL,NULL,@MailSendDate);

		-- SQL Merging Recipients
		MERGE dbo.MailRecipient AS T
		USING #Recipient AS S
		ON (T.MarMaiId = S.MailId AND T.MarRecipientId = S.Id)
		WHEN NOT MATCHED BY TARGET
			THEN INSERT(MarMaiId,MarRecipientId) VALUES(S.MailId, S.Id)
		WHEN NOT MATCHED BY SOURCE
			THEN DELETE
		OUTPUT $action, Inserted.*, Deleted.*;
		ROLLBACK TRAN;
	END


