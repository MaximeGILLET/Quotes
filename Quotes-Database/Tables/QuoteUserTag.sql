CREATE TABLE [dbo].[QuoteUserTag]
(
	[QutId] INT NOT NULL IDENTITY,
	[QutQuoId] INT NOT NULL,
	[QutTagId] INT NOT NULL,
	[QutUsrId] INT NOT  NULL, 
    CONSTRAINT [PK_QuoteUserTag] PRIMARY KEY ([QutQuoId],[QutTagId],[QutUsrId])
)
