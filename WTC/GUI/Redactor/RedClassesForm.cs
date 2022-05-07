using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WTC.GUI
{
    public partial class RedClassesForm : Form
    {
        public RedClassesForm()
        {
            InitializeComponent();
            DGVInit();
            test();

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

        public void test()
        {
            dataGridView1.Rows.Add("test1");
            dataGridView1.Rows.Add("test2");
            dataGridView1.Rows.Add("test3");
            dataGridView1.Rows.Add("test4");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddClass();
        }

        public void AddClass()
        {
            dataGridView1.Rows.Add(textBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (MessageBox.Show(string.Format("Удалить класс водного транспорта: {0}?", row.Cells["class"].Value), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dataGridView1.Rows.Remove(row);
                }
            }
        }
    }
}
