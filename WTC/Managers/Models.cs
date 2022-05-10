using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTC.Managers
{
    public class Class
    {
        public int id;
        public string name;
        public List<Attribute> description;

        public Class(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    public class Attribute
    {
        public int id;
        public string name;
        public int type;
        public string values;

        public Attribute(int id, string name, int type)
        {
            this.id=id;
            this.name=name;
            this.type=type;
        }
    }

}
