/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DELETE dbo.Tag WHERE TagLabel IN ('Like','Dislike','Star','Flag');
INSERT INTO dbo.Tag (TagLabel,TagTtyId) VALUES ('Like',1),('Dislike',1),('Star',1),('Flag',1)

DELETE dbo.QuoteStatus 
INSERT INTO dbo.QuoteStatus (QuoStatusId,QuoStatusLabel) VALUES (1,'Awaiting Validation'),(2,'Moderated'),(3,'Removed')

DELETE dbo.AspNetRoles 
SET IDENTITY_INSERT dbo.AspNetRoles ON
INSERT INTO dbo.AspNetRoles (Id,Name) VALUES (1,'Admin'),(2,'Moderator')
SET IDENTITY_INSERT dbo.AspNetRoles OFF


DELETE dbo.AspNetUserRoles 
INSERT INTO dbo.AspNetUserRoles (RoleId,UserId) SELECT r.Id,u.Id FROM dbo.AspNetRoles r, dbo.AspNetUsers u WHERE UserName = 'nerevar' AND Name = 'Admin'

DELETE dbo.AnnouncementStatus 
INSERT INTO dbo.AnnouncementStatus (AnsId,AnsLabel) VALUES (1,'Backlog'),(2,'Published'),(3,'Archived')

DELETE dbo.TagType
INSERT INTO dbo.TagType(TtyId,TtyLabel) VALUES (1,'Main'),(2,'Dynamic')