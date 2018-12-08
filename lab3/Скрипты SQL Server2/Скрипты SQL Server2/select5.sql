SELECT GroupID, Name
FROM Groups
WHERE EXISTS
(
SELECT Groups.GroupID
FROM Groups LEFT OUTER JOIN Albums
ON Groups.GroupID = Albums.GroupID
WHERE Albums.Duration > 20
)