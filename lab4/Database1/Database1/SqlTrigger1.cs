using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

public partial class Triggers
{        
    // Введите существующую таблицу или представление для целевого объекта и раскомментируйте строку атрибута.
    // [Microsoft.SqlServer.Server.SqlTrigger (Name="SqlTrigger1", Target="Table1", Event="FOR UPDATE")]
    public static void SqlTrigger1 ()
    {
	    SqlContext.Pipe.Send("Trigger. Just Trigger. And it works. ");
    }
}

