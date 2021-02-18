using System;
using System.Windows.Forms;

namespace P3DCleaner
{
    public partial class ProcessP3DForm : Form
    {
        public ProcessP3DForm()
        {
            InitializeComponent();
        }

        public void ProcessFiles(string path, bool singleFile, bool[] Settings, string[] CustomHistoryLines)
        {
        }

        private void ProcessP3DForm_Load(object sender, EventArgs e)
        {
            Finish.Hide();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Finish_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}