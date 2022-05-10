using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTC.Managers
{
    internal class DatabaseManager
    {
        public SQLiteConnection con;

        public DatabaseManager()
        {
            string db = "../../../Database/database.sqlite3";
            con = new SQLiteConnection("Data Source=" + db);
            if (!File.Exists(db))
                SQLiteConnection.CreateFile(db);
        }

        public void openCon()
        {
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
        }

        public void closeCon()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }

        public void add_class(string name)
        {
            openCon();
            string query = "INSERT INTO classes ('name') VALUES (@name)";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_class(string name)
        {
            openCon();
            string query = "DELETE FROM classes WHERE name='" + name + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void add_attribute(string name, int type)
        {
            openCon();
            string query = "INSERT INTO attributes ('name', 'type') VALUES (@name, @type)";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_attribute(string name)
        {
            openCon();
            string query = "DELETE FROM attributes WHERE name='" + name + "'"; 
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void add_description(int class_id, int attr_id)
        {
            openCon();
            string query = "INSERT INTO description ('class_id', 'attribute_id') VALUES (@class_id, @attribute_id)";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            cmd.Parameters.AddWithValue("@class_id", class_id);
            cmd.Parameters.AddWithValue("@attribute_id", attr_id);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_description(int class_id, int attr_id)
        {
            openCon();
            string query = "DELETE FROM description WHERE class_id='" + class_id + "' AND attribute_id='" + attr_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_description_by_class(int class_id)
        {
            openCon();
            string query = "DELETE FROM description WHERE class_id='" + class_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_description_by_attribute(int attr_id)
        {
            openCon();
            string query = "DELETE FROM description WHERE attribute_id='" + attr_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void add_values(int attr_id, string values)
        {
            openCon();
            string query = "INSERT INTO generalvalues ('attribute_id', 'possible_values') VALUES (@attribute_id, @values)";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            cmd.Parameters.AddWithValue("@attribute_id", attr_id);
            cmd.Parameters.AddWithValue("@values", values);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_values(int attr_id, string values)
        {
            openCon();
            string query = "DELETE FROM generalvalues WHERE attribute_id='" + attr_id + "' AND possible_values='" + values + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_values_by_attribute(int attr_id)
        {
            openCon();
            string query = "DELETE FROM generalvalues WHERE attribute_id='" + attr_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void add_classvalues(int class_id, int attr_id, string values)
        {
            openCon();
            string query = "INSERT INTO classvalues ('class_id', 'attribute_id', 'possible_values') VALUES (@class_id, @attribute_id, @values)";
            SQLiteCommand cmd = new SQLiteCommand(query, con);

            cmd.Parameters.AddWithValue("@class_id", class_id);
            cmd.Parameters.AddWithValue("@attribute_id", attr_id);
            cmd.Parameters.AddWithValue("@values", values);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_classvalues(int class_id, int attr_id, string values)
        {
            openCon();
            string query = "DELETE FROM classvalues WHERE class_id='" + class_id + "' AND attribute_id='" + attr_id + "' AND possible_values='" + values + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_classvalues_by_class(int class_id)
        {
            openCon();
            string query = "DELETE FROM classvalues WHERE class_id='" + class_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public void remove_classvalues_by_attribute(int attr_id)
        {
            openCon();
            string query = "DELETE FROM classvalues WHERE attribute_id='" + attr_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            closeCon();
        }

        public ClassModel get_class(int class_id)
        {
            openCon();
            ClassModel wtclass = null;
            string query = "SELECT * FROM classes WHERE id='" + class_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                int id = Convert.ToInt32(reader["id"]);
                string name = (string)reader["name"];

                wtclass = new ClassModel(id, name);
            }
            reader.Close();
            closeCon();
            return wtclass;
        }

        public int search_class(string name)
        {
            openCon();
            int id = -1;
            string query = "SELECT * FROM classes WHERE name='" + name + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                id = Convert.ToInt32(reader["id"]);
            }
            reader.Close();
            closeCon();
            return id;
        }

        public List<ClassModel> get_classes()
        {
            List<ClassModel> classes = new List<ClassModel>();
            openCon();
            string query = "SELECT * FROM classes";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = (string)reader["name"];
                    ClassModel temp = new ClassModel(id, name);
                    classes.Add(temp);
                }
            }
            reader.Close();
            closeCon();
            return classes;
        }

        public AttributeModel get_attribute(int attr_id)
        {
            openCon();
            AttributeModel attribute = null;
            string query = "SELECT * FROM attributes WHERE id='" + attr_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();

                int id = Convert.ToInt32(reader["id"]);
                string name = (string)reader["name"];
                int type = Convert.ToInt32(reader["type"]);

                attribute = new AttributeModel(id, name, type);
            }
            reader.Close();
            closeCon();

            return attribute;
        }

        public int search_attribute(string name)
        {
            openCon();
            int id = -1;
            string query = "SELECT * FROM attributes WHERE name='" + name + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                id = Convert.ToInt32(reader["id"]);
            }
            reader.Close();
            closeCon();
            return id;
        }

        public List<AttributeModel> get_attributes()
        {
            List<AttributeModel> attributes = new List<AttributeModel>();
            openCon();
            string query = "SELECT * FROM attributes";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = (string)reader["name"];
                    int type = Convert.ToInt32(reader["type"]);
                    AttributeModel temp = new AttributeModel(id, name, type);
                    attributes.Add(temp);
                }
            }
            reader.Close();
            closeCon();
            return attributes;
        }

        public List<AttributeModel> get_description(int class_id)
        {
            List<AttributeModel> attributes = new List<AttributeModel>();
            List<int> attr_ids = new List<int>();
            openCon();
            string query = "SELECT * FROM description WHERE class_id='" + class_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int attr_id = Convert.ToInt32(reader["attribute_id"]);
                    attr_ids.Add(attr_id);
                }
            }
            reader.Close();
            closeCon();

            foreach (int attr_id in attr_ids)
            {
                AttributeModel temp = get_attribute(attr_id);
                if (temp != null)
                    attributes.Add(temp);
            }
            return attributes;
        }

        public string get_value(int attr_id)
        {
            string value = null;
            openCon();
            string query = "SELECT * FROM generalvalues WHERE attribute_id='" + attr_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                value = (string)reader["possible_values"];
            }
            reader.Close();
            closeCon();
            return value;
        }

        public string get_classvalue(int class_id, int attr_id)
        {
            string value = null;
            openCon();
            string query = "SELECT * FROM classvalues WHERE class_id='" + class_id + "' AND attribute_id='" + attr_id + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                value = (string)reader["possible_values"];
            }
            reader.Close();
            closeCon();
            return value;
        }
    }
}
