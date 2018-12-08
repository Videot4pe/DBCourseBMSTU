delete from Groups
where GroupID in
(
	select GroupID 
	from Albums
	where TopOfTheYear = 77
)