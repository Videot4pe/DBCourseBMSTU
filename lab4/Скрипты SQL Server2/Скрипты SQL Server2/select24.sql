select g.GroupID, Name, 
	avg(a.Duration) over (partition by g.GroupID) as avgDuration,
	min(a.Duration) over (partition by g.GroupID) as minDuration,
	max(a.Duration) over (partition by g.GroupID) as maxDuration,
	row_number() over (partition by g.GroupID Order by (select NULL)) as d
from Groups g join Albums a on g.GroupID = a.GroupID