using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoItX3Lib;
using Easy_AutoIT_Helper.Model;
using Easy_AutoIT_Helper.StringConst;

namespace Easy_AutoIT_Helper.AutoItUlti
{
    class AUlti
    {

        #region Global
        String Pre_Class = String.Empty;
        String Pre_ClassNN = String.Empty;
        #endregion

        #region Main Balancing

        public void RunStep(StepModel Step)
        {

           

            if (Step.StepName.Equals(SystemString.STEP_OPEN_APP))
            {
                FileOption.FileHelper.OpenExe(Step.MetaData);
                return; 
            }
            //Click with full Class
            if(Step.StepName.Equals(SystemString.STEP_CLICK))
            {
                LeftClick(Step.Class, Step.ClassnameNN);
                return;
            }
            //Click to pos
            if (Step.StepName.Equals(SystemString.STEP_CLICK_POS))
            { 
                
                String MetaData = Step.MetaData;
                String X = String.Empty;
                while(MetaData.Length > 0 && (MetaData[0] != ',' || MetaData[0] != ' '))
                {
                    X = X + MetaData[0];
                    MetaData = MetaData.Substring(1,MetaData.Length-1);
                }

                while(X.Length > 0 && (MetaData[0] == ' ' || MetaData[0] == ','))
                {
                  MetaData = MetaData.Substring(1,MetaData.Length-1);
                }

                String Y = MetaData.Trim();
                LeftClick(Int32.Parse(X), Int32.Parse(Y));
                return;
            }

            if (Step.StepName.Equals(SystemString.STEP_FOCUS))
            {
                Focus(Step.Class, Step.ClassnameNN);
                return;
            }

            if (Step.StepName.Equals(SystemString.STEP_SETTEXT))
            {
                SendText(Step.Class, Step.ClassnameNN, Step.MetaData);
                return;
            }

            if (Step.StepName.Equals(SystemString.STEP_SETTEXT_FOCUS))
            {
                SendTextToPreviousControl(Step.MetaData);
                return;
            }

            if (Step.StepName.Equals(SystemString.STEP_SEND_KEY))
            {
                SendKey(Step.MetaData);
            }


            
        }

        #endregion

        #region Detail
        private void LeftClick(String Class, String ClassnameNN)
        {
           // Class = "[Class:" +Class + "]";
            AutoItX3 auto = new AutoItX3();
            auto.ControlClick(Class, "", ClassnameNN);

            Pre_Class = Class;
            Pre_ClassNN = ClassnameNN;
        }

        public void Active(String Title)
        {
            AutoItX3 auto = new AutoItX3();
            auto.WinActivate(Title);
        }

        private void LeftClick(int x, int y)
        {
            AutoItX3 auto = new AutoItX3();
            auto.MouseClick("LEFT", x, y, 1);
        }

        private void SendText(String Class, String ClassNN ,String Text)
        {
         //   Class = "[Class:" +Class + "]";
            AutoItX3 auto = new AutoItX3();
            auto.ControlSetText(Class, "", ClassNN, Text);
        }

   
        private void SendTextToPreviousControl(String Text)
        {
            AutoItX3 auto = new AutoItX3();
            auto.ControlSetText(Pre_Class, "", Pre_ClassNN, Text);
        }

        private void Focus(String Class, String ClassNN)
        {
         //   Class = "[Class:" + Class + "]";
            AutoItX3 auto = new AutoItX3();
            auto.ControlFocus(Class, "", ClassNN);
        }

        private void SendKey(String key)
        {
            AutoItX3 auto = new AutoItX3();
            auto.Send(key);
        }
        #endregion
    }
}
