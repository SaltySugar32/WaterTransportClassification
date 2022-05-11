using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WTC.GUI.Redactor;
using WTC.Managers;

namespace WTC.GUI
{
    public partial class RedAttributeForm : Form
    {
        public RedAttributeForm()
        {
            InitializeComponent();
            DGVInit();
            load_attributes();
        }

        public void DGVInit()
        {
            int width = dataGridView1.Width-10;

            DataGridViewButtonColumn delete_button_column = new DataGridViewButtonColumn();
            delete_button_column.HeaderText = "";
            delete_button_column.Width = (int)(width * 0.1);
            delete_button_column.Name = "deletebuttonColumn";
            delete_button_column.Text = "Удалить";
            delete_button_column.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn update_button_column = new DataGridViewButtonColumn();
            update_button_column.HeaderText = "";
            update_button_column.Width = (int)(width * 0.1);
            update_button_column.Name = "updatebuttonColumn";
            update_button_column.Text = "Изменить";
            update_button_column.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add("attr", "ПРИЗНАКИ");
            dataGridView1.Columns[0].Width = (int)(width * 0.8);

            dataGridView1.Columns.Add(update_button_column);
            dataGridView1.Columns.Add(delete_button_column);

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        public void load_attributes()
        {
            dataGridView1.Rows.Clear();

            DatabaseManager db = new DatabaseManager();
            List<AttributeModel> attributes = db.get_attributes();
            foreach (AttributeModel attribute in attributes)
            {
                dataGridView1.Rows.Add(attribute.name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddAttr();
        }

        public void AddAttr()
        {
            string name = textBox1.Text;
            if (name.Trim() == "")
                MessageBox.Show("Ошибка ввода", "Результат", MessageBoxButtons.OK);
            else
            {
                DatabaseManager db = new DatabaseManager();
                int id = db.search_attribute(name);
                if (id < 0)
                {
                    dataGridView1.Rows.Add(name);
                    db.add_attribute(textBox1.Text, 0);
                    MessageBox.Show(string.Format("Добавлен признак: {0}.", name), "Результат", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show(string.Format("Ошибка. Признак {0} уже существует.", name), "Результат", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string attr_name = (string)row.Cells["attr"].Value;
                UpdateAttributeForm updateAttributeForm = new UpdateAttributeForm(attr_name);
                updateAttributeForm.ShowDialog();
            }

            if (e.ColumnIndex == 2)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string attr_name = (string)row.Cells["attr"].Value;
                if (MessageBox.Show(string.Format("Удалить признак: {0}?", attr_name), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DatabaseManager db = new DatabaseManager();
                    int attr_id = db.search_attribute(attr_name);
                    if (attr_id >= 0)
                    {
                        db.remove_attribute(attr_name);
                        db.remove_description_by_attribute(attr_id);
                        db.remove_values_by_attribute(attr_id);
                        db.remove_classvalues_by_attribute(attr_id);
                    }
                    dataGridView1.Rows.Remove(row);
                }
            }
        }
    }
}
