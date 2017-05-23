CREATE PROCEDURE [dbo].[FriendList]
	@UserId int
AS
	SELECT
	asp.Id,
	asp.UserName
	FROM dbo.Friends fr
	INNER JOIN AspNetUsers asp ON asp.Id = fr.FriendId
	WHERE fr.FriendStatusId = 1
	AND fr.UserId = @UserId
