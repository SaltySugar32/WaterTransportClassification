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
    public partial class UpdateClassForm : Form
    {
        string original_name;
        public UpdateClassForm(string name)
        {
            this.original_name = name;
            InitializeComponent();
            label1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateClass();
        }

        public void UpdateClass()
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
                    int original_id = db.search_class(original_name);
                    db.update_class(original_id, name);
                    MessageBox.Show(string.Format("Изменен класс: {0}.", name), "Результат", MessageBoxButtons.OK);
                }
                else
                    MessageBox.Show(string.Format("Ошибка. Класс {0} уже существует.", name), "Результат", MessageBoxButtons.OK);
            }
        }
    }
}
