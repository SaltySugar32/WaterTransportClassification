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

namespace WTC.GUI.Classifier
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
            DGVInit();
            load_attributes();
        }

        public void DGVInit()
        {
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            int width = dataGridView1.Width - 15;

            DataGridViewTextBoxColumn textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.HeaderText = "ЗНАЧЕНИЕ";
            textBoxColumn.Width = (int)(width * 0.2);
            textBoxColumn.Name = "textboxcolumn";
            textBoxColumn.ValueType = typeof(string);
            textBoxColumn.ReadOnly = false;
            textBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            textBoxColumn.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dataGridView1.Columns.Add("attr", "ПРИЗНАКИ");
            dataGridView1.Columns[0].Width = (int)(width * 0.3);
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.Columns[0].DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dataGridView1.Columns.Add("attr_type", "ТИП ПРИЗНАКА");
            dataGridView1.Columns[1].Width = (int)(width * 0.2);
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.Columns[1].DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dataGridView1.Columns.Add("pos_value", "ВОЗМОЖНЫЕ ЗНАЧЕНИЯ");
            dataGridView1.Columns[2].Width = (int)(width * 0.3);
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.Columns[2].DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dataGridView1.Columns.Add(textBoxColumn);

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
                string value_text;
                if (attribute.type == 0)
                    value_text = "Качественный";
                else
                    value_text = "Количественный";

                dataGridView1.Rows.Add(attribute.name, value_text, db.get_value(attribute.id), "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool correct = true;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[3].Value == null)
                    row.Cells[3].Value = "";

                if (!check_input(row))
                {
                    correct = false;
                    button1.Text = row.Index + " incorrect";
                }
                    
            }
            if (correct)
                button1.Text = "cor";
        }

        public bool check_input(DataGridViewRow row)
        {

            DatabaseManager db = new DatabaseManager();
            List<AttributeModel> attributes = db.get_attributes();
            AttributeModel attribute = attributes[row.Index];

            string general_value = db.get_value(attribute.id);
            string[] general_words = general_value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string value = (string)row.Cells[3].Value;
            if (value == null)
                return false;

            string[] words = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words == null)
                return false;

            if (value == "")
                return true;

            int index = attribute.type;

            if (index == 1)
            {
                Double num;

                if (words.Length != 1)
                    return false;

                if (!Double.TryParse(words[0], out num))
                    return false;

                Double gen_num1, gen_num2;
                Double.TryParse(general_words[0], out gen_num1);
                Double.TryParse(general_words[1], out gen_num2);

                if ((num < gen_num1) || (num > gen_num2))
                    return false;

            }
            else
            {
                if (words.Length != words.Distinct().Count())
                    return false;

                if (!general_words.Contains(words[0]))
                    return false;

            }
            return true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
