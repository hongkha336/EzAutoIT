using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Easy_AutoIT_Helper
{
    public partial class frmIntro : Form
    {

       
        public frmIntro(int Flag)
        {
            InitializeComponent();
            if (Flag == 1)
            {
                pictureBox1.Image = Image.FromFile("sourcepic\\s1.png");
            }

            if (Flag == 2)
            {
                pictureBox1.Image = Image.FromFile("sourcepic\\s2.png");
            }
        }

        
    }
}
