sp_configure 'show advanced options', 1
GO
RECONFIGURE
GO
sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO

sp_configure 'clr strict security', 0
GO
RECONFIGURE
GO

DROP ASSEMBLY SqlFunc

CREATE ASSEMBLY SqlFunc
FROM 'C:\Users\Ev\source\repos\Database1\Database1\obj\Debug\Database1.dll'

Drop Aggregate SqlAggregate1
GO

CREATE AGGREGATE SqlAggregate1(@duration int)
RETURNS INT
EXTERNAL NAME
SqlFunc.[SqlAggregate1]
GO

Drop PROCEDURE AlbumsByDuration
GO

CREATE PROCEDURE AlbumsByDuration(@duration int)
AS EXTERNAL NAME SqlFunc.StoredProcedures.AlbumsByDuration 
GO

exec AlbumsByDuration 54

Drop TRIGGER SqlTrigger1
GO

CREATE TRIGGER SqlTrigger1 ON Groups INSTEAD OF DELETE
AS EXTERNAL NAME SqlFunc.[Triggers].SqlTrigger1
GO

DELETE Groups
WHERE GroupId = 1
GO

DROP FUNCTION GetPow
GO

CREATE FUNCTION GetPow (@number int)
RETURNS INT
AS EXTERNAL NAME SqlFunc.[SqlServerUDF].GetPow 
GO

print(dbo.GetPow(20))

SELECT dbo.SqlAggregate1(Duration) AS Value
FROM Albums
GO

DROP TYPE dbo.groupElem
GO

CREATE TYPE dbo.groupElem  
EXTERNAL NAME SqlFunc.[SqlUserDefinedType1];
GO

DROP TABLE Test
GO

CREATE TABLE dbo.Test
( 
  id INT IDENTITY(1,1) NOT NULL, 
  g groupElem NULL
);
GO

INSERT INTO dbo.Test(g) VALUES('England,Portishead'); 

select *
from Test

CREATE FUNCTION getIdGroups()
RETURNS TABLE 
(
	Id int,
	Name NVARCHAR(30)
)
AS
EXTERNAL NAME
SqlFunc.UserDefinedFunctions.getIdGroups 
GO

SELECT * FROM dbo.getIdGroups()
GO