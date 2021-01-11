
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


DROP TABLE Notes
CREATE TABLE Notes
(
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(50),
        Content TEXT,
        CreationDate DATETIME,
        NoteType INT,
        ImageFile NVARCHAR(255),
        ImageThumb NVARCHAR(255),
        UpdateUserGuid NVARCHAR(255),
        LastUpdateDate DATETIME,
        Status nvarchar(10)
)

DROP TABLE Comments
CREATE TABLE Comments
(
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Content TEXT,
        CreationDate DATETIME,
        NoteId INT,
        CommentType INT,
        UpdateUserGuid NVARCHAR(255),
        LastUpdateDate DATETIME,
        Status nvarchar(10)
)

DROP TABLE Links
CREATE TABLE Links
(
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(256),
        CreationDate DATETIME,
        NoteId INT,
        LinkType INT,
        ExternalLink NVARCHAR(255),
        InternalLink NVARCHAR(255),
        UpdateUserGuid NVARCHAR(255),
        LastUpdateDate DATETIME,
        Status nvarchar(10)
 )

DROP TABLE LinkType
CREATE TABLE LinkType
(
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(255),
        CreationDate DATETIME,
        LastUpdateDate DATETIME,
        Status nvarchar(10)

)

DROP TABLE CommentType
CREATE TABLE CommentType
(
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(256),
        CreationDate DATETIME,
        LastUpdateDate DATETIME,
        Status nvarchar(10)
)

DROP TABLE NoteType
CREATE TABLE NoteType
(
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(255),
        CreationDate DATETIME,
        LastUpdateDate DATETIME,
        Status nvarchar(10)
)

GO