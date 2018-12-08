create function getcountalbums(@GroupID int)
returns int
as
begin
	declare @count int;
	select @count = count(a.AlbumID)
	from Groups g join Albums a on a.GroupID = g.GroupID
	where g.GroupID = @GroupID;
	return @count;
end;

create function getalbums()
returns table
as
return 
(
	select AlbumID, Title, DateOfRecord
	from Albums
	where AlbumID > 10
)

create function getgroups(@NumberOfMembers int)
returns @group table
(
	Name  nvarchar(50),
	DateOfCreation date,
	NumberOfMembers int
)
as
begin
	declare @MaxId int;
	select @MaxId = max(GroupID)
	from Groups;
	declare @i int = 1;
	while @i < @MaxId
	begin
		insert @group
			select Name, DateOfCreation, NumberOfMembers
			from Groups
			where NumberOfMembers = @NumberOfMembers and GroupID = @i
		set @i += 1
	end;
	return 
end;

create function getmusicianscount(@current int)
returns int
as
begin
	declare @max int;
	select @max = MAX(GroupID)
	from Groups;
	declare @count int;
	select @count = NumberOfMembers
	from Groups;
	if @current != @max
		set @count += dbo.getmusicianscount(@current+1);
	return @count;
end;

create procedure groupsalbumsamount @amount int
as
select Name, AlbumsAmount
from Groups
where AlbumsAmount > @amount;

create procedure printgroupsbymembers (@id int, @amount int)
as
begin
	declare @name varchar(50);
	declare @title varchar(50);
	declare @members int;
	declare @max int;
	select @max = MAX(GroupID)
	from Groups;
	declare @next int;
	set @next = @id + 1;
	select @name = Name, @title = Title, @members = NumberOfMembers
	from Groups g join Albums a on a.GroupID = g.GroupID
	where g.GroupID = @id;
	if @members > @amount
	begin
		print @name;
		print @title;
	end;
	if @id < @max
		exec printgroupsbymembers @next, @amount;
end;

create procedure printgroups @id int
as
begin
	declare @name  nvarchar(50), @albums int
	declare c cursor for
		select Name, AlbumsAmount
		from Groups
		Where GroupID > @id;
	open c;
	fetch c into @name, @albums;
	while @@FETCH_STATUS = 0
	begin
		print '   ' + @name + '   ' +str(@albums) + '   ';
		fetch c into @name, @albums;
	end;
	close c;
	deallocate c;
end;

create procedure metaaccess @schema varchar(100)
as
select * from INFORMATION_SCHEMA.TABLES 
where TABLE_SCHEMA = @schema

drop trigger insertTrigger;
go

create trigger insertTrigger on Groups after insert
as
begin
	select * from inserted;
	Update Groups
	set Ins = 1
	where GroupID in
	(select GroupID from inserted)
end;

drop trigger deleteTrigger;
go

create trigger deleteTrigger on Groups instead of delete
as
begin
	Update Groups
	set Del = 1
	where GroupID in
	(select GroupID from deleted)
end;
go

м€гкое удаление