using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Easy_AutoIT_Helper.FileOption;
using Easy_AutoIT_Helper.StringConst;
using Easy_AutoIT_Helper.Global;
using Easy_AutoIT_Helper.Model;
using Easy_AutoIT_Helper.Data;
using Easy_AutoIT_Helper.AutoItUlti;
using AutoItX3Lib;
using System.Threading;
namespace Easy_AutoIT_Helper
{
    public partial class frmMain : Form
    {

        
        public frmMain()
        {
            InitializeComponent();
        }

   
        
        private void frmMain_Load(object sender, EventArgs e)
        {
          
            CheckForIllegalCrossThreadCalls = false;//step1: CheckExist AutoInfor
            if (!FileHelper.IsExistFile(SystemString.AutoItInfor))
            {
                DialogResult rs = MessageBox.Show(ErrorString.AUTOIT_NOT_INSTALL, ErrorString.AUTOIT_ERROR_TOPIC, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (rs == DialogResult.OK)
                {
                    FileHelper.OpenExe(SystemString.AutoItSetUp);
                }
            }

            LoadSetting();
        }

        private void btnOpenAutoIt_Click(object sender, EventArgs e)
        {
            try
            {
               FileHelper.OpenExe(SystemString.AutoItInfor);
            }
            catch
            {
                DialogResult rs = MessageBox.Show(ErrorString.AUTOIT_NOT_INSTALL, ErrorString.AUTOIT_ERROR_TOPIC, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                 if (rs == DialogResult.OK)
                 {
                     FileHelper.OpenExe(SystemString.AutoItSetUp);
                 }
            }
        }

        private void btnInstallAutoItInfor_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(ErrorString.AUTOIT_QUESTION_INSTALL, ErrorString.AUTOIT_QUESTION_TOPIC, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.OK)
            {
                FileHelper.OpenExe(SystemString.AutoItSetUp);
            }
        }


        private void LoadSetting()
        {
            txtSetupFileDir.Text = SystemString.AutoItSetUp;
            txtAutoInfoDir.Text = SystemString.AutoItInfor;
        }

        private void btnBrowse1_Click(object sender, EventArgs e)
        {
           String dir = FileHelper.OpenFileDialog();
           if (!dir.Equals(String.Empty))
           {
               txtSetupFileDir.Text = dir;
               SystemString.AutoItSetUp = dir;
           }
        }

        private void btnBrows2_Click(object sender, EventArgs e)
        {
            String dir = FileHelper.OpenFileDialog();
            if (!dir.Equals(String.Empty))
            {
                txtAutoInfoDir.Text = dir;
                SystemString.AutoItInfor = dir;
            }
        }

        private void authorInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright of Dang Nguyen Hong Kha","Easy AutoIt");
        }

        private void newScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewScript frm = new frmNewScript();
            frm.ShowDialog();
            if (!GlobalScripting.ApplicationDir.Equals(String.Empty)
                && !GlobalScripting.ScriptName.Equals(String.Empty))
            {
                tabMain.Show();
                lb_dir.Text = GlobalScripting.ApplicationDir;
                lb_name.Text = GlobalScripting.ScriptName;
                this.WindowState = FormWindowState.Maximized;

                List<String> cmbOptions = new List<string> { 
                "Default","Debug"
                };
                cmbOption.DataSource = cmbOptions;
                cmbOption.Text = "Default";
                btnRun.BackColor = Color.Green;
                LoadFirstData();
                Global.GlobalScripting.IsSaveSript = false;
            }
        }

        private void LoadFirstData()
        {
           // dgvStep.RowHeadersVisible = false;
            List<StepModel> listData = new List<StepModel> { 
            new StepModel(1,SystemString.STEP_OPEN_APP,String.Empty,String.Empty,"Open Application",1000),
            };
            listData[0].MetaData = Global.GlobalScripting.ApplicationDir;
            DataTable dt = DataHelper.ConvertToDataTable(listData);
            dgvStep.DataSource = dt;
        }

