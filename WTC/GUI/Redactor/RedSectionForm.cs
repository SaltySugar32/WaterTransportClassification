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
    public partial class RedSectionForm : Form
    {
        public RedSectionForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RedClassesForm redClassesForm = new RedClassesForm();
            redClassesForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RedAttributeForm redAttributeForm = new RedAttributeForm();
            redAttributeForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RedDescriptionForm redDescriptionForm = new RedDescriptionForm();
            redDescriptionForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RedValuesForm redValuesForm = new RedValuesForm();
            redValuesForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RedValuesForClassForm redValuesForClassForm = new RedValuesForClassForm();
            redValuesForClassForm.ShowDialog();
        }
    }
}
