using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Mining_Over_Cancer_Data_V2
{
    public class Rule
    {
        private Patient.Classification classification;
        private List<RuleItem> _ruleItems = new List<RuleItem>();
        public double Accuracy { get; set; }

        public IList<RuleItem> RuleItems { get { return _ruleItems; } }

        public Patient.Classification Classification { get { return classification; } }
        public void SetClassification(string in_classification)
        {
            if (in_classification == "Cancer")
            {
                classification = Patient.Classification.Cancer;
            }
            else if (in_classification == "Polyp")
            {
                classification = Patient.Classification.Polyp;
            }
            else if (in_classification == "Normal")
            {
                classification = Patient.Classification.Normal;
            }
            else if (in_classification == "Infection")
            {
                classification = Patient.Classification.Infection;
            }
        }
    }
}
