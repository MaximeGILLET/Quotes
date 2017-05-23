CREATE PROCEDURE [dbo].[MailRecipientFind]
	@MailId int,
	@UserId int
AS
	SELECT 
		MaiId,
		asp.UserName
		MaiObject,
		MaiContent,
		MaiLabel,
		MaiCreatedDate
		FROM dbo.Mail 
		INNER JOIN dbo.MailRecipient mar ON mar.MarMaiId = MaiId AND mar.MarRecipientId = @UserId
		INNER JOIN dbo.AspNetUsers asp ON asp.Id = MaiSenderId
		AND MailIsDeleted = 0
