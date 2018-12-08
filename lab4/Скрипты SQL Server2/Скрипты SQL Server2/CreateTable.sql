USE MusicDB
GO

CREATE TABLE Groups
	(GroupID int PRIMARY KEY IDENTITY(1, 1),
	Name  nvarchar(50) NOT NULL,
	DateOfCreation date NOT NULL,
	NumberOfMembers int NOT NULL,
	AlbumsAmount int NOT NULL)
GO

CREATE TABLE Albums
	(AlbumID int PRIMARY KEY IDENTITY(1, 1),
	Title nvarchar(50) NOT NULL,
	DateOfRecord date NOT NULL,
	GroupID int NOT NULL,
	Genre nvarchar(50) NOT NULL,
	Country nvarchar(50) NOT NULL,
	Duration int NOT NULL,
	TopOfTheYear int NOT NULL)
GO

CREATE TABLE Musicians
	(MusicianID int PRIMARY KEY IDENTITY(1, 1),
	Name  nvarchar(50) NOT NULL,
	Surname nvarchar(50) NOT NULL,
	BirthDate date NOT NULL,
	Instrument nvarchar(50) NOT NULL)
GO

-- RELATION TABLES
CREATE TABLE MusiciansAndGroups
	(GroupID int NOT NULL,
	 MusicianID int NOT NULL)
GO