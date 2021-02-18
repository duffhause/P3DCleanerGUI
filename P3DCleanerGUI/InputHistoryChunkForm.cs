using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace P3DCleaner
{
    public partial class InputHistoryChunkForm : Form
    {
        public int Line = 2;
        public List<System.Windows.Forms.TextBox> lines = new List<System.Windows.Forms.TextBox>();

        public InputHistoryChunkForm()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            decimal value = amountOfLines.Value;
            if (value > lines.Count)
            {
                do
                {
                    SpawnLine();
                } while (value != lines.Count);
            }

            if (value < lines.Count)
            {
                do
                {
                    this.Controls.Remove(lines[lines.Count - 1]);
                    lines[lines.Count - 1].Dispose();
                    lines.RemoveAt(lines.Count - 1);
                    Line--;
                } while (value != lines.Count);
            }
        }

        public void SpawnLine()
        {
            lines.Add(new System.Windows.Forms.TextBox());
            
            this.Controls.Add(lines[lines.Count - 1]);
            lines[lines.Count - 1].Top = Line * 25;
            lines[lines.Count - 1].Left = 15;
            lines[lines.Count - 1].Size = new System.Drawing.Size(260, 20);
            lines[lines.Count - 1].TabIndex = 4;
            lines[lines.Count - 1].MaxLength = 65535;

            string text;
            if (lines.Count == 1)
            {
                text = "Hello, World!";
            }
            else
            {
                text = String.Format("Line #{0}", lines.Count);
            }
            lines[lines.Count - 1].Text = text;

            Line++;
        }

        private void CustomHistoryChunkScreen_Load(object sender, EventArgs e)
        {
            Line = 2;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void SubmitLines_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}