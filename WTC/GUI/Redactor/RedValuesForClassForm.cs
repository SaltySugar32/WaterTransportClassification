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
    public partial class RedValuesForClassForm : Form
    {
        public RedValuesForClassForm()
        {
            InitializeComponent();
            CB2Init();
            textBox1.Enabled = false;
        }
        public void CB2Init()
        {
            comboBox2.Items.Add("Качественный признак");
            comboBox2.Items.Add("Количественный признак");
            comboBox2.SelectedIndex = 0;
            comboBox2.Refresh();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = "Возможные значения: ";
            int index = comboBox2.SelectedIndex;
            switch (index)
            {
                case 0:
                    groupBox3.Text = text + "печеслисление значений, разделенных пробелом";
                    break;

                default:
                    groupBox3.Text = text + "нижняя и верхняя граница, разделенные пробелом";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
        }
    }
}
