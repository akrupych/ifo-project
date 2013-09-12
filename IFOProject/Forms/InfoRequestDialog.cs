using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IFOProject.Forms
{
    /// <summary>
    /// Requests user for additional info when exporting results.
    /// Info is stored in dialog fields and can be accessed after dialog is closed.
    /// </summary>
    public partial class InfoRequestDialog : Form
    {
        // fields for storing results
        // can be accessed after dialog is closed
        public string CrystalMaterial { get; private set; }
        public string Cut { get; private set; }
        public string LoadDirection { get; private set; }

        public InfoRequestDialog()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Save results when form is closing
        /// </summary>
        private void InfoRequestDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            CrystalMaterial = textBoxCrystalMaterial.Text;
            Cut = textBoxCut.Text;
            LoadDirection = textBoxLoadDirection.Text;
        }
    }
}
