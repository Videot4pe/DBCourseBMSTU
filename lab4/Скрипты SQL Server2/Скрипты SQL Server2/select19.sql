update Albums
set Duration = 
	(
		select avg(Duration)
		from Albums
		where TopOfTheYear > 25
	)
where GroupID > 2