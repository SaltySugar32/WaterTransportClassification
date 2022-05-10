using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTC.Managers
{
    public class ClassModel
    {
        public int id;
        public string name;
        public List<AttributeModel> description;

        public ClassModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    public class AttributeModel
    {
        public int id;
        public string name;
        public int type;
        public string values;

        public AttributeModel(int id, string name, int type)
        {
            this.id=id;
            this.name=name;
            this.type=type;
        }
    }

}
