select distinct NumberOfMembers
from Groups
where NumberOfMembers < 
	(
	select TopOfTheYear
	from Groups g join Albums a on g.GroupID = a.GroupID
	group by TopOfTheYear
	having avg(TopOfTheYear) =
		(
		select max(avgTop)
		from
			(
			select avg(a.TopOfTheYear) as avgTop
			from Groups g join Albums a on g.GroupID = a.GroupID
			group by TopOfTheYear
			) as t
		)
	)