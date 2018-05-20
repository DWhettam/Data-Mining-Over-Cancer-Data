using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Mining_Over_Cancer_Data_V2
{
    public class AttributeRanking
    {
        private double weight;
        private int number;

        public void SetAttributeRanking(string[] newAttr)
        {
            weight = Convert.ToDouble(newAttr[0]);
            number = Convert.ToInt32(newAttr[1]);
        }

        public double GetWeight()
        {
            return weight;
        }

        public int GetNumber()
        {
            return number;
        }
    }
}
