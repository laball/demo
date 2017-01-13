
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/12/2017 16:13:17
-- Generated from EDMX file: D:\libo\code\git\demo\EFDemo\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [tese];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Class__TeacherID__60A75C0F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Class] DROP CONSTRAINT [FK__Class__TeacherID__60A75C0F];
GO
IF OBJECT_ID(N'[dbo].[FK__SudentCla__Suden__6477ECF3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SudentClass] DROP CONSTRAINT [FK__SudentCla__Suden__6477ECF3];
GO
IF OBJECT_ID(N'[dbo].[FK__SudentCla__Teach__6383C8BA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SudentClass] DROP CONSTRAINT [FK__SudentCla__Teach__6383C8BA];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Class]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Class];
GO
IF OBJECT_ID(N'[dbo].[Student]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Student];
GO
IF OBJECT_ID(N'[dbo].[SudentClass]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SudentClass];
GO
IF OBJECT_ID(N'[dbo].[Teacher]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teacher];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Class'
CREATE TABLE [dbo].[Class] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [TeacherID] int  NULL
);
GO

-- Creating table 'Student'
CREATE TABLE [dbo].[Student] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [ClassLevel] int  NULL,
    [Age] int  NULL,
    [BirthDay] datetime  NULL
);
GO

-- Creating table 'SudentClass'
CREATE TABLE [dbo].[SudentClass] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TeacherID] int  NULL,
    [SudentID] int  NULL
);
GO

-- Creating table 'Teacher'
CREATE TABLE [dbo].[Teacher] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Class'
ALTER TABLE [dbo].[Class]
ADD CONSTRAINT [PK_Class]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Student'
ALTER TABLE [dbo].[Student]
ADD CONSTRAINT [PK_Student]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SudentClass'
ALTER TABLE [dbo].[SudentClass]
ADD CONSTRAINT [PK_SudentClass]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Teacher'
ALTER TABLE [dbo].[Teacher]
ADD CONSTRAINT [PK_Teacher]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TeacherID] in table 'Class'
ALTER TABLE [dbo].[Class]
ADD CONSTRAINT [FK__Class__TeacherID__60A75C0F]
    FOREIGN KEY ([TeacherID])
    REFERENCES [dbo].[Teacher]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Class__TeacherID__60A75C0F'
CREATE INDEX [IX_FK__Class__TeacherID__60A75C0F]
ON [dbo].[Class]
    ([TeacherID]);
GO

-- Creating foreign key on [SudentID] in table 'SudentClass'
ALTER TABLE [dbo].[SudentClass]
ADD CONSTRAINT [FK__SudentCla__Suden__6477ECF3]
    FOREIGN KEY ([SudentID])
    REFERENCES [dbo].[Student]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__SudentCla__Suden__6477ECF3'
CREATE INDEX [IX_FK__SudentCla__Suden__6477ECF3]
ON [dbo].[SudentClass]
    ([SudentID]);
GO

-- Creating foreign key on [TeacherID] in table 'SudentClass'
ALTER TABLE [dbo].[SudentClass]
ADD CONSTRAINT [FK__SudentCla__Teach__6383C8BA]
    FOREIGN KEY ([TeacherID])
    REFERENCES [dbo].[Teacher]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__SudentCla__Teach__6383C8BA'
CREATE INDEX [IX_FK__SudentCla__Teach__6383C8BA]
ON [dbo].[SudentClass]
    ([TeacherID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------