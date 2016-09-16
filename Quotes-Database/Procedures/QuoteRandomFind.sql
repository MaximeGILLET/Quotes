CREATE PROCEDURE [dbo].[QuoteRandomFind]
AS
	SELECT TOP 1  QuoId,QuoUsrId,QuoText,QuoDate , Usr.UserName 
	FROM  dbo.Quote 
	INNER JOIN dbo.AspNetUsers Usr ON Usr.Id = QuoUsrId  
	WHERE QuoStatus = 2 
	ORDER BY NEWID()

