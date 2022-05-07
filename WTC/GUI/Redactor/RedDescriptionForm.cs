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
    public partial class RedDescriptionForm : Form
    {
        public RedDescriptionForm()
        {
            InitializeComponent();
            DGVInit();
            CBInit();
            test();
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

        public void CBInit()
        {
            for (int i = 0; i < 5; i++)
                comboBox1.Items.Add("класс " + i);

            comboBox1.SelectedIndex = 0;
            comboBox1.Refresh();
        }

        public void test(string test = "")
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            dataGridView1.Rows.Add("test1");
            dataGridView1.Rows.Add("test2");
            dataGridView1.Rows.Add("test3");
            dataGridView1.Rows.Add("test4");
            dataGridView2.Rows.Add("test5");

            if(test != null)
                dataGridView2.Rows.Add(test);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                dataGridView1.Rows.Remove(row);
                dataGridView2.Rows.Add(row.Cells[0].Value);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                dataGridView2.Rows.Remove(row);
                dataGridView1.Rows.Add(row.Cells[0].Value);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            test(selectedState);
        }
    }
}
