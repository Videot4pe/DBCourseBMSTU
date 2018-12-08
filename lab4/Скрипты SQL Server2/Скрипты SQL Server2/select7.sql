SELECT AVG(MembersOnAlbums) AS 'AVG'
FROM 
(
SELECT SUM(NumberOfMembers*AlbumsAmount) AS MembersOnAlbums
FROM Groups
GROUP BY GroupID
) AS GroupAlbumMembers