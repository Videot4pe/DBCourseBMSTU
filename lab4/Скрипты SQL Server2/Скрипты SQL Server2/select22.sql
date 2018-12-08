with CTE(avgDuration)
as
(
	select avg(Duration)
	from Albums a join Groups g on a.GroupID = g.GroupID
)
select TopOfTheYear
from Albums a join Groups g on a.GroupID = g.GroupID
where TopOfTheYear < all (select avgDuration from CTE)