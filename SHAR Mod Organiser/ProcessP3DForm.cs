using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHARModOrganiserGUI
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
