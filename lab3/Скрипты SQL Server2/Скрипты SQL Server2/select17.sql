insert Groups (GroupID, Name, DateOfCreation, NumberOfMembers, AlbumsAmount, Del, Ins)
select (
	select GroupID 
	from Albums
	where Duration > 30
), 'Portishead', '1991-01-02', 4, 3, 0, 1