        private void cmbOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOption.Text.Equals("Debug"))
            {
                btnRun.BackColor = Color.Red;
            }
            else
            {
                btnRun.BackColor = Color.Green;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (cmbOption.Text.Equals("Default"))
            {
               
                    Invoke(new Action(() =>
                    {
                        btnRun.Enabled = false;
                    }));
                   
                    tabControl1.SelectedTab = tabPage4;
                    AUlti auto = new AUlti();
                    List<StepModel> MySteps = GetStepFormView();
                    Thread t = new Thread(() =>
                    {
                    foreach (StepModel step in MySteps)
                    {
                        auto.Active(step.Class);
                        Thread.Sleep(100);
                        String metanew = Rand.EzRand.RandFromMeta(step.MetaData);
                        step.MetaData = metanew;
                        WriteLog("Start " + step.StepName + " - " + step.ClassnameNN + " - Desciption: " + step.Description + " - with data: " + step.MetaData);
                        auto.RunStep(step);
                        WriteLog("Wait " + step.WaitDuration.ToString());
                        Thread.Sleep(Int32.Parse(step.WaitDuration.ToString()));
                    }
                    Invoke(new Action(() =>
                    {
                        btnRun.Enabled = true;
                    }));
                   
               
                });
                t.Start();

            }
        }

        private void WriteLog(String Text)
        {
           
            Invoke(new Action(() =>
            {
                String text = "[Datetime: " + DateTime.Now.ToString() + "] \t";
                richTextBox1.Text = richTextBox1.Text + text + Text + "\n";
            }));
        }


        private List<Model.StepModel> GetStepFormView()
        {
            List<Model.StepModel> mylist = new List<StepModel>();
            foreach (DataGridViewRow row in dgvStep.Rows)
            {
                try
                {
                    Model.StepModel model = new StepModel();
                    model.StepId = Convert.ToInt32(row.Cells["StepId"].Value.ToString());
                    model.Description = row.Cells["Description"].Value.ToString();
                    model.Class = row.Cells["Class"].Value.ToString();
                    model.ClassnameNN = row.Cells["ClassnameNN"].Value.ToString();
                    model.MetaData = row.Cells["MetaData"].Value.ToString();
                    model.StepName = row.Cells["StepName"].Value.ToString();
                    model.WaitDuration = Convert.ToDouble(row.Cells["WaitDuration"].Value.ToString());
                    mylist.Add(model);
                }
                catch { }
            }
            return mylist;
        
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {
            frmNewStep f = new frmNewStep();
            f.ShowDialog();
            if (Memory.PreviousStep != null)
            {
                DataTable dt = (DataTable)dgvStep.DataSource;
                dt.Rows.Add(Memory.PreviousStep.StepId, Memory.PreviousStep.StepName,Memory.PreviousStep.Class,Memory.PreviousStep.ClassnameNN,
                    Memory.PreviousStep.MetaData,Memory.PreviousStep.WaitDuration,Memory.PreviousStep.Description);

                dgvStep.DataSource = dt;
                dgvStep.Refresh();
                Memory.PreviousStep = null;
                Global.GlobalScripting.IsSaveSript = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Global.GlobalScripting.IsSaveSript = false;
            
            if (btnUpdate.Text.Equals("Change"))
            {
                btnUpdate.Text = "Save";
                dgvStep.Enabled = true;
               
            }
            else
            {
                btnUpdate.Text = "Change";
                dgvStep.Enabled = false;
                List<Model.StepModel> mylist = GetStepFormView();
                DataTable dt = DataHelper.ConvertToDataTable(mylist);
                dgvStep.DataSource = dt;
            }
        }

        private void btnSaveScript_Click(object sender, EventArgs e)
        {
            string date = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
            String Name = Global.GlobalScripting.ScriptName + ".ini";
        
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\Script"))
            { 
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Script");
            }
            FileOption.FileHelper.writeFile(Application.StartupPath+"\\Script\\"+Name, GetStepFormView());
            MessageBox.Show("Save Script Successfully! ");
            FileHelper.OpenFolder(Application.StartupPath + "\\Script");
            Global.GlobalScripting.IsSaveSript = true;
        }

        

        private void openScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
           String dir = FileOption.FileHelper.OpenFileDialog1();
           if (!dir.Equals(String.Empty))
           {
               try
               {
                   List<StepModel> myList = FileOption.FileHelper.readDataFromFile(dir);

                   if (!GlobalScripting.ApplicationDir.Equals(String.Empty)
                && !GlobalScripting.ScriptName.Equals(String.Empty))
                   {
                       tabMain.Show();
                       lb_dir.Text = GlobalScripting.ApplicationDir;
                       lb_name.Text = GlobalScripting.ScriptName;
                       this.WindowState = FormWindowState.Maximized;

                       List<String> cmbOptions = new List<string> { 
                "Default","Debug"
                };
                       cmbOption.DataSource = cmbOptions;
                       cmbOption.Text = "Default";
                       btnRun.BackColor = Color.Green;
                       DataTable dt = Data.DataHelper.ConvertToDataTable(myList);
                       dgvStep.DataSource = dt;
                        Global.GlobalScripting.IsSaveSript = true;
                   }

               }
               catch {
                   MessageBox.Show("File not sultatble!");
               }
           }
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {

            String date = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
            String Name = "Logs " + Global.GlobalScripting.ScriptName + date + ".txt";
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\Logs"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Logs");
            }
            FileOption.FileHelper.writeFile(Application.StartupPath + "\\Script\\" + Name, richTextBox1.Text);
            MessageBox.Show("Save Logs Successfully! ");
            FileHelper.OpenFolder(Application.StartupPath + "\\Logs");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Global.GlobalScripting.IsSaveSript)
                return;
            DialogResult tl = MessageBox.Show("Script was not saved, Do you want to save it?", "Ez Auto", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if(tl == DialogResult.Cancel)
                e.Cancel = true;
            else
            {
                if(tl == DialogResult.Yes)
                {
                    string date = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
                    String Name = Global.GlobalScripting.ScriptName + ".ini";

                    if (!System.IO.Directory.Exists(Application.StartupPath + "\\Script"))
                    {
                        System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Script");
                    }
                    FileOption.FileHelper.writeFile(Application.StartupPath + "\\Script\\" + Name, GetStepFormView());
                    MessageBox.Show("Save Script Successfully! ");
                    FileHelper.OpenFolder(Application.StartupPath + "\\Script");
                    Global.GlobalScripting.IsSaveSript = true;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String str = txtInPut.Text;
            String newString = Rand.EzRand.RandFromMeta(str);
            txtOutPut.Text = newString;
        }
    }
}
