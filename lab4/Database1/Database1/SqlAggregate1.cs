using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.UserDefined, MaxByteSize = 8000)]
public struct SqlAggregate1 : IBinarySerialize
{
    private int avg;
    private int amount;
    public void Init()
    {
        amount = 0;
        avg = 0;
    }

    public void Accumulate(int Value)
    {
        amount += 1;
        avg += Value;
    }

    public void Merge (SqlAggregate1 Group)
    {
        amount += Group.amount;
        avg += Group.avg;
    }

    public int Terminate ()
    {
        return avg/amount;
    }

    public void Read(System.IO.BinaryReader r)
    {
        amount = r.ReadInt32();
        avg = r.ReadInt32();
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write((avg/amount).ToString());
    }
}
