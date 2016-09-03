CREATE PROCEDURE [dbo].[QuoteSave]
	@QuoId int = 0,
	@UsrId int ,
	@text nvarchar(MAX)
	
AS
	IF EXISTS(SELECT 1 FROM dbo.AspNetUsers WHERE Id = @UsrId)
	BEGIN
		IF EXISTS(SELECT 1 FROM dbo.Quote WHERE QuoId = @QuoId)
		BEGIN
			UPDATE dbo.Quote SET QuoText = @text WHERE QuoId = @QuoId AND QuoUsrId = @UsrId
		END
		ELSE
		BEGIN
			INSERT INTO dbo.Quote (QuoUsrId,QuoText,QuoDate) VALUES (@UsrId,@text,SYSUTCDATETIME())
		END
	END

