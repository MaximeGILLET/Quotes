CREATE TABLE [dbo].[Announcement]
(
	[AnnId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[AnnTitle] NVARCHAR(256) NULL,
    [AnnRawHtmlBody] NVARCHAR(MAX) NULL,
	[AnnCreationDate] DATETIME2 NULL, 
	[AnnCreatedById] int NULL,
    [AnnUpdateDate] DATETIME2 NULL, 
    [AnnModifiedById] int NULL, 
    [AnnStatus] INT NULL 
)
