using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Easy_AutoIT_Helper.StringConst;
using Easy_AutoIT_Helper.Global;

namespace Easy_AutoIT_Helper
{
    public partial class frmNewStep : Form
    {
        public frmNewStep()
        {
            InitializeComponent();
        }

        private void frmNewStep_Load(object sender, EventArgs e)
        {
            List<String> listStep = new List<string> { 
            SystemString.STEP_CLICK,SystemString.STEP_CLICK_POS,SystemString.STEP_FOCUS,SystemString.STEP_OPEN_APP,
            SystemString.STEP_SEND_KEY,SystemString.STEP_SETTEXT,SystemString.STEP_SETTEXT_FOCUS
            };
            comboBox1.DataSource = listStep;

            textBox1.Text = Global.Memory.RecentID.ToString() ;
            txtClass.Text = Global.Memory.Title;
        }

        private void btnHelp1_Click(object sender, EventArgs e)
        {
            frmIntro f = new frmIntro(1);
            f.ShowDialog();
        }

        private void btnHelp2_Click(object sender, EventArgs e)
        {
            frmIntro f = new frmIntro(2);
            f.ShowDialog();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FileOption.FileHelper.OpenExe(StringConst.SystemString.AutoItInfor);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String key = comboBox1.Text;

            txtMetaData.Enabled = false;
           // txtClass.Enabled = false;
            txtClassNN.Enabled = false;

            if (key.Equals(SystemString.STEP_CLICK_POS))
            {
                txtMetaData.Enabled = true;
            }

            if (key.Equals(SystemString.STEP_FOCUS))
            {
                txtClass.Enabled = true;
                txtClassNN.Enabled = true;
            }

            if (key.Equals(SystemString.STEP_CLICK))
            {
                txtClass.Enabled = true;
                txtClassNN.Enabled = true;
            }


            if (key.Equals(SystemString.STEP_OPEN_APP))
            {
                txtClass.Enabled = true;
                txtMetaData.Enabled = true;
            }

            if (key.Equals(SystemString.STEP_SEND_KEY))
            {
                txtMetaData.Enabled = true;
            }

            if (key.Equals(SystemString.STEP_SETTEXT))
            {
                txtClass.Enabled = true;
                txtClassNN.Enabled = true;
                txtMetaData.Enabled = true;
            }

            if (key.Equals(SystemString.STEP_SETTEXT_FOCUS))
            {
                txtMetaData.Enabled = true;
            }

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtDes.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("The description is required");
                txtDes.Focus();
                return;
            }
            if (txtDur.Text.Trim().Equals(String.Empty))
            {
                MessageBox.Show("The duration is required");
                txtDur.Focus();
                 return;
            }

            Double duration = 1000;
            try{
            duration = Convert.ToDouble(txtDur.Text.Trim());
            }
            catch{
                MessageBox.Show("The duration must be number");
                txtDur.Focus();
                return;
            }

            Model.StepModel model = new Model.StepModel();
            model.StepId = Memory.RecentID;
            model.MetaData = txtMetaData.Text;
            model.StepName = comboBox1.Text;
            model.WaitDuration = duration;
            model.Description = txtDes.Text;
            model.ClassnameNN = txtClassNN.Text;
            model.Class = txtClass.Text;
            Memory.PreviousStep = model;   
             Memory.RecentID = Memory.RecentID + 1;
             Memory.Title = txtClass.Text;

             this.Close();
               
        }

        private void btnOpenApp_Click(object sender, EventArgs e)
        {
            FileOption.FileHelper.OpenExe(Global.GlobalScripting.ApplicationDir);
        }

        
    }
}
