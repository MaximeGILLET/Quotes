CREATE PROCEDURE [dbo].[UserUnFollow]
	@targetId int,
	@followerId int
AS
	DELETE dbo.UserFollowings WHERE UsrId = @targetId AND FollowerId = @followerId
