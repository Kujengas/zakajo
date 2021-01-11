DECLARE @RC int
DECLARE @Title nvarchar(255)

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[CreateLinkType] 
   @Title = "External WebLink"

   EXECUTE @RC = [dbo].[CreateLinkType] 
   @Title = "Internal Note"
GO
-----------------------------------------------------------------------------

DECLARE @RC int
DECLARE @Title nvarchar(255)

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[CreateCommentType] 
   @Title = "Update"

EXECUTE @RC = [dbo].[CreateCommentType] 
   @Title = "Follow Up"
GO

------------------------------------------------------------------
DECLARE @RC int
DECLARE @Title nvarchar(255)



EXECUTE @RC = [dbo].[CreateNoteType] 
  @Title = "Journal"


EXECUTE @RC = [dbo].[CreateNoteType] 
   @Title = "Dream Journal"


EXECUTE @RC = [dbo].[CreateNoteType] 
   @Title = "General Notes"


EXECUTE @RC = [dbo].[CreateNoteType] 
   @Title = "Project Ideas"
GO

------------------------------------------------
--test of entry Proc

DECLARE @RC int
DECLARE @Title nvarchar(255)
DECLARE @Content NVARCHAR(MAX)
DECLARE @NoteTypeId int
DECLARE @ImageFile nvarchar(255)
DECLARE @ImageThumb nvarchar(255)
DECLARE @UserGuid nvarchar(255)

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[CreateNote] 
   @Title = 'Test Note 5'
  ,@Content= 'This is a fifth test of the journaling app data'
  ,@NoteTypeId = 1
  ,@ImageFile = ''
  ,@ImageThumb = ''
  ,@UserGuid = '22c9e6dd-2b43-479f-a3a1-0f465e81aed3'


EXECUTE [dbo].[GetNotes] 
   @UpdateUserGuid = '22c9e6dd-2b43-479f-a3a1-0f465e81aed3'

GO
