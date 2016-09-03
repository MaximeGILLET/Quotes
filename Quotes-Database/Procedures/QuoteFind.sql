CREATE PROCEDURE [dbo].[QuoteFind]
	@text nvarchar(MAX)
AS
	SELECT  QuoId,QuoUsrId,QuoText,QuoDate , Usr.UserName FROM dbo.Quote 
	INNER JOIN dbo.AspNetUsers Usr ON Usr.Id = QuoUsrId 
	WHERE  CHARINDEX(@text, QuoText) > 0
	OR CHARINDEX(@text, Usr.UserName) > 0
	ORDER BY QuoDate
-- Later on can implement an order on the number of occurence to ponderate the results (hint : use string replace and cout the number of characters removed from orinigale string to have the number of occurence)