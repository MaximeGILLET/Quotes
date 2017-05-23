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
		MaiLabel,
		MaiCreatedDate
		FROM dbo.Mail 
		INNER JOIN dbo.AspNetUsers asp ON asp.Id = MaiSenderId
		WHERE MaiSenderId = @UserId
		AND ISNULL(MailIsDeleted,0) = 0
	END 
	ELSE
	BEGIN 
		SELECT 
		MaiId,
		asp.UserName,
		MaiObject,
		MaiContent,
		MaiLabel,
		MaiCreatedDate
		FROM dbo.Mail 
		INNER JOIN dbo.MailRecipient mar ON mar.MarMaiId = MaiId AND mar.MarRecipientId = @UserId
		INNER JOIN dbo.AspNetUsers asp ON asp.Id = MaiSenderId
		AND ISNULL(MailIsDeleted,0) !=1
		AND (@Archived IS NULL OR @Archived = MailIsArchived)
	END
	
