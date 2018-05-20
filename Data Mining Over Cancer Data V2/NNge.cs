using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Data_Mining_Over_Cancer_Data_V2
{
    class NNge : Model
    {
        public override void ReadRules(string filepath)
        {
            //Regex for finding rules
            Regex regexRulesStart = new Regex(@"class [a-zA-z]+ IF : .*");
            Regex regexClassification = new Regex(@"(?<=class\s).*(?=\sIF)");
            Regex regexRuleItem = new Regex(@"([A-Za-z() _-]+)(=|<|>|<=|>=)([0-9](\.[0-9]))");            

            using (StreamReader sr = new StreamReader(filepath))
            {
                string currentLine;
                int currentRule = 0;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (regexRulesStart.Match(currentLine).Success)
                    {
                        Rules.Add(new Rule());
                        Rules[currentRule].SetClassification(regexClassification.Match(currentLine).ToString());

                        //Regex for getting elemnts of rules
                        Regex featureMatch = new Regex(pattern: @"([A-Za-z]+((( )|\.|-)*)?([(a-zA-Z)]|[0-9]*)?(( )|\.|-)?)+");
                        Regex operatorMatch = new Regex(pattern: @"<=|>=|<|>|=");
                        Regex valueMatch = new Regex(pattern: @"[0-9]+\.[0-9]+");

                        //splitting up rule items, remove initial class text
                        string[] ruleItems = currentLine.Split('^');
                        ruleItems[0] = Regex.Replace(ruleItems[0], @"class [a-zA-Z]+ IF : ", "").TrimStart();

                        foreach (var item in ruleItems)
                        {
                            MatchCollection ruleItemOperatorMatches = operatorMatch.Matches(item);
                            MatchCollection ruleItemValueMatches = valueMatch.Matches(item);
                            string op1 = ruleItemOperatorMatches[0].ToString();
                            string op2 = "";

                            if (ruleItemOperatorMatches.Count == 2 && ruleItemValueMatches.Count == 2)
                            {
                                op2 = ruleItemOperatorMatches[1].ToString();

                                if (ruleItemOperatorMatches[0].ToString() == "<=" || ruleItemOperatorMatches[0].ToString() == ">=")
                                {
                                    op1 = ruleItemOperatorMatches[0].ToString().Replace('<', '>');
                                }
                            }

                            string feature = featureMatch.Match(item).ToString();
                            RuleItem ruleItem = new RuleItem(feature);

                            double value = Convert.ToDouble(ruleItemValueMatches[0].ToString());
                            ruleItem.SetValue(value);
                            if (ruleItemValueMatches.Count > 1)
                            {
                                double value2 = Convert.ToDouble(ruleItemValueMatches[1].ToString());
                                ruleItem.SetValue(value2);
                            }
                            ruleItem.SetOp(op1);
                            if (ruleItemOperatorMatches.Count > 1)
                            {
                                ruleItem.SetOp(op2);
                            }

                            Rules[currentRule].RuleItems.Add(ruleItem);                            
                        }
                        currentRule++;
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
    }
}
