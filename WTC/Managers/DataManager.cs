using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTC.Managers
{
    internal class DataManager
    {
        public static DataManager instance;

        public static DataManager GetInstance()
        {
            if(instance==null)
                instance = new DataManager();

            return instance;
        }
        

    }
}
