using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Easy_AutoIT_Helper.StringConst
{
    static class SystemString
    {
       public static String AutoItMainFolder = @"C:\Program Files (x86)\AutoIt3";
       public static String AutoItInfor = @"C:\Program Files (x86)\AutoIt3\Au3Info.exe";
       public static String AutoItSetUp = Application.StartupPath + "\\autoit\\setup.exe";


       public static String STEP_CLICK = "CLICK";
       public static String STEP_SETTEXT = "SET_TEXT";
       public static String STEP_FOCUS = "FOCUS";
       public static String STEP_SETTEXT_FOCUS = "SET_TEXT_TO_THE_FOCUS_CONTROL";
       public static String STEP_CLICK_POS = "CLICK_TO_POS";
       public static String STEP_SEND_KEY = "SEND_KEY";
       public static String STEP_OPEN_APP = "OPEN_APP";

       
    }
}
