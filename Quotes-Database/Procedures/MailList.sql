CREATE PROCEDURE [dbo].[MailList]
	@UserId int,
	@Archived bit = NULL,
	@Sent bit = NULL
AS
	IF(@Sent = 1)
	BEGIN
		SELECT 
		MaiId,
		asp.UserName
		MaiObject,
		MaiContent,
		MaiCreatedDate
		FROM dbo.Mail 
		INNER JOIN dbo.AspNetUsers asp ON asp.Id = MaiRecipientId
		WHERE MaiSenderId = @UserId
		AND MailIsDeleted = 0
	END 
	ELSE
	BEGIN 
		SELECT 
		MaiId,
		asp.UserName
		MaiObject,
		MaiContent,
		MaiCreatedDate
		FROM dbo.Mail 
		INNER JOIN dbo.AspNetUsers asp ON asp.Id = MaiSenderId
		WHERE MaiRecipientId = @UserId
		AND MailIsDeleted = 0
		AND (@Archived IS NULL OR @Archived = MailIsArchived)
	END
	
