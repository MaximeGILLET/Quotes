CREATE TABLE [dbo].[Mail]
(
	[MaiId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[MaiSenderId] INT NOT NULL,
	[MaiObject] VARCHAR(255) NULL,
	[MaiLabel] NVARCHAR(56) NULL,
	[MaiContent] NVARCHAR(512) NULL,
	[MaiCreatedDate] datetime2(7) NOT NULL,
	[MaiParentId] INT NULL,
	[MaiIsDeleted] BIT NULL, 
    [MaiIsArchived] BIT NULL, 
    [MaiSentDate] DATETIME2 NULL
)
