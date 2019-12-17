using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Easy_AutoIT_Helper.FileOption;

namespace Easy_AutoIT_Helper
{
    public partial class frmNewScript : Form
    {
        public frmNewScript()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            String dir = FileHelper.OpenFileDialog();
            if (!dir.Equals(String.Empty))
            {
                txtTestDir.Text = dir;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Global.GlobalScripting.ScriptName = txtScriptName.Text.Trim();
            Global.GlobalScripting.ApplicationDir = txtTestDir.Text.Trim();
            this.Close();
        }

        private void txtTestDir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCreate_Click(null, null);
            }
        }
    }
}
