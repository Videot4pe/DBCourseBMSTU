select Duration, avg(NumberOfMembers) as 'avgMembers'
from Albums a join Groups g on a.GroupID = g.GroupID
group by Duration
having avg(NumberOfMembers) <
	(
	select avg(NumberOfMembers)
	from Groups
	)