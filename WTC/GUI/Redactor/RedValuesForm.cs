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
    public partial class RedValuesForm : Form
    {
        public RedValuesForm()
        {
            InitializeComponent();
            load_attributes();
            load_types();
            load_values();
            textBox1.Enabled = false;
        }

        public void load_types()
        {
            comboBox2.Items.Clear();

            if (comboBox1.Items.Count != 0)
            {
                DatabaseManager db = new DatabaseManager();
                List<AttributeModel> attributes = db.get_attributes();
                AttributeModel selected_attribute = attributes[comboBox1.SelectedIndex];

                comboBox2.Items.Add("Качественный признак");
                comboBox2.Items.Add("Количественный признак");

                comboBox2.SelectedIndex = selected_attribute.type;
            }

            comboBox2.Refresh();
        }

        public void load_values()
        {
            textBox1.Text = "";
            if (comboBox1.Items.Count != 0)
            {
                DatabaseManager db = new DatabaseManager();
                List<AttributeModel> attributes = db.get_attributes();
                AttributeModel selected_attribute = attributes[comboBox1.SelectedIndex];

                string value = db.get_value(selected_attribute.id);
                textBox1.Text = value;
            }
        }

        public void load_attributes()
        {
            comboBox1.Items.Clear();

            DatabaseManager db = new DatabaseManager();
            List<AttributeModel> attributes = db.get_attributes();
            foreach (AttributeModel attribute in attributes)
            {
                comboBox1.Items.Add(attribute.name);
            }

            if (comboBox1.Items.Count != 0)
                comboBox1.SelectedIndex = 0;
            comboBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_groupbox_text();
        }

        public void load_groupbox_text()
        {
            string text = "Возможные значения: ";
            int index = comboBox2.SelectedIndex;
            switch (index)
            {
                case 0:
                    groupBox3.Text = text + "множество значений, разделенных пробелом";
                    break;

                default:
                    groupBox3.Text = text + "диапазон (нижняя и верхняя границы), разделенные пробелом";
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check_value())
            {
                DatabaseManager db = new DatabaseManager();
                List<AttributeModel> attributes = db.get_attributes();
                AttributeModel selected_attribute = attributes[comboBox1.SelectedIndex];
                string value = db.get_value(selected_attribute.id);

                db.remove_values(selected_attribute.id, value);
                db.add_values(selected_attribute.id, textBox1.Text);

                db.update_attribute(selected_attribute.name, comboBox2.SelectedIndex);

                MessageBox.Show(string.Format("Добавлены значения '{0}' для признака '{1}'", textBox1.Text, selected_attribute.name), "Результат", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Ошибка ввода", "Результат", MessageBoxButtons.OK);

            textBox1.Enabled=false;
        }

        public bool check_value()
        {
            if (comboBox1.Items.Count == 0)
                return false;

            string value = textBox1.Text;
            string[] words = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int index = comboBox2.SelectedIndex;

            if(value == null)
                return false;

            if (index == 1)
            {
                Double num1, num2;
                
                if (words.Length != 2)
                    return false;

                if (!Double.TryParse(words[0], out num1))
                    return false;

                if (!Double.TryParse(words[1], out num2))
                    return false;

                if (num2<num1)
                    return false;

            }
            else
            {
                if (words.Length != words.Distinct().Count())
                    return false;
            }

            return true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_types();
            load_values();
        }
    }
}
