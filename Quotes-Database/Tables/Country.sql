﻿CREATE TABLE [dbo].[Country]
(
	[CtyId] INT NOT NULL IDENTITY(1,1), 
	[CtyRef] CHAR(2) NOT NULL PRIMARY KEY,
    [CtyLabel] VARCHAR(128) NULL

)
