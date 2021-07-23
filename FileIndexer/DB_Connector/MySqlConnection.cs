using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mk.DBConnector;


namespace DB_Connector
{
    public class MySqlConnection : IConnection
    {

        /*static Connection()
        {
            Connection.Adapter = new DBAdapter(DatabaseType.MySql, Instance.NewInstance, "Unnamed-1", 3306, "Database"; "UserID", "Password", "logfile");


        private string _SqlAdapter;
        */

        public bool IsConnected { get; set; }

        private DBAdapter _SqlAdapter;

        public void Connect()
        {
            if (!IsConnected)
            {
                try
                {
                    _SqlAdapter = new DBAdapter(DatabaseType.MySql, Instance.Singleton, "localhost", 3306, "test", "defaultuser", "password", "dbconnector.log");
                    _SqlAdapter.Adapter.LogFile = true;
                    IsConnected = true;
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.Print("Connect failed\n");
                }
            }
            else
            {
                System.Diagnostics.Debug.Print("Already connected...\n");
            }
        }

        public DBAdapter GetAdapter()
        {
            if (IsConnected && _SqlAdapter != null)
            {
                return _SqlAdapter;
            }
            return null;
        }
    }



}

