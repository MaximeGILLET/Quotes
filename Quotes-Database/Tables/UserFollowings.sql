CREATE TABLE [dbo].[UserFollowings]
(
	[UsrId] INT NOT NULL,
	[FollowerId] INT NOT NULL, 
    [TimeStamp] DATETIME2 NULL, 
    CONSTRAINT [FK_UserFollowings_AspNetUsers] FOREIGN KEY (UsrId) REFERENCES AspNetUsers(Id),
	CONSTRAINT [FK_UserFollowings_AspNetUsers2] FOREIGN KEY (FollowerId) REFERENCES AspNetUsers(Id), 
    CONSTRAINT [PK_UserFollowings] PRIMARY KEY (UsrId,FollowerId)
)
