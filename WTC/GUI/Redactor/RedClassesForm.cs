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

            DataGridViewButtonColumn update_button_column = new DataGridViewButtonColumn();
            update_button_column.HeaderText = "";
            update_button_column.Width = (int)(width * 0.1);
            update_button_column.Name = "update_buttonColumn";
            update_button_column.Text = "Изменить";
            update_button_column.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn delete_button_column = new DataGridViewButtonColumn();
            delete_button_column.HeaderText = "";
            delete_button_column.Width = (int)(width * 0.1);
            delete_button_column.Name = "delete_buttonColumn";
            delete_button_column.Text = "Удалить";
            delete_button_column.UseColumnTextForButtonValue = true;



            dataGridView1.Columns.Add("class", "КЛАССЫ ВОДНОГО ТРАНСПОРТА");
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
            if(e.ColumnIndex == 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string class_name = (string)row.Cells["class"].Value;
                UpdateClassForm updateClassForm = new UpdateClassForm(class_name);
                updateClassForm.ShowDialog();

            }
            else if (e.ColumnIndex == 2)
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
