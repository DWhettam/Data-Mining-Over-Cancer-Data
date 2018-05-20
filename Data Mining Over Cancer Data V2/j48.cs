using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Data_Mining_Over_Cancer_Data_V2
{
    class J48:Model
    {       
        public override void ReadRules(string filepath)
        {
            //super crazy regex that should match any j48 rule
            Regex regexRuleStart = new Regex(@"([A-Za-z() |\.-])+(<|>|=|<=|>=) [0-9]+(\.[0-9]+)?(: [a-zA-Z]+ \([0-9]+(\.[0-9]+)?(\/[0-9]+(\.[0-9]+)?\))?)?");            

            using (StreamReader sr = new StreamReader(filepath))
            {
                string currentLine;
                int ruleLevel = 0;
                int previousRuleLevel = -1;
                int currentRule = 0;
                Rules.Add(new Rule());

                while ((currentLine = sr.ReadLine()) != null)
                {                    
                    if (regexRuleStart.Match(currentLine).Success)
                    {
                        ruleLevel = currentLine.Count(c => c == '|');

                        //regex to match feature, operator, value, classification and accruacy of a rule
                        Regex featureMatch = new Regex(@"([A-Za-z]+((( )|\.|-)*)?([(a-zA-z)]|[0-9]*)?(( )|\.|-)?)+");
                        Regex operatorMatch = new Regex(@"<=|>=|<|>|=");
                        Regex valueMatch = new Regex(@"[0-9]+(\.[0-9]+)?");
                        
                        string feature = featureMatch.Match(currentLine).ToString().Trim();                        
                        double value = Convert.ToDouble(valueMatch.Match(currentLine).ToString());
                        RuleItem ruleItem = new RuleItem(feature);
                        ruleItem.SetOp(operatorMatch.Match(currentLine).ToString().Trim());
                        ruleItem.SetValue(Convert.ToDouble(valueMatch.Match(currentLine).ToString()));

                        //If rule level is increasing, the ruleitem is part of the current rule. If not, create a new rule
                        if (ruleLevel > previousRuleLevel)
                        {
                            Rules[currentRule].RuleItems.Add(ruleItem);
                        }
                        else
                        {
                            currentRule++;
                            Rules.Add(new Rule());
                            for (int i = 0; i < ruleLevel; i++)
                            {                                
                                Rules[currentRule].RuleItems.Add(Rules[currentRule-1].RuleItems[i]);
                            }
                            Rules[currentRule].RuleItems.Add(ruleItem);                            
                        }

                        SetClassificationAccuracy(currentLine, currentRule);         
                        previousRuleLevel = ruleLevel;
                    }
                    if (currentLine.Contains("Correctly Classified Instances"))
                    {
                        string accuracy = regexAccuracy.Match(currentLine).ToString();
                        Accuracy = Convert.ToDouble(accuracy.Remove(accuracy.Length - 1));
                    }
                }
                sr.Close();
            }
        }

        public void SetClassificationAccuracy(string currentLine, int currentRule)
        {
            //Regex for getting classification and accuracy for each rule
            Regex classificationAccuracyMatch = new Regex(@": [a-zA-Z]+ \([0-9]+(\.[0-9]+)?\/[0-9]+(\.[0-9])?\)");
            Regex classificationMatch = new Regex(@"[a-zA-Z]+");
            Regex accuracyMatch = new Regex(@"[0-9]+(\.[0-9])?\/[0-9]+(\.[0-9])?");
            Regex val1AccuracyMatch = new Regex(@"([0-9]+\.[0-9])?");

            //setting rule classification
            Rules[currentRule].SetClassification(classificationMatch.Match(classificationAccuracyMatch.Match(currentLine).ToString()).ToString());

            //setting accuracy
            double val1 = 0, val2 = 0;
            string accuracyString = accuracyMatch.Match(classificationAccuracyMatch.Match(currentLine).ToString()).ToString();
            if (accuracyString != "")
            {
                val1 = Convert.ToDouble(val1AccuracyMatch.Match(accuracyString).ToString());
                accuracyString = Regex.Replace(accuracyString, @"([0-9]+\.[0-9])\/", "");
                val2 = Convert.ToDouble(accuracyString);

                Rules[currentRule].Accuracy = ((val1 - val2) / val1) * 100;
            }

        }
    }
}
