using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WTC.Managers;

namespace WTC.GUI
{
    public partial class RedDescriptionForm : Form
    {
        public RedDescriptionForm()
        {
            InitializeComponent();
            DGVInit();
            load_classes();
            load_attributes();
        }

        public void DGVInit()
        {
            int button_width = 30;
            int width = dataGridView1.Width - button_width;

            DataGridViewButtonColumn button_column_plus = new DataGridViewButtonColumn();
            button_column_plus.HeaderText = "";
            button_column_plus.Width = button_width;
            button_column_plus.Name = "buttonColumnPlus";
            button_column_plus.Text = "+";
            button_column_plus.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add("attr", "ПРИЗНАКИ");
            dataGridView1.Columns[0].Width = width;
            dataGridView1.Columns.Add(button_column_plus);

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EnableHeadersVisualStyles = false;

            DataGridViewButtonColumn button_column_minus = new DataGridViewButtonColumn();
            button_column_minus.HeaderText = "";
            button_column_minus.Width = button_width;
            button_column_minus.Name = "buttonColumnMinus";
            button_column_minus.Text = "-";
            button_column_minus.UseColumnTextForButtonValue = true;

            dataGridView2.Columns.Add("desc", "ПРИЗНАКОВОЕ ОПИСАНИЕ");
            dataGridView2.Columns[0].Width = width;
            dataGridView2.Columns.Add(button_column_minus);

            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.EnableHeadersVisualStyles = false;
        }

        public void load_classes()
        {
            comboBox1.Items.Clear();

            DatabaseManager db = new DatabaseManager();
            List<ClassModel> classes = db.get_classes();
            foreach (ClassModel cls in classes)
            {
                comboBox1.Items.Add(cls.name);
            }

            comboBox1.SelectedIndex = 0;
            comboBox1.Refresh();
        }

        public void load_attributes()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            DatabaseManager db = new DatabaseManager();
            List<ClassModel> classes = db.get_classes();
            List<AttributeModel> attributes = db.get_attributes();
            ClassModel selected_class = classes[comboBox1.SelectedIndex];
            List<AttributeModel> description = db.get_description(selected_class.id);

            foreach (AttributeModel attr in attributes)
            {
                bool found = false;
                foreach(AttributeModel desc in description)
                {
                    if (desc.name == attr.name)
                        found = true;
                }

                if (found)
                    dataGridView2.Rows.Add(attr.name);
                else
                    dataGridView1.Rows.Add(attr.name);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DatabaseManager db = new DatabaseManager();
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string attr_name = (string)row.Cells["attr"].Value;
                int attr_id = db.search_attribute(attr_name);
                if (attr_id >= 0)
                {
                    List<ClassModel> classes = db.get_classes();
                    ClassModel selected_class = classes[comboBox1.SelectedIndex];

                    db.add_description(selected_class.id, attr_id);
                    dataGridView1.Rows.Remove(row);
                    dataGridView2.Rows.Add(attr_name);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DatabaseManager db = new DatabaseManager();
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                string attr_name = (string)row.Cells["desc"].Value;
                int attr_id = db.search_attribute(attr_name);
                if (attr_id >= 0)
                {
                    List<ClassModel> classes = db.get_classes();
                    ClassModel selected_class = classes[comboBox1.SelectedIndex];

                    db.remove_description(selected_class.id, attr_id);

                    dataGridView2.Rows.Remove(row);
                    dataGridView1.Rows.Add(attr_name);
                }
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_attributes();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
