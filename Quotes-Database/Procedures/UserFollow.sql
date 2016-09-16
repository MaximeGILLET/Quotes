CREATE PROCEDURE [dbo].[UserFollow]
	@targetId int,
	@followerId int
AS
	INSERT INTO dbo.UserFollowings (UsrId,FollowerId,[TimeStamp]) VALUES (@targetId,@followerId, SYSUTCDATETIME())
