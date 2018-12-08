select TopOfTheYear,
	case Genre
		when 'Rock' then 'Cool'
		else 'So boring'
	end as 'Genre'
from Albums