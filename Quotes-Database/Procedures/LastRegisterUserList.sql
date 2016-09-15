CREATE PROCEDURE [dbo].[LastRegisterUserList]
AS
	SELECT TOP 10 UserName , RegistrationDate FROM dbo.AspNetUsers ORDER BY RegistrationDate DESC
