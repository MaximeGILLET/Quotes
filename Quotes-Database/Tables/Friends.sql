CREATE TABLE [dbo].[Friends]
(
    [Id] INT NOT NULL IDENTITY(1,1), 
	[UserId] INT NOT NULL , 
    [FriendId] INT NOT NULL, 
    [FriendStatusId] INT NOT NULL, 
 
    PRIMARY KEY ([UserId], [FriendId])
)
