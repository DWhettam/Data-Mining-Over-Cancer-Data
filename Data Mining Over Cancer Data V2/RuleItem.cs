using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Mining_Over_Cancer_Data_V2
{
    public class RuleItem
    {
        public enum Operators
        {
            Equal,
            LessOrEqual,
            GreatorOrEqual,
            Less,
            Greater
        }
        private List<Operators> op = new List<Operators>();
        private List<double> Value = new List<double>();
        private string feature;

        public List<Operators> Op { get => op; }
        public string Feature { get => feature; set => feature = value; }

        public RuleItem(string feature) => Feature = feature;

        public List<double> GetValues()
        {
            return Value;
        }
        public void SetValue(double value)
        {
            Value.Add(value);
        }

        public List<Operators> GetOps()
        {
            return op;
        }
        public void SetOp(string opsymbol)
        {
            if (opsymbol == "=")
            {
                op.Add(Operators.Equal);
            }
            else if (opsymbol == "<=")
            {
                op.Add(Operators.LessOrEqual);
            }
            else if (opsymbol == ">=")
            {
                op.Add(Operators.GreatorOrEqual);
            }
            else if (opsymbol == "<")
            {
                op.Add(Operators.Less);
            }
            else if (opsymbol == ">")
            {
                op.Add(Operators.Greater);
            }
        }

    }
}
