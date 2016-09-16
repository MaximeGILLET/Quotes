CREATE PROCEDURE [dbo].[LastRegisterUserList]
AS
	SELECT TOP 10 UserName , RegistrationDate FROM dbo.AspNetUsers WHERE EmailConfirmed = 1 ORDER BY RegistrationDate DESC
