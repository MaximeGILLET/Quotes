CREATE TABLE [dbo].[MailRecipient]
(
	[MarId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [MarMaiId] INT NOT NULL, 
    [MarRecipientId] INT NOT NULL, 
    [MarReceptionDate] DATETIME2 NULL, 
    [MarIsDeleted] INT NULL
)
