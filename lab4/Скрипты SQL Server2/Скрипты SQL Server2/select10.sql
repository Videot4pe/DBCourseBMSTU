select Title,
	case
		when Duration > 40 then 'lp'
		else 'ep'
	end as 'Duration'
from Albums