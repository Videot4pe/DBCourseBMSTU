USE MusicDB

SELECT GroupID, DateOfCreation
FROM Groups
WHERE GroupID IN (
SELECT GroupID
FROM MusiciansAndGroups
WHERE MusicianID > 98
)