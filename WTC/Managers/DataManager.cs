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
        public List<ClassModel> check_description_integrity()
        {
            List<ClassModel> empty_classes = new List<ClassModel> ();

            DatabaseManager db = new DatabaseManager();
            List<ClassModel> classes = db.get_classes();
            foreach (ClassModel cls in classes)
            {
                List<AttributeModel> description = db.get_description(cls.id);
                if (description.Count == 0)
                    empty_classes.Add(cls);
            }
            return empty_classes;
        }

        public List<AttributeModel> check_attribute_values()
        {
            List<AttributeModel> empty_attribute_values = new List<AttributeModel> ();

            DatabaseManager db = new DatabaseManager();
            List<AttributeModel> attributes = db.get_attributes();
            foreach (AttributeModel attr in attributes)
            {
                if (db.get_value(attr.id) == null)
                    empty_attribute_values.Add(attr);
            }
            return empty_attribute_values;
        }

        public List<AttributeModel> check_attribute_class_values(int class_id)
        {
            List <AttributeModel> empty_attribute_classvalues = new List<AttributeModel> () ;

            DatabaseManager db = new DatabaseManager();
            List<AttributeModel> description = db.get_description(class_id);
            foreach (AttributeModel attr in description)
            {
                if (db.get_classvalue(class_id, attr.id) == null)
                    empty_attribute_classvalues.Add(attr);
            }
            return empty_attribute_classvalues;
        }

    }
}
