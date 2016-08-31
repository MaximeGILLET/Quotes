CREATE PROCEDURE [dbo].[QuoteSave]
	@QuoId int = 0,
	@UsrId int ,
	@text nvarchar(MAX)
	
AS
	INSERT INTO dbo.Quote (QuoUsrId,QuoText,QuoDate) VALUES (@QuoId,@UsrId,@text)

