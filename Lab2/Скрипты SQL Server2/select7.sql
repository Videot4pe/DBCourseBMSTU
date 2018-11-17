SELECT AVG(MembersOnAlbums) AS 'AVG', SUM(MembersOnAlbums)/COUNT(GroupID)AS 'Real AVG'
FROM 
(
SELECT GroupID, SUM(NumberOfMembers*AlbumsAmount) AS MembersOnAlbums
FROM Groups
GROUP BY GroupID
) AS GroupAlbumMembers