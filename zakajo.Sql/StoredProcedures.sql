
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


DROP PROCEDURE GetLinkTypes
GO

Create PROCEDURE GetLinkTypes AS
SELECT [Id]
      ,[Title]
      ,[CreationDate]
      ,[LastUpdateDate]

 FROM [dbo].[LinkType]
 WHERE Status <> 'Deleted'
  GO
---------------------------------------------------------------

DROP PROCEDURE UpdateLinkType
GO
Create PROCEDURE UpdateLinkType
@Id INT,
@Title NVARCHAR(255)
AS

UPDATE [dbo].[LinkType] 
    SET  [Title] = @Title,
    [LastUpdateDate] = GETDATE()
    WHERE [Id]=@Id
GO
------------------------------------------------------------------

DROP PROCEDURE CreateLinkType
GO
Create PROCEDURE CreateLinkType
@Title NVARCHAR(255)
AS

Insert Into [dbo].[LinkType] ([Title],[CreationDate],[LastUpdateDate],[Status]) Values (@TItle, GETDATE(),GETDATE(),'Active')
GO

-------------------------------------------------------------

DROP PROCEDURE DeleteLinkType
GO
Create PROCEDURE DeleteLinkType
@Id INT
AS

UPDATE [dbo].[LinkType] 
    SET  [Status] = 'Deleted'
    ,[LastUpdateDate]=GETDATE()
    WHERE [Id]=@Id
GO

-------------------------------------------------------------

DROP PROCEDURE GetNoteTypes
GO
Create PROCEDURE GetNoteTypes AS
SELECT [Id]
      ,[Title]
      ,[CreationDate]
      ,[LastUpdateDate]
FROM [dbo].[NoteType]
WHERE Status <> 'Deleted'

GO
-----------------------------------------------------------

DROP PROCEDURE UpdateNoteType
GO
Create PROCEDURE UpdateNoteType
@Id INT,
@Title NVARCHAR(255)
AS

UPDATE [dbo].[NoteType] 
    SET  [Title] = @Title
    ,[LastUpdateDate]=GETDATE()
    WHERE [Id]=@Id
GO
----------------------------------------------------------

DROP PROCEDURE DeleteNoteType
GO
Create PROCEDURE DeleteNoteType
@Id INT
AS

UPDATE [dbo].[NoteType] 
    SET  [Status] = 'Deleted'
    ,[LastUpdateDate]=GETDATE()
    WHERE [Id]=@Id
GO

---------------------------------------------------------------

DROP PROCEDURE CreateNoteType
GO
Create PROCEDURE CreateNoteType
@Title NVARCHAR(255)
AS

Insert Into [dbo].[NoteType] ([Title],[CreationDate],[LastUpdateDate],[Status]) Values (@TItle, GETDATE(),GETDATE(),'Active')
GO

----------------------------------------------------

DROP PROCEDURE GetCommentTypes
GO
Create PROCEDURE GetCommentTypes AS
SELECT [Id]
      ,[Title]
      ,[CreationDate]
      ,[LastUpdateDate]
  FROM [dbo].[CommentType]
  WHERE Status <> 'Deleted'

  GO
-------------------------------------------------------

DROP PROCEDURE UpdateCommentType
GO
Create PROCEDURE UpdateCommentType
@Id INT,
@Title NVARCHAR(255)
AS

UPDATE [dbo].[CommentType] 
    SET  [Title] = @Title
    ,[LastUpdateDate]=GETDATE()
    WHERE [Id]=@Id
GO
---------------------------------------

DROP PROCEDURE CreateCommentType
GO
Create PROCEDURE CreateCommentType
@Title NVARCHAR(255)
AS

Insert Into [dbo].[CommentType] ([Title],[CreationDate],[LastUpdateDate],[Status]) Values (@TItle, GETDATE(),GETDATE(),'Active')
GO

---------------------------------------------

DROP PROCEDURE DeleteCommentType
GO
Create PROCEDURE DeleteCommentType
@Id INT
AS

UPDATE [dbo].[CommentType] 
    SET  [Status] = 'Deleted'
    ,[LastUpdateDate]=GETDATE()
    WHERE [Id]=@Id
GO

----------------------------------------------------

DROP PROCEDURE GetNotes
GO
Create PROCEDURE GetNotes

 @UpdateUserGuid NVARCHAR(255)

AS
SELECT 
        [Notes].Id ,
        [Notes].Title ,
        [Notes].Content ,
        [Notes].CreationDate ,
        [Notes].NoteType ,
        [Notes].ImageFile ,
        [Notes].ImageThumb ,
        [Notes].UpdateUserGuid ,
        [Notes].LastUpdateDate ,
        [Notes].Status ,
        [NoteType].Title as NoteTypeTitle

 FROM [dbo].[Notes] INNER JOIN [dbo].[NoteType]
 ON [dbo].[Notes].[NoteType] = [dbo].[NoteType].[Id]

 WHERE [Notes].Status <> 'Deleted' and UpdateUserGuid = @UpdateUserGuid
  GO


------------------------------------------------------------------------

DROP PROCEDURE GetComments
GO
Create PROCEDURE GetComments
@NoteId INT
AS

SELECT
      [dbo].[Comments].Id ,
      [dbo].[Comments].Content ,
      [dbo].[Comments].CreationDate ,
      [dbo].[Comments].NoteId,
      [dbo].[Comments].CommentType,
      [dbo].[Comments].UpdateUserGuid ,
      [dbo].[Comments].LastUpdateDate ,
      [dbo].[Comments].Status,
      [dbo].[CommentType].Title as CommentTypeTitle
 FROM [dbo].[Comments] INNER JOIN [dbo].[CommentType]
 ON [dbo].[Comments].[CommentType] = [dbo].[CommentType].[Id]

 WHERE [Comments].Status <> 'Deleted' and [Comments].[NoteId] = @NoteId
