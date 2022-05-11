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

namespace WTC.GUI.Redactor
{
    public partial class UpdateAttributeForm : Form
    {
        string original_name;
        public UpdateAttributeForm(string name)
        {
            this.original_name = name;
            InitializeComponent();
            label1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateAttr();
        }

        public void UpdateAttr()
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
                    int original_id = db.search_attribute(original_name);
                    db.update_attribute(original_id, name);
                    MessageBox.Show(string.Format("Изменен признак: {0}.", name), "Результат", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show(string.Format("Ошибка. Признак {0} уже существует.", name), "Результат", MessageBoxButtons.OK);
            }
        }
    }
}
