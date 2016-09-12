CREATE PROCEDURE [dbo].[QuoteTagSave]
	@TagLabel varchar(32),
	@QuoteId int,
	@UserId int
AS
	INSERT INTO QuoteUserTag (QutTagId,QutQuoId,QutUsrId)
	SELECT TagId,QuoId,QuoUsrId FROM TAG , Quote quo INNER JOIN AspNetUsers ON Id = quo.QuoId WHERE quo.QuoId = @QuoteId AND TagLabel = @TagLabel
	
