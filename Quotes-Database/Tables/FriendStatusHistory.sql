CREATE TABLE [dbo].[FriendStatusHistory]
(
	[FriendsId] INT NOT NULL PRIMARY KEY, 
    [StatusId] INT NOT NULL, 
    [StatusTimeStamp] DATETIME2 NOT NULL
)
