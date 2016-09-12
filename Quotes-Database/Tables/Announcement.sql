CREATE TABLE [dbo].[Announcement]
(
	[AnnId] INT NOT NULL PRIMARY KEY, 
	[AnnTitle] NVARCHAR(256) NULL,
    [AnnRawHtmlBody] NVARCHAR(MAX) NULL,
	[AnnCreationDate] DATETIME2 NULL, 
    [AnnUpdateDate] DATETIME2 NULL, 
    [AnnModifiedById] NCHAR(10) NULL, 
)
