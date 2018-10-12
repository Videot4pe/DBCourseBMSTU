USE MusicDB
GO

BULK INSERT Musicians
FROM 'C:\Users\Ev\Documents\musicians.csv'
WITH
    (
		CHECK_CONSTRAINTS,
		DATAFILETYPE = 'char',
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '0x0A'
    )
GO

BULK INSERT Groups
FROM 'C:\Users\Ev\Documents\group.csv'
WITH
(
	CHECK_CONSTRAINTS,
	DATAFILETYPE = 'char',
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0A'
)
GO


BULK INSERT Albums
FROM 'C:\Users\Ev\Documents\albums.csv'
WITH
(
	CHECK_CONSTRAINTS,
	DATAFILETYPE = 'char',
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0A'
)
GO


BULK INSERT MusiciansAndGroups
FROM 'C:\Users\Ev\Documents\musiciansAndGroups.csv'
WITH
(
	CHECK_CONSTRAINTS,
	DATAFILETYPE = 'char',
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0A'
)
GO