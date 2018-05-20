using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Mining_Over_Cancer_Data_V2
{
    public class Patient
    {
        private Dictionary<int, string> PatientAttributes = new Dictionary<int, string>();
        public enum Classification
        {
            Cancer,
            Polyp,
            Infection,
            Normal,
            Unknown
        };

        public Classification GivenDiagnosis { get; set; }
        public Classification Diagnosis { get; set; }
        public Classification J48Diagnosis { get; set; }
        public Classification NngeDiagnosis { get; set; }
        public Classification EuclidDiagnosis { get; set; }

        public int ID { get; set; }   

        //---------------------Getting and Setting Attributes + Features----------------------\\
        public void SetAttributes(string[] attributes)
        {
            this.PatientAttributes.Clear();
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i] = string.Format("{0:G29}", decimal.Parse(attributes[i]));                
                this.PatientAttributes.Add(i, attributes[i]);
            }
        }
        public void SetSingleAttribute(int key, double value)
        {
            PatientAttributes[key] = value.ToString();
        }
        public Dictionary<int, string> GetAllAttributes()
        {
            return PatientAttributes;
        }
        public double GetSingleAttribute(int key)
        {
            return Convert.ToDouble(PatientAttributes[key]);
        }
    }
}
