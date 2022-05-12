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
    public partial class ResultForm : Form
    {
        public List<string> input_values;
        public ResultForm(List<string> values)
        {
            this.input_values = values;
            InitializeComponent();
            label2.Text = "";
            load_result();
        }

        public void load_result()
        {
            DatabaseManager db = new DatabaseManager();
            List<ClassModel> classes = db.get_classes();

            foreach (ClassModel cls in classes)
            {
                List<AttributeModel> unfit_attributes = classify(cls);
                if (unfit_attributes.Count == 0)
                    label2.Text += "\t" + cls.name + "\n";
            }
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = "";

            DatabaseManager db = new DatabaseManager();
            List<ClassModel> classes = db.get_classes();

            foreach (ClassModel cls in classes)
            {
                List<AttributeModel> unfit_attributes = classify(cls);
                if (unfit_attributes.Count != 0)
                {
                    text += "\nКласс '" + cls.name + "' отвергнут из-за значений признаков:";
                    foreach(AttributeModel attr in unfit_attributes)
                        text += "\n\t" + attr.name;
                }
                    
            }

            MessageBox.Show(text, "Подробнее", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public List<AttributeModel> classify(ClassModel cls)
        {
            List<AttributeModel> unfit_attributes = new List<AttributeModel>();

            DatabaseManager db = new DatabaseManager();
            List<AttributeModel> all_attributes = db.get_attributes();
            List<AttributeModel> description = db.get_description(cls.id);

            foreach(AttributeModel attr in all_attributes)
            {
                bool found = false;
                foreach (AttributeModel desc in description)
                {
                    if (desc.name == attr.name)
                    {
                        found = true;

                        string class_value = db.get_classvalue(cls.id, attr.id);
                        string input_value = input_values[all_attributes.IndexOf(attr)];

                        if (!check_value(attr.type, class_value, input_value))
                        {
                            unfit_attributes.Add(attr);
                        }
                    }
                }
                if ((!found) && (input_values[all_attributes.IndexOf(attr)] != ""))
                    unfit_attributes.Add(attr);
            }

            return unfit_attributes;
        }

        public bool check_value(int type, string gen_value, string value)
        {
            string[] general_words = gen_value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string[] words = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if(value == "")
                return true;

            if (words == null)
                return false;

            if (type == 1)
            {
                Double num;

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
                if (!general_words.Contains(words[0]))
                    return false;
            }
            return true;
        }
    }
}
