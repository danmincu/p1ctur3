
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 07/09/2013 23:56:37
-- Generated from EDMX file: C:\TFS\ShutterTale\SqlProvider\ShutterTaleDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ShutterTale];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MediaPreview]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Previews] DROP CONSTRAINT [FK_MediaPreview];
GO
IF OBJECT_ID(N'[dbo].[FK_Image_inherits_Media]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Media_Image] DROP CONSTRAINT [FK_Image_inherits_Media];
GO
IF OBJECT_ID(N'[dbo].[FK_Audio_inherits_Media]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Media_Audio] DROP CONSTRAINT [FK_Audio_inherits_Media];
GO
IF OBJECT_ID(N'[dbo].[FK_Video_inherits_Audio]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Media_Video] DROP CONSTRAINT [FK_Video_inherits_Audio];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Media]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Media];
GO
IF OBJECT_ID(N'[dbo].[Previews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Previews];
GO
IF OBJECT_ID(N'[dbo].[Media_Image]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Media_Image];
GO
IF OBJECT_ID(N'[dbo].[Media_Audio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Media_Audio];
GO
IF OBJECT_ID(N'[dbo].[Media_Video]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Media_Video];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Media'
CREATE TABLE [dbo].[Media] (
    [Id] uniqueidentifier  NOT NULL,
    [CaptureDateTime] datetime  NULL,
    [FileDateTime] datetime  NOT NULL,
    [ImportDateTime] datetime  NOT NULL,
    [Location] geography  NULL,
    [PixelX] int  NOT NULL,
    [PixelY] int  NOT NULL,
    [Dpi] float  NOT NULL,
    [ContentType] nvarchar(max)  NOT NULL,
    [Make] nvarchar(max)  NULL,
    [Model] nvarchar(max)  NULL,
    [SoftwareVersion] nvarchar(max)  NULL,
    [Origin] nvarchar(max)  NOT NULL,
    [Size] int  NOT NULL,
    [Quadkey18] nvarchar(18)  NOT NULL
);
GO

-- Creating table 'Previews'
CREATE TABLE [dbo].[Previews] (
    [Id] uniqueidentifier  NOT NULL,
    [Level0] varbinary(max)  NULL,
    [Level1] varbinary(max)  NULL,
    [Level2] varbinary(max)  NULL,
    [Level3] varbinary(max)  NULL,
    [PreviewType] nvarchar(max)  NOT NULL,
    [Medium_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Contents'
CREATE TABLE [dbo].[Contents] (
    [Id] uniqueidentifier  NOT NULL,
    [Provider] nvarchar(max)  NOT NULL,
    [ProviderKey] nvarchar(max)  NOT NULL,
    [Medium_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Media_Image'
CREATE TABLE [dbo].[Media_Image] (
    [Orientation] tinyint  NULL,
    [YCbCrPositioning] tinyint  NULL,
    [ExposureTime] float  NULL,
    [FNumber] float  NULL,
    [ExposureProgram] tinyint  NULL,
    [ISOSpeedRatings] smallint  NULL,
    [ShutterSpeedValue] float  NULL,
    [ApertureValue] float  NULL,
    [MeteringMode] tinyint  NULL,
    [Flash] tinyint  NULL,
    [FocalLength] float  NULL,
    [FlashpixVersion] nvarchar(max)  NULL,
    [ColorSpace] tinyint  NULL,
    [SensingMethod] tinyint  NULL,
    [ExposureMode] tinyint  NULL,
    [WhiteBalance] tinyint  NULL,
    [SceneCaptureType] tinyint  NULL,
    [Sharpness] tinyint  NULL,
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Media_Audio'
CREATE TABLE [dbo].[Media_Audio] (
    [AudioChannels] tinyint  NOT NULL,
    [Duration] nvarchar(max)  NOT NULL,
    [AudioCodec] nvarchar(max)  NOT NULL,
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Media_Video'
CREATE TABLE [dbo].[Media_Video] (
    [VideoChannels] tinyint  NOT NULL,
    [VideoCodec] nvarchar(max)  NOT NULL,
    [Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Media'
ALTER TABLE [dbo].[Media]
ADD CONSTRAINT [PK_Media]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Previews'
ALTER TABLE [dbo].[Previews]
ADD CONSTRAINT [PK_Previews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contents'
ALTER TABLE [dbo].[Contents]
ADD CONSTRAINT [PK_Contents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Media_Image'
ALTER TABLE [dbo].[Media_Image]
ADD CONSTRAINT [PK_Media_Image]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Media_Audio'
ALTER TABLE [dbo].[Media_Audio]
ADD CONSTRAINT [PK_Media_Audio]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Media_Video'
ALTER TABLE [dbo].[Media_Video]
ADD CONSTRAINT [PK_Media_Video]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Medium_Id] in table 'Previews'
ALTER TABLE [dbo].[Previews]
ADD CONSTRAINT [FK_MediaPreview]
    FOREIGN KEY ([Medium_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaPreview'
CREATE INDEX [IX_FK_MediaPreview]
ON [dbo].[Previews]
    ([Medium_Id]);
GO

-- Creating foreign key on [Medium_Id] in table 'Contents'
ALTER TABLE [dbo].[Contents]
ADD CONSTRAINT [FK_MediaContent]
    FOREIGN KEY ([Medium_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaContent'
CREATE INDEX [IX_FK_MediaContent]
ON [dbo].[Contents]
    ([Medium_Id]);
GO

-- Creating foreign key on [Id] in table 'Media_Image'
ALTER TABLE [dbo].[Media_Image]
ADD CONSTRAINT [FK_Image_inherits_Media]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Media_Audio'
ALTER TABLE [dbo].[Media_Audio]
ADD CONSTRAINT [FK_Audio_inherits_Media]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Media_Video'
ALTER TABLE [dbo].[Media_Video]
ADD CONSTRAINT [FK_Video_inherits_Audio]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Media_Audio]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------