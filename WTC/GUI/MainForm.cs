using WTC.GUI;

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

        }
    }
}