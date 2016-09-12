CREATE TABLE [dbo].[Quote]
(
	[QuoId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [QuoText] NVARCHAR(MAX) NULL, 
    [QuoDate] DATETIME2 NOT NULL, 
    [QuoUsrId] INT NULL, 
    [QuoStatus] INT NULL, 
    CONSTRAINT [FK_Quote_AspNetUsers] FOREIGN KEY (QuoUsrId) REFERENCES dbo.AspNetUsers(Id)
)
