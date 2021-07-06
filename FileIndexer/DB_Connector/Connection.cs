using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Connector
{
    public interface IConnection
    {
        void Connect();
        bool IsConnected { get; set; }
        DBAdapter GetAdapter();
    }
}
