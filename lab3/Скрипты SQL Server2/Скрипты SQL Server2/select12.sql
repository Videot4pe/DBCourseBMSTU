select g.AlbumsAmount
from Groups g join
	(
	select top 1 AlbumsAmount, avg(AlbumsAmount) as avgA
	from Groups g join Albums a on g.GroupID = a.GroupID
	group by AlbumsAmount
	order by avgA desc
	) as s on s.AlbumsAmount = g.AlbumsAmount
union
select g.NumberOfMembers
from Groups g join
	(
	select top 1 NumberOfMembers, avg(NumberOfMembers) as avgM
	from Groups g join Albums a on g.GroupID = a.GroupID
	group by NumberOfMembers
	order by avgM desc
	) as d on d.NumberOfMembers = g.NumberOfMembers