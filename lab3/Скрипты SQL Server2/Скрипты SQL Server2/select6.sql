SELECT GroupID, Name, AlbumsAmount
FROM Groups
WHERE AlbumsAmount > ALL
(
SELECT AlbumsAmount
FROM Groups
WHERE GroupID = 2
)