CREATE TABLE [dbo].[Mail]
(
	[MaiId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[MaiSenderId] INT NOT NULL,
	[MaiObject] VARCHAR(255) NOT NULL,
	[MaiLabel] NVARCHAR(56) NULL,
	[MaiContent] NVARCHAR(512) NOT NULL,
	[MaiCreatedDate] datetime2(7) NOT NULL,
	[MaiParentId] INT NULL,
	[MailIsDeleted] BIT NULL, 
    [MailIsArchived] BIT NULL
)
