with emp(GroupID, Level)
as
(
	select Groups.GroupID, 0 AS 'Level'
	from Groups
	where Groups.GroupID < 3
	union all
	select Groups.GroupID, Level + 1
	from emp e join Groups Groups on e.GroupID = Groups.GroupID
)
select GroupID, Level
from emp;