using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections;

public partial class UserDefinedFunctions
{
    private class Groups
    {
        public int id;
        public string name;

        public Groups(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    [Microsoft.SqlServer.Server.SqlFunction(
       DataAccess = DataAccessKind.Read,
       FillRowMethodName = "getGroups",
       TableDefinition = "Id int, Name nvarchar(30)"
       )]
    public static IEnumerable getIdGroups()
    {
        ArrayList groups = new ArrayList();

        using (SqlConnection c = new SqlConnection("context connection=true"))
        {
            c.Open();
            using (SqlCommand cmd = new SqlCommand(
                "select GroupId, Name " +
                "from Groups " +
                "where GroupId > 3", c
                ))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        groups.Add(new Groups(
                            reader.GetInt32(0),
                            reader.GetString(1)));
                    }
                }
            }
        }

        return groups;
    }

    public static void getGroups(object obj, out int id, out string name)
    {
        Groups g = (Groups)obj;
        id = g.id;
        name = g.name;
    }
}
