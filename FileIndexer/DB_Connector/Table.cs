using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DB_Connector
{
    public abstract class Table<T>
    {
        public Table()
        {
            Initialize();
        }



        public List<T> GetFiles()
        {
            if (_Connection != null)
            {
                if (_LoadedData == null)
                {
                    LoadObjects(out _LoadedData);
                }
            }
        }


        protected abstract void Initialize();
        protected IConnection _Connection;
        protected List<T> _LoadedData = null;
        protected abstract void LoadObjects();


    }
}
