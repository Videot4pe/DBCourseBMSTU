USE MusicDB

SELECT DISTINCT Name, Title
FROM Groups JOIN Albums ON Groups.GroupID = Albums.GroupID
WHERE Name LIKE '%head%' 