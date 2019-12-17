using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy_AutoIT_Helper.Model
{
    public class StepModel
    {
        public int StepId { get; set; }
        public String StepName { get; set; }
        public String Class { get; set; }
        public String ClassnameNN { get; set; }
        public String MetaData { get; set; }
        public Double WaitDuration { get; set; }

        public String Description { get; set; }

        public StepModel(int StepId, String StepName,String Class, String ClassnameNN, String Description, Double duration)
        {
            this.StepId = StepId;
            this.StepName = StepName;
            this.ClassnameNN = ClassnameNN;
            this.Description = Description;
            this.WaitDuration = duration;
            this.Class = Class;
        }

        public StepModel()
        {
        }
    }
}
