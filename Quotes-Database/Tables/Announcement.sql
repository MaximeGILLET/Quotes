CREATE TABLE [dbo].[Announcement]
(
	[AnnId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[AnnTitle] NVARCHAR(256) NULL,
    [AnnRawHtmlBody] NVARCHAR(MAX) NULL,
	[AnnCreationDate] DATETIME2 NULL, 
    [AnnUpdateDate] DATETIME2 NULL, 
    [AnnModifiedById] NCHAR(10) NULL, 
    [AnnStatus] INT NULL, 
    CONSTRAINT [FK_Announcement_AnnouncementStatus] FOREIGN KEY ([AnnStatus]) REFERENCES [AnnouncementStatus](AnsId), 
)
