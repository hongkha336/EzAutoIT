using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Easy_AutoIT_Helper.FileOption
{
    static class FileHelper
    {
        public static bool IsExistFolder(String FolderPath)
        {
            return System.IO.Directory.Exists(FolderPath);
        }

        public static bool IsExistFile(String FilePath)
        {
            return System.IO.File.Exists(FilePath);
        }

        public static void OpenFolder(String FolderPath)
        {
            System.Diagnostics.Process.Start("explorer.exe", FolderPath);
        }

        public static void OpenExe(String ExePath)
        {
            System.Diagnostics.Process.Start(ExePath);
        }

        public static String OpenFileDialog()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "exe",
                Filter = "exe files (*.exe)|*.exe",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            else
            {
                return String.Empty;
            }
        }

        public static void writeFile(String fileName, String Content)
        {
            String filepath = fileName;
            FileStream fs = new FileStream(filepath, FileMode.Create);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            sWriter.WriteLine(Content);
            sWriter.Flush();
            fs.Close();
        }

        public static String OpenFileDialog1()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "ini",
                Filter = "easy autoit file (*.ini)|*.ini",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                
              
                return openFileDialog1.FileName;


            }
            else
            {
                return String.Empty;
            }
        }



        public static void writeFile(String fileName, List<Model.StepModel> list)
        {
            String filepath = fileName;
            FileStream fs = new FileStream(filepath, FileMode.Create);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8);
            sWriter.WriteLine(Global.GlobalScripting.ScriptName);
            sWriter.WriteLine(Global.GlobalScripting.ApplicationDir);
            sWriter.WriteLine("-");
            foreach (Model.StepModel model in list)
            {
                sWriter.WriteLine(model.StepId);
                sWriter.WriteLine(model.StepName);
                sWriter.WriteLine(model.Class);
                sWriter.WriteLine(model.ClassnameNN);
                sWriter.WriteLine(model.MetaData);
                sWriter.WriteLine(model.WaitDuration);
                sWriter.WriteLine(model.Description);
                sWriter.WriteLine("-");
            }
            sWriter.Flush();
            fs.Close();
        }

        public static String readCodeAuto()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\constring\\auto.txt", FileMode.Open);            StreamReader rd = new StreamReader(fs, Encoding.UTF8);            String content = rd.ReadToEnd();            rd.Close();            return content;
        }

        public static List<Model.StepModel> readDataFromFile(string filePath)
        {
        List<Model.StepModel> myList = new List<Model.StepModel>();
       
         int count  = 0;
         int takeInfor = 0;
            Model.StepModel m = new Model.StepModel();
            string[] lines = File.ReadAllLines(filePath);
            foreach (String s in lines)
         {
             if (takeInfor < 2)
             {
                 if(takeInfor == 0)
                    Global.GlobalScripting.ScriptName = s;
                 if (takeInfor == 1)
                     Global.GlobalScripting.ApplicationDir = s;
             }
             else
             {
                 if (count == 0)
                 {
                     m = new Model.StepModel();
                 }

                 if (count == 1)
                 {
                     m.StepId = Convert.ToInt32(s);
                 }

                 if (count == 2)
                 {
                     m.StepName = s;
                 }

                 if (count == 3)
                 {
                     m.Class = s;
                 }

                 if (count == 4)
                 {
                     m.ClassnameNN = s;
                 }

                 if (count == 5)
                 {
                     m.MetaData = s;
                 }

                 if (count == 6)
                 {
                     m.WaitDuration = Convert.ToDouble(s);
                 }

                 if (count == 7)
                 {
                     m.Description = s;
                     count = -1;
                     myList.Add(m);
                 }

                 count++;
             }
             takeInfor++;
         
         }

         return myList;
         

        }
    }
}