GO

---------------------------------------------------------------------------

DROP PROCEDURE GetLinks
GO
Create PROCEDURE GetLinks
@NoteId INT
AS

SELECT
        [dbo].[Links].Id,
        [dbo].[Links].Title ,
        [dbo].[Links].CreationDate,
        [dbo].[Links].NoteId,
        [dbo].[Links].LinkType ,
        [dbo].[Links].ExternalLink,
        [dbo].[Links].InternalLink,
        [dbo].[Links].UpdateUserGuid,
        [dbo].[Links].LastUpdateDate,
        [dbo].[Links].Status,
    [dbo].[LinkType].Title as LinkTypeTitle

 FROM [dbo].[Links] INNER JOIN [dbo].[LinkType]
 ON [dbo].[Links].[LinkType] = [dbo].[LinkType].[Id]

 WHERE [Links].Status <> 'Deleted' and [Links].[NoteId] = @NoteId
GO





----------------------------------------------------------------------------------

DROP PROCEDURE CreateNote
GO

Create PROCEDURE CreateNote

@Title NVARCHAR(255)
,@Content TEXT
,@NoteTypeId INT
,@ImageFile NVARCHAR(255)
,@ImageThumb NVARCHAR(255)
,@UserGuid NVARCHAR(255)

AS

INSERT INTO [dbo].[Notes]  (
      [Title]
      ,[Content]
      ,[CreationDate]
      ,[NoteType]
      ,[ImageFile]
      ,[ImageThumb]
      ,[UpdateUserGuid]
      ,[LastUpdateDate]
      ,[Status])
VALUES
(@Title,@Content,GETDATE(),@NoteTypeId,@ImageFile,@ImageThumb,@UserGuid,GETDATE(),'Active')

GO



----------------------------------


DROP PROCEDURE CreateComment
GO
Create PROCEDURE CreateComment

@NoteId INT,
@Content  TEXT ,
@CommentType INT,
@UserGuid NVARCHAR(255) 


AS

INSERT INTO [dbo].[Comments]  
      (     
      [dbo].[Comments].Content ,
      [dbo].[Comments].CreationDate ,
      [dbo].[Comments].NoteId,
      [dbo].[Comments].CommentType,
      [dbo].[Comments].UpdateUserGuid ,
      [dbo].[Comments].LastUpdateDate ,
      [dbo].[Comments].Status
      )
      VALUES
      (
       @Content ,
       GETDATE(),
       @NoteId ,
       @CommentType,
       @UserGuid,
       GETDATE(),
       'Active'
       )
    
GO
-------------------------------------------------


DROP PROCEDURE CreateLink
GO
Create PROCEDURE CreateLink
@NoteId INT,
@Title  NVARCHAR(255) ,
@ExternalLink NVARCHAR(255),
@InternalLink NVARCHAR(255),
@LinkType INT,
@UserGuid NVARCHAR(255)

AS

INSERT INTO [dbo].[Links]  
      (     
        [dbo].[Links].Title ,
        [dbo].[Links].CreationDate,
        [dbo].[Links].NoteId,
        [dbo].[Links].LinkType ,
        [dbo].[Links].ExternalLink,
        [dbo].[Links].InternalLink,
        [dbo].[Links].UpdateUserGuid,
        [dbo].[Links].LastUpdateDate,
        [dbo].[Links].Status
      )
      VALUES
      (
       @Title ,
       GETDATE(),
       @NoteId,
       @LinkType,
       @ExternalLink,
       @InternalLink,
       @UserGuid,
       GETDATE(),
       'Active'
       )
    
GO
-----------------------------------
DROP PROCEDURE UpdateNote
GO

Create PROCEDURE [dbo].[UpdateNote]
 @NoteId INT
,@Title NVARCHAR(255)
,@Content TEXT
,@NoteTypeId INT
,@ImageFile NVARCHAR(255)
,@ImageThumb NVARCHAR(255)
,@UpdateUserGuid NVARCHAR(255)


AS

UPDATE [dbo].[Notes]  SET
      [Title]= @Title
      ,[Content]= @Content
      ,[NoteType] = @NoteTypeId
      ,[ImageFile] = @ImageFile
      ,[ImageThumb]= @ImageThumb
      ,[LastUpdateDate] = GETDATE()
WHERE [Id] = @NoteId and [UpdateUserGuid] = @UpdateUserGuid

GO



-----------------------------------
DROP PROCEDURE UpdateComment
GO

Create PROCEDURE [dbo].[UpdateComment]

@CommentId INT,
@Content  TEXT ,
@CommentType INT


AS

UPDATE [dbo].[Comments]  
   SET   
      [dbo].[Comments].Content = @Content,
      [dbo].[Comments].CommentType = @CommentType,
      [dbo].[Comments].LastUpdateDate =GETDATE() 

      WHERE [Id]= @CommentId
GO

------------------------------------
DROP PROCEDURE UpdateLink
GO

Create PROCEDURE [dbo].[UpdateLink]
@LinkId INT,
@Title  NVARCHAR(255) ,
@ExternalLink NVARCHAR(255),
@InternalLink NVARCHAR(255),
@LinkType INT

AS
UPDATE [dbo].[Links]  
    SET   
        [dbo].[Links].Title = @Title  ,
        [dbo].[Links].LinkType = @LinkType,
        [dbo].[Links].ExternalLink = @ExternalLink,
        [dbo].[Links].InternalLink = @InternalLink,
        [dbo].[Links].LastUpdateDate = GETDATE()
 WHERE [Id] = @LinkId
    
GO

------------------------------------
