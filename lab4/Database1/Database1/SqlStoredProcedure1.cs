using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void AlbumsByDuration(int duration)
    {
        using (SqlConnection c = new SqlConnection("context connection = true"))
        {
            SqlCommand cmd = new SqlCommand(
                "select AlbumId " +
                "from Albums " +
                "where Duration = @duration", c);
            SqlParameter pduration = new SqlParameter("@duration", SqlDbType.Int);
            pduration.Value = duration;
            cmd.Parameters.Add(pduration);
            c.Open();
            SqlContext.Pipe.ExecuteAndSend(cmd);
            c.Close();
        }
    }
}
