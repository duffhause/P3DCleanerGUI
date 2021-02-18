using P3DCleaner.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace P3DCleaner
{
    public partial class MainWindow : Form
    {
        public static bool darkTheme = false;
        private bool singleP3D = false;
        private string[] lines;
        public static bool deleteUnexpectedChunks = true;

        private ProcessP3DForm ProcessP3DForm;

        public int StartCustomLinesDialog()
        {
            InputHistoryChunkForm CustomLinesWindow = new InputHistoryChunkForm();

            //this.Hide();
            this.Enabled = false;

            if (CustomLinesWindow.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<System.Windows.Forms.TextBox> lines1 = CustomLinesWindow.lines;
                CustomLinesWindow.Line = 2;
                lines = new string[lines1.Count];
                for (int i = 0; i < lines1.Count; i++)
                {
                    lines[i] = lines1[i].Text;
                }
                //this.Show();
                this.Enabled = true;
                return 0;
            }
            else
            {
                //this.Show();
                this.Enabled = true;
                return -1;
            }
            
        }

        public void OutputToPseudoConsole(string text)
        {
            ProcessP3DForm.textBox1.AppendText(text + Environment.NewLine);
        }

        public void ProcessFile(string path)
        {
            if (Path.GetExtension(path) != ".p3d")
            {
                return;
            }
            OutputToPseudoConsole(string.Format("Processing {0}", path));
            ProcessP3DForm.progressBar1.Increment(1);
            ProcessP3DForm.label1.Text = path;
            ProcessP3DForm.Update();
            P3D file = new P3D();
            file.ReadP3D(path);

            file.DeleteUnexpectedChunksInRoot();

            if (orderChunksLexo.Checked)
            {
                file.LexographChunks();
            }
            if (removeHistory.Checked)
            {
                file.RemoveHistoryChunks();
            }
            if (customHistory.Checked)
            {
                file.Root = file.AddHistory(file.Root, lines);
            }

            //OutputToPseudoConsole(string.Format("Writing {0}", path));
            if (file.WriteP3D(path) == 1) OutputToPseudoConsole(string.Format("Done {0}", path));
            else OutputToPseudoConsole(string.Format("No changes made to {0}", path));

            ProcessP3DForm.Update();
        }

        private int GetFileCount(string dir)
        {
            if (singleP3D)
            {
                return 1;
            }
            int i = 0;

            foreach (string file in Directory.GetFiles(dir))
            {
                if (Path.GetExtension(file).ToLower() == ".p3d")
                {
                    i++;
                }
            }

            foreach (string file in Directory.GetDirectories(dir))
            {
                i += GetFileCount(file);
            }
            return i;
        }

        private void ProcessDir(string path)
        {
            if (!singleP3D)
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    ProcessFile(file);
                }

                foreach (string file in Directory.GetDirectories(path))
                {
                    ProcessDir(file);
                }
            }
            else
            {
                ProcessFile(path);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (customHistory.Checked)
            {
                int var1 = StartCustomLinesDialog();
                if (var1 < 0)
                {
                    return;
                }
            }

            deleteUnexpectedChunks = this.deleteMisplaced.Checked;

            if (singleP3D && Path.GetExtension(modPath.Text).ToLower() != ".p3d")
            {
                MessageBox.Show(String.Format("The following file is not a P3D\n{0}", modPath.Text));
                return;
            }
            ProcessP3DForm = new ProcessP3DForm();
            ProcessP3DForm.Show();
            ProcessP3DForm.label1.Text = "Initilising";
            ProcessP3DForm.progressBar1.Maximum = GetFileCount(modPath.Text);
            Console.WriteLine(ProcessP3DForm.progressBar1.Maximum);
            ProcessDir(modPath.Text);
            ProcessP3DForm.Finish.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            modPath.Text = folderDlg.SelectedPath;
            singleP3D = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderDlg = new OpenFileDialog();
            DialogResult result = folderDlg.ShowDialog();
            modPath.Text = folderDlg.FileName;
            singleP3D = true;
        }

        private void customHistory_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void deleteDuplicate_CheckedChanged(object sender, EventArgs e)
        {
        }

		private void changeTheme_Click(object sender, EventArgs e)
		{
            Themes.ChangeTheme(this);
            darkTheme = !darkTheme;
		}
	}
}