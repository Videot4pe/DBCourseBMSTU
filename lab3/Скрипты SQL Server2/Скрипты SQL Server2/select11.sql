select Duration,
	avg(TopOfTheYear) as AvgTop
into #table
from Albums
group by Duration