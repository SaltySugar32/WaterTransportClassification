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
    public partial class RedClassesForm : Form
    {
        public RedClassesForm()
        {
            InitializeComponent();
            DGVInit();
            load_classes();

        }

        public void DGVInit()
        {
            int width = dataGridView1.Width;

            DataGridViewButtonColumn button_column = new DataGridViewButtonColumn();
            button_column.HeaderText = "";
            button_column.Width = (int)(width * 0.1);
            button_column.Name = "buttonColumn";
            button_column.Text = "Удалить";
            button_column.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add("class", "КЛАССЫ ВОДНОГО ТРАНСПОРТА");
            dataGridView1.Columns[0].Width = (int)(width * 0.9);

            dataGridView1.Columns.Add(button_column);

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EnableHeadersVisualStyles = false;


        }

        public void load_classes()
        {
            dataGridView1.Rows.Clear();

            DatabaseManager db = new DatabaseManager();
            List<ClassModel> classes = db.get_classes();
            foreach (ClassModel cls in classes)
            {
                dataGridView1.Rows.Add(cls.name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddClass();
        }

        public void AddClass()
        {
            string name = textBox1.Text;
            if (name.Trim() == "")
                MessageBox.Show("Ошибка ввода", "Результат", MessageBoxButtons.OK);
            else
            {
                DatabaseManager db = new DatabaseManager();
                int id = db.search_class(name);
                if (id < 0)
                {

                    dataGridView1.Rows.Add(name);
                    db.add_class(name);
                    MessageBox.Show(string.Format("Добавлен класс: {0}.", name), "Результат", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show(string.Format("Ошибка. Класс {0} уже существует.", name), "Результат", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string class_name = (string)row.Cells["class"].Value;
                if (MessageBox.Show(string.Format("Удалить класс водного транспорта: {0}?", class_name), "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DatabaseManager db = new DatabaseManager();
                    int class_id = db.search_class(class_name);
                    if (class_id >= 0)
                    {
                        db.remove_class(class_name);
                        db.remove_description_by_class(class_id);
                        db.remove_classvalues_by_class(class_id);
                    }
                    dataGridView1.Rows.Remove(row);
                }
            }
        }
    }
}
