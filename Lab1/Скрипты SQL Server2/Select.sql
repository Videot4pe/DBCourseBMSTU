USE MusicDB

SELECT
	Musicians.Name,
	Musicians.Surname,
	Groups.Name

FROM Musicians
JOIN MusiciansAndGroups ON Musicians.MusicianID = MusiciansAndGroups.MusicianID
JOIN Groups ON Groups.GroupID = MusiciansAndGroups.GroupID
WHERE (AlbumsAmount > 3 )
ORDER BY Musicians.Name, Musicians.Surname