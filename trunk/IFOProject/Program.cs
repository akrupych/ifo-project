using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IFOProject.Forms;
using IFOProject.Experimental;

namespace IFOProject
{
    static class Program
    {
        public static Package Package { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
