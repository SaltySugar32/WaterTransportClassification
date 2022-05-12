using WTC.GUI;
using WTC.GUI.Classifier;
using WTC.Managers;

namespace WTC
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RedSectionForm red_form = new RedSectionForm();
            red_form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check_data_integrity())
            {
                InputForm inputForm = new InputForm();
                inputForm.ShowDialog();
            }

        }

        public bool check_data_integrity()
        {
            bool correct = true;
            string text = "";

            DataManager dm = new DataManager();
            DatabaseManager db = new DatabaseManager();

            List<ClassModel> empty_classes = dm.check_description_integrity();
            if (empty_classes.Count != 0)
            {
                correct = false;
                text += "Ќет признакового описани€ дл€ классов водного транспорта:\n";
                foreach (ClassModel cls in empty_classes)
                {
                    text += "\t" + cls.name + "\n";
                }
            }

            List<AttributeModel> empty_values = dm.check_attribute_values();
            if (empty_values.Count != 0)
            {
                correct = false;
                text += "Ќет возможных значений признака:\n";
                foreach (AttributeModel attr in empty_values)
                {
                    text += "\t" + attr.name + "\n";
                }
            }

            List<ClassModel> classes = db.get_classes();
            foreach (ClassModel cls in classes)
            {
                List<AttributeModel> empty_classvalues = dm.check_attribute_class_values(cls.id);
                if (empty_classvalues.Count != 0)
                {
                    correct = false;
                    text += "Ќет возможных значений признаков дл€ класса водного транспорта " + cls.name + ":\n";
                    foreach (AttributeModel attr in empty_classvalues)
                    {
                        text += "\t" + attr.name + "\n";
                    }
                }
            }

            if(!correct)
                MessageBox.Show(text, "ѕроверка целостности данных", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return correct;
        }
    }
}