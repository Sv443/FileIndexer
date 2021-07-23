using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CommonTypes1;

namespace DB_Connector
{
    class TableFiles
    {


    }


    public override void Save(IEntity entity)
    {
        File obj = (File)entity;
        string query = string.Format("INSERT INTO {0} VALUES(0, \"{1}\", \"{2}\", {3}, \"{4}\")", TableName, obj.FahrgestellNR, obj.Kennzeichen, obj.Leistung, obj.Typ);
        _Connection.GetAdapter().Adapter.ExecuteSQL(query);
    }
}
