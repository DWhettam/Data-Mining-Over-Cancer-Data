using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Data_Mining_Over_Cancer_Data_V2
{
    public abstract class Model
    {
        public Regex regexAccuracy = new Regex(@"([0-9]+.[0-9]+)(?:.(?![0-9]+))+$");

        public List<Rule> Rules = new List<Rule>();
        public abstract void ReadRules(string filePath);
        public double Accuracy { get; set; }
    }
}
