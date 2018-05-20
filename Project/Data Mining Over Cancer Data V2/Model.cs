using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Mining_Over_Cancer_Data_V2
{
    abstract class Model
    {
        public List<Rule> Rules = new List<Rule>();
        public abstract void ReadRules(string filePath);
    }
}
