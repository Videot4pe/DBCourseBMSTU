select Duration, 
	min(a.Duration) as 'min',
	avg(a.Duration) as 'avg', 
	max(a.Duration) as 'max'
from Albums a
group by Duration