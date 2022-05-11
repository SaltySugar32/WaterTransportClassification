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
    public partial class RedValuesForClassForm : Form
    {
        public RedValuesForClassForm()
        {
            InitializeComponent();
            load_classes();
            load_attributes();
            load_types();
            load_values();
            textBox1.Enabled = false;
            comboBox2.Enabled = false;
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

            if (comboBox1.Items.Count != 0)
                comboBox1.SelectedIndex = 0;

            comboBox1.Refresh();
        }

        public void load_attributes()
        {
            comboBox3.Items.Clear();
            if (comboBox1.Items.Count != 0)
            {

                DatabaseManager db = new DatabaseManager();
                List<ClassModel> classes = db.get_classes();
                ClassModel selected_class = classes[comboBox1.SelectedIndex];

                List<AttributeModel> description = db.get_description(selected_class.id);

                foreach (AttributeModel attr in description)
                {
                    comboBox3.Items.Add(attr.name);
                }

                if (comboBox3.Items.Count != 0)
                    comboBox3.SelectedIndex = 0;
            }

            comboBox3.Refresh();
        }

        public void load_types()
        {
            comboBox2.Items.Clear();
            if (comboBox3.Items.Count != 0)
            {
                DatabaseManager db = new DatabaseManager();
                List<ClassModel> classes = db.get_classes();
                ClassModel selected_class = classes[comboBox1.SelectedIndex];

                List<AttributeModel> description = db.get_description(selected_class.id);

                AttributeModel selected_attribute = description[comboBox3.SelectedIndex];

                comboBox2.Items.Add("Качественный признак");
                comboBox2.Items.Add("Количественный признак");

                comboBox2.SelectedIndex = selected_attribute.type;
            }
            comboBox2.Refresh();
        }

        public void load_values()
        {
            textBox1.Text = "";
            label1.Text = "";
            if (comboBox3.Items.Count != 0)
            {
                DatabaseManager db = new DatabaseManager();
                List<ClassModel> classes = db.get_classes();
                ClassModel selected_class = classes[comboBox1.SelectedIndex];

                List<AttributeModel> description = db.get_description(selected_class.id);
                AttributeModel selected_attribute = description[comboBox3.SelectedIndex];

                string general_value = "Возможные значения признака: " + db.get_value(selected_attribute.id) + "";
                label1.Text = general_value;

                string value = db.get_classvalue(selected_class.id, selected_attribute.id);
                textBox1.Text = value;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_types();
            load_values();
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check_value())
            {
                DatabaseManager db = new DatabaseManager();
                List<ClassModel> classes = db.get_classes();
                ClassModel selected_class = classes[comboBox1.SelectedIndex];

                List<AttributeModel> description = db.get_description(selected_class.id);
                AttributeModel selected_attribute = description[comboBox3.SelectedIndex];

                string original = db.get_classvalue(selected_class.id, selected_attribute.id);
                if (original == null)
                {
                    db.add_classvalues(selected_class.id, selected_attribute.id, textBox1.Text);
                    MessageBox.Show(string.Format("Добавлены значения '{0}' для признака '{1}'", textBox1.Text, selected_attribute.name), "Результат", MessageBoxButtons.OK);
                }
                else if (original != textBox1.Text)
                {
                    db.update_classvalues(selected_class.id, selected_attribute.id, textBox1.Text);
                    MessageBox.Show(string.Format("Добавлены значения '{0}' для признака '{1}'", textBox1.Text, selected_attribute.name), "Результат", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show("Ошибка. Нет изменений", "Результат", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Ошибка ввода", "Результат", MessageBoxButtons.OK);

            textBox1.Enabled = false;
        }

        public bool check_value()
        {
            if (comboBox3.Items.Count == 0)
                return false;

            DatabaseManager db = new DatabaseManager();
            List<ClassModel> classes = db.get_classes();
            ClassModel selected_class = classes[comboBox1.SelectedIndex];

            List<AttributeModel> description = db.get_description(selected_class.id);
            AttributeModel selected_attribute = description[comboBox3.SelectedIndex];

            string general_value = db.get_value(selected_attribute.id);
            string[] general_words = general_value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string value = textBox1.Text;
            string[] words = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int index = comboBox2.SelectedIndex;

            if (value == null)
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

                if (num2 < num1)
                    return false;

                Double gen_num1, gen_num2;
                Double.TryParse(general_words[0], out gen_num1);
                Double.TryParse(general_words[1], out gen_num2);

                if ((num1 < gen_num1) || (num1 > gen_num2))
                    return false;

                if ((num2 < gen_num1) || (num2 > gen_num2))
                    return false;

            }
            else
            {
                if (words.Length != words.Distinct().Count())
                    return false;

                foreach (string word in words)
                {
                    if (!general_words.Contains(word))
                        return false;
                }
            }

            return true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_attributes();
            load_types();
            load_values();
        }
    }
}
