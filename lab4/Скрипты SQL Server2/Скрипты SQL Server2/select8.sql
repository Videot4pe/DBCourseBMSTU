select distinct topOfTheYear,
	(
	select avg(a.TopOfTheYear)
	from Albums a
	) as avgTop
from Albums