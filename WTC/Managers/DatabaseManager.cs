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

        public void remove_attribute(string name, int type)
        {
            openCon();
            string query = "DELETE FROM attributes WHERE name='" + name + "' AND type='" + type + "'"; 
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
    }
}
