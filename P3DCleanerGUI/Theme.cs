using P3DCleaner.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace P3DCleaner
{
    public static class Themes
    {

        private static bool defaultDark = true;

        public static void MakeLight(Form window)
        {
            foreach (Control ctrl in window.Controls)
            {
                ctrl.BackColor = System.Drawing.SystemColors.Control;
                ctrl.ForeColor = System.Drawing.Color.Black;
                if (ctrl is Button)
                {
                    ctrl.BackColor = System.Drawing.SystemColors.ControlLight;
                }
            }

            window.BackColor = System.Drawing.SystemColors.Control;
            window.Opacity = 1;
        }

        public static void MakeDark(Form window)
        {
            foreach (Control ctrl in window.Controls)
            {
                ctrl.BackColor = System.Drawing.SystemColors.WindowText;
                ctrl.ForeColor = System.Drawing.Color.White;
            }

            window.BackColor = System.Drawing.SystemColors.WindowText;
            window.Opacity = 0.9;
        }

        public static void ChangeTheme(Form window)
        {
            bool darkTheme = P3DCleaner.MainWindow.darkTheme;
            if (darkTheme)
            {
                MakeLight(window);

            }
            else
            {

                MakeDark(window);
            }
            //darkTheme = P3DCleaner.MainWindow.darkTheme = !P3DCleaner.MainWindow.darkTheme;
            Application.DoEvents();
        }

    }
}
