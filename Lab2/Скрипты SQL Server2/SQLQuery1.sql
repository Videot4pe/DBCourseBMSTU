print(dbo.getcountalbums(3));

select * from getalbums()
select * from getgroups(2)
select dbo.getmusicianscount(0)

exec printgroupsbymembers 1, 2

exec printgroups 2

exec groupsalbumsamount 3
exec metaaccess 'dbo'


insert Groups (GroupID, Name, DateOfCreation, NumberOfMembers, AlbumsAmount, Del, Ins) values (20, 'Radiohead????', '23.01.1999', 5, 9, 0, 0)

delete from Groups where GroupID = 1

logfunc;