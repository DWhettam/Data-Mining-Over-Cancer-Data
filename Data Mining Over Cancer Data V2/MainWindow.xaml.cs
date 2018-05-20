using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using Data_Mining_Over_Cancer_Data;

namespace Data_Mining_Over_Cancer_Data_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /// <summary>
    /// Patient class
    /// </summary>


    public partial class MainWindow : Window
    {
        //----------------------------------------------DATA + FILEPATHS---------------------------------------\\
        //Weka Inputs
        public static List<Patient> dataList = new List<Patient>();
        public static List<Patient> inputList = new List<Patient>();
        public static List<AttributeRanking> rankingList = new List<AttributeRanking>();
        public static List<string> patientFeatures = new List<string>();

        //FilePaths
        public static string dataFile = System.IO.Path.Combine(Environment.CurrentDirectory, @"files\", "Final.csv");
        public static string inputFile = System.IO.Path.Combine(Environment.CurrentDirectory, @"files\", "Input.csv");
        public static string rankingFile = System.IO.Path.Combine(Environment.CurrentDirectory, @"files\", "AttributeWeightings.csv");
        public static string j48File = System.IO.Path.Combine(Environment.CurrentDirectory, @"files\", "j48 -C 0.1 -M 7.txt");
        public static string nngeFile = System.IO.Path.Combine(Environment.CurrentDirectory, @"files\", "NNge -unchanged.txt");

        public static Dictionary<Patient.Classification, List<double>> classificationDistances = new Dictionary<Patient.Classification, List<double>>();
        public static List<Model> wekaModels = new List<Model>();

        public static List<int> nngeClassifiedRule = new List<int>();
        public static List<int> j48ClassifiedRule = new List<int>();
        //------------------------------------------MAIN PROGRAM------------------------------------------------\\
        public MainWindow()
        {            
            InitializeComponent();
            ReadData();
            NormaliseData();
            ReadRules();
            txtNum.Text = inputList[0].ID.ToString();
            GenerateToolTips();
        }

        #region Classification
        private Patient.Classification WeightedEuclidClassify()
        {
            List<Patient> cancerData = dataList.Where(C => C.Diagnosis == Patient.Classification.Cancer).ToList();
            List<Patient> infectionData = dataList.Where(C => C.Diagnosis == Patient.Classification.Infection).ToList();
            List<Patient> polypData = dataList.Where(C => C.Diagnosis == Patient.Classification.Polyp).ToList();
            List<Patient> normalData = dataList.Where(C => C.Diagnosis == Patient.Classification.Normal).ToList();
            List<List<Patient>> completeData = new List<List<Patient>>
            {
                cancerData,
                infectionData,
                polypData,
                normalData
            };

            double diff = 0;
            double euclid = 0;
            double weightedEuclid = 0;

            //getting index of selected input patient
            int inputId = Convert.ToInt32(txtNum.Text);
            int inputIndex = inputList.FindIndex(x => x.ID == inputId);

            foreach (var dataSet in completeData)
            {
                List<double> weightedDistances = new List<double>();
                List<double> distances = new List<double>();
                for (int data = 0; data < dataSet.Count; data++)
                {
                    weightedEuclid = 0;
                    euclid = 0;
                    for (int attr = 1; attr < rankingList.Count; attr++)
                    {
                        foreach (var item in rankingList)
                        {
                            if (item.GetNumber() == attr)
                            {
                                double weight = item.GetWeight();
                                double inputVal = inputList[inputIndex].GetSingleAttribute(attr - 1);
                                double dataVal = dataSet[data].GetSingleAttribute(attr - 1);

                                diff = inputVal - dataVal;
                                euclid += Math.Pow(diff, 2);
                                weightedEuclid += weight * Math.Sqrt(Math.Pow(diff, 2));
                                break;
                            }
                        }
                    }
                    distances.Add(Math.Abs(Math.Sqrt(euclid)));
                    weightedDistances.Add(Math.Abs(weightedEuclid));
                }
                //List<double> weightedDistances_2 = weightedDistances;                
                classificationDistances.Add(dataSet[0].Diagnosis, weightedDistances);
            }

            //getting smallest mean distance
            double minMean = int.MaxValue;
            Patient.Classification closestAverageClassification = new Patient.Classification();
            foreach (var item in classificationDistances)
            {
                if (item.Value.Average() < minMean)
                {
                    closestAverageClassification = item.Key;
                    minMean = item.Value.Average();
                }
            }

            inputList[int.Parse(txtNum.Text) - 1].EuclidDiagnosis = closestAverageClassification;
            return closestAverageClassification;
        }
        private Patient.Classification PerfectMatch()
        {
            //getting index of selected input patient
            string input = txtNum.Text;
            int inputId = Convert.ToInt32(input);
            int inputIndex = inputList.FindIndex(x => x.ID == inputId);
            Patient.Classification classification = Patient.Classification.Unknown;

            for (int data = 0; data < dataList.Count; data++)
            {
                int test = (dataList[data].GetAllAttributes().Where(entry => inputList[inputIndex].GetAllAttributes()[entry.Key] != entry.Value).ToDictionary(entry => entry.Key, entry => entry.Value)).Count;
                if ((dataList[data].GetAllAttributes().Where(entry => inputList[inputIndex].GetAllAttributes()[entry.Key] != entry.Value).ToDictionary(entry => entry.Key, entry => entry.Value)).Count == 0)
                {
                    perfectMatchLabel.Visibility = Visibility.Visible;
                    perfectMatch.Visibility = Visibility.Visible;

                    if (typeof(Patient.Classification).IsEnumDefined(Convert.ToString(dataList[data].Diagnosis)))
                    {
                        inputList[inputIndex].Diagnosis = (Patient.Classification)Enum.Parse(typeof(Patient.Classification), Convert.ToString(dataList[data].Diagnosis));
                    }

                    classification = inputList[inputIndex].Diagnosis;
                }
            }
            return classification;
        }
        private List<Patient.Classification> WekaClassify()
        {
            //getting index of selected input patient
            int inputId = Convert.ToInt32(txtNum.Text);
            int inputIndex = inputList.FindIndex(x => x.ID == inputId);
            Patient selectedPatient = inputList[inputIndex];
            List<Patient.Classification> j48Classification = new List<Patient.Classification>();
            List<Patient.Classification> nngeClassification = new List<Patient.Classification>();
            j48ClassifiedRule.Clear();
            nngeClassifiedRule.Clear();
            double j48Accuracy = 0;

            //iterating through models
            for (int modeli = 0; modeli < wekaModels.Count; modeli++)
            {
                if (modeli == 0)
                {
                    inputList[inputIndex].J48ModelAccuracy = wekaModels[modeli].Accuracy;
                }
                else
                {
                    inputList[inputIndex].NNgeAccuracy = wekaModels[modeli].Accuracy;
                }

                //iterating through model rules
                for (int rulei = 0; rulei < wekaModels[modeli].Rules.Count; rulei++)
                {
                    bool match = false;
                    //iterating through ruleitems
                    for (int ruleItemi = 0; ruleItemi < wekaModels[modeli].Rules[rulei].RuleItems.Count; ruleItemi++)
                    {
                        RuleItem ruleItem = wekaModels[modeli].Rules[rulei].RuleItems[ruleItemi];
                        int attributeIndex = patientFeatures.IndexOf(ruleItem.Feature) - 1;
                        double patientValue = selectedPatient.GetSingleAttribute(attributeIndex);
                        List<double> ruleVals = ruleItem.GetValues();
                        List<RuleItem.Operators> ruleOps = ruleItem.GetOps();

                        for (int i = 0; i < ruleVals.Count; i++)
                        {
                            if (ruleOps[i] == RuleItem.Operators.Equal)
                            {
                                if (patientValue == ruleVals[i])
                                {
                                    match = true;
                                }
                                else
                                {
                                    match = false;
                                    break;
                                }
                            }
                            else if (ruleOps[i] == RuleItem.Operators.Less)
                            {
                                if (patientValue < ruleVals[i])
                                {
                                    match = true;
                                }
                                else
                                {
                                    match = false;
                                    break;
                                }
                            }
                            else if (ruleOps[i] == RuleItem.Operators.Greater)
                            {
                                if (patientValue > ruleVals[i])
                                {
                                    match = true;
                                }
                                else
                                {
                                    match = false;
                                    break;
                                }
                            }
                            else if (ruleOps[i] == RuleItem.Operators.LessOrEqual)
                            {
                                if (patientValue <= ruleVals[i])
                                {
                                    match = true;
                                }
                                else
                                {
                                    match = false;
                                    break;
                                }
                            }
                            else if (ruleOps[i] == RuleItem.Operators.GreatorOrEqual)
                            {
                                if (patientValue >= ruleVals[i])
                                {
                                    match = true;
                                }
                                else
                                {
                                    match = false;
                                    break;
                                }
                            }
                        }
                        if (!match)
                        {
                            break;
                        }
                    }
                    if (match)
                    {
                        if (modeli == 0)
                        {
                            j48Accuracy = wekaModels[modeli].Rules[rulei].Accuracy;
                            inputList[inputIndex].J48RuleAccuracy = j48Accuracy;
                            j48Classification.Add(wekaModels[modeli].Rules[rulei].Classification);                            
                            j48ClassifiedRule.Add(rulei);
                        }
                        else
                        {
                            nngeClassification.Add(wekaModels[modeli].Rules[rulei].Classification);                            
                            nngeClassifiedRule.Add(rulei);
                        }
                    }
                }
            }
            if (j48Classification.Count > 0)
            {
                inputList[inputIndex].J48Diagnosis = MostFrequentClass(j48Classification);
            }
            else
            {
                inputList[inputIndex].J48Diagnosis = Patient.Classification.Unknown;
            }
            if (nngeClassification.Count > 0)
            {
                inputList[inputIndex].NngeDiagnosis = MostFrequentClass(nngeClassification);
            }
            else
            {
                inputList[inputIndex].NngeDiagnosis = Patient.Classification.Unknown;
            }

            return new List<Patient.Classification> { inputList[inputIndex].J48Diagnosis, inputList[inputIndex].NngeDiagnosis };
        }

        private Patient.Classification RecommendedClassification(Patient.Classification distanceClassification, Patient.Classification perfectMatch, List<Patient.Classification> weka)
        {
            List<Patient.Classification> classifications = new List<Patient.Classification>
            {
                weka[0],
                weka[1],
                distanceClassification
            };
            if (perfectMatch != Patient.Classification.Unknown)
            {
                classifications.Add(perfectMatch);
            }

            Patient.Classification recommendation = MostFrequentClass(classifications);

            inputList[int.Parse(txtNum.Text) - 1].Diagnosis = recommendation;
            return recommendation;
        }

        private Patient.Classification MostFrequentClass(List<Patient.Classification> classifications)
        {
            var frequency = classifications.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            Patient.Classification recommendation;

            //If no majority diagnosis, diagnose in order of severity.
            if (frequency.Values.Distinct().Count() == 1 && frequency.ContainsValue(1))
            {
                if (classifications.Contains(Patient.Classification.Cancer))
                {
                    recommendation = Patient.Classification.Cancer;
                }
                else if (classifications.Contains(Patient.Classification.Polyp))
                {
                    recommendation = Patient.Classification.Polyp;
                }
                else if (classifications.Contains(Patient.Classification.Infection))
                {
                    recommendation = Patient.Classification.Infection;
                }
                else if (classifications.Contains(Patient.Classification.Normal))
                {
                    recommendation = Patient.Classification.Normal;
                }
                else
                {
                    recommendation = Patient.Classification.Unknown;
                }
            }
            else
            {
                //finds majority diagnosis
                recommendation = (from item in classifications
                                  group item by item into g
                                  orderby g.Count() descending
                                  select g.Key).First();
            }

            return recommendation;
        }


        #endregion

        #region Handling Input    
        private void ReadData()
        {
            List<string> fileNames = new List<string>
            {
                dataFile,
                inputFile,
                rankingFile
            };

            //loop over all files
            foreach (var file in fileNames)
            {
                //reading file
                using (StreamReader sr = new StreamReader(file))
                {
                    string currentLine;
                    int lineCount = 0;
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        string[] inputValues = currentLine.Split(',');
                        List<string> features = new List<string>();
                        string classification = "";

                        if (file.ToLower().Contains("final"))
                        {
                            classification = inputValues[inputValues.Length - 1];

                            if (lineCount == 0)
                            {
                                for (int i = 0; i < inputValues.Length; i++)
                                {
                                    features.Add(inputValues[i]);
                                }
                                patientFeatures = features;
                            }

                        }
                        if (file.ToLower().Contains("input"))
                        {
                            classification = inputValues[inputValues.Length - 1];
                        }

                        if ((file.ToLower().Contains("final") || file.ToLower().Contains("input")) && lineCount > 0)
                        {
                            for (int i = 0; i < inputValues.Length; i++)
                            {
                                if (inputValues[i].ToLower() == "yes")
                                {
                                    inputValues[i] = "1";
                                }
                                else if (inputValues[i].ToLower() == "no")
                                {
                                    inputValues[i] = "0";
                                }
                                else if (patientFeatures[i].ToLower().Contains("num") && (inputValues[i] != "0" && inputValues[i] != "1"))
                                {
                                    if (file.ToLower().Contains("final"))
                                    {
                                        MessageBox.Show("Invalid data file");
                                    }
                                    else if (file.ToLower().Contains("input"))
                                    {
                                        MessageBox.Show("Invalid input file");                                        
                                    }
                                }
                            }
                        }
                        inputValues = inputValues.Where(x => double.TryParse(x, out double temp)).ToArray();
                        AddPatient(inputValues, file, classification);
                        lineCount++;
                    }
                    sr.Close();
                }
            }

        }
        private void ReadRules()
        {
            List<string> classifierFileNames = new List<string>
            {
                j48File,
                nngeFile
            };
            J48 j48Model = new J48();
            NNge nngeModel = new NNge();

            foreach (var file in classifierFileNames)
            {
                //Checking if j48 or nnge file
                if (file.Contains("j48"))
                {
                    j48Model.ReadRules(file);
                    wekaModels.Add(j48Model);
                    //inputList[int.Parse(txtNum.Text) - 1].J48ModelAccuracy = j48Model.Accuracy;
                }
                else
                {
                    nngeModel.ReadRules(file);
                    wekaModels.Add(nngeModel);
                    //inputList[int.Parse(txtNum.Text) - 1].NNgeAccuracy = nngeModel.Accuracy;
                }
            }

        }
        private void NormaliseData()
        {
            List<List<Patient>> dataLists = new List<List<Patient>>
            {
                dataList,
                inputList
            };
            List<Patient> dataListCopy = new List<Patient>();
            List<Patient> inputListCopy = new List<Patient>();

            foreach (var item in dataList)
            {
                Patient newPatient = new Patient();
                newPatient.SetAttributes(item.GetAllAttributes().Values.ToArray());
                dataListCopy.Add(newPatient);
            }
            int count = 0;
            foreach (var data in dataLists)
            {               
                for (int feature = 0; feature < patientFeatures.Count - 2; feature++)
                {
                    List<double> featureValues = new List<double>();
                    foreach (var item in dataListCopy)
                    {
                        featureValues.Add(item.GetSingleAttribute(feature));
                    }
                    double minValue = featureValues.Min();
                    double maxValue = featureValues.Max();
                    for (int patient = 0; patient < data.Count; patient++)
                    {
                        //if value should be normalised and isn't -> normalise
                        double currentValue = data[patient].GetSingleAttribute(feature);
                        if ((maxValue > 1 && count == 0) || (currentValue >= 1 && count == 1 ))
                        {
                            double normalisedValue = Math.Round(((currentValue - minValue) / (maxValue - minValue)), 4);
                            data[patient].SetSingleAttribute(feature, normalisedValue);
                        }
                    }
                }
                count++;
            }

        }
        public static void AddPatient(string[] newPatient, string file, string classification)
        {
            if (newPatient.Length > 0)
            {
                Patient patient = new Patient();
                patient.SetAttributes(newPatient.Skip(1).ToArray());

                if (file.Contains("Input"))
                {
                    patient.ID = Convert.ToInt32(newPatient[0]);

                    if (typeof(Patient.Classification).IsEnumDefined(classification))
                    {
                        patient.GivenDiagnosis = (Patient.Classification)Enum.Parse(typeof(Patient.Classification), classification);
                    }
                    else
                    {
                        patient.GivenDiagnosis = Patient.Classification.Unknown;
                    }

                    inputList.Add(patient);
                }
                else if (file.Contains("Attribute"))
                {
                    AttributeRanking attr = new AttributeRanking();
                    attr.SetAttributeRanking(newPatient);
                    rankingList.Add(attr);
                }
                else
                {
                    patient.ID = Convert.ToInt32(newPatient[0]);

                    if (typeof(Patient.Classification).IsEnumDefined(classification))
                    {
                        patient.Diagnosis = (Patient.Classification)Enum.Parse(typeof(Patient.Classification), classification);
                    }

                    dataList.Add(patient);
                }
            }
        }
        #endregion

        #region Handling UI
        private void UpdateUI(Patient.Classification distanceClassification, Patient.Classification perfectMatchClassification, Patient.Classification j48Classification, Patient.Classification nngeClassification, Patient.Classification recommendation, Patient.Classification givenClassification)
        {
            DistanceResult.Content = distanceClassification;
            j48Result.Content = j48Classification;
            NNgeResult.Content = nngeClassification;
            RecommendedDiagnosis.Content = recommendation;
            perfectMatch.Content = perfectMatchClassification;

            if (givenClassification != Patient.Classification.Unknown)
            {
                givenDiagnosis.Content = givenClassification;
            }
        }
        private void ClearUI()
        {
            j48Result.Content = "";
            NNgeResult.Content = "";
            perfectMatchLabel.Visibility = Visibility.Hidden;
            perfectMatch.Visibility = Visibility.Hidden;
            givenDiagnosisLabel.Visibility = Visibility.Hidden;
            givenDiagnosis.Visibility = Visibility.Hidden;
        }
        private void GenerateToolTips()
        {
            ToolTip j48TT = new ToolTip();
            j48TT.Content = "J48 is a decision tree algorithm used for classification. \r\nIt is one one of the most popular and widely used data mining algorithms. \r\nSee README.md for more details";
            j48label.ToolTip = j48TT;

            ToolTip nngeTT = new ToolTip();
            nngeTT.Content = "NNge is a nearest-neighbour-like algorithm that has had wide use in the clinical domain. \r\nSee README.md for more details";
            nngelabel.ToolTip = nngeTT;

            ToolTip distanceTT = new ToolTip();
            distanceTT.Content = "Weighted Distance is a measure of similarity of your input to the patients in our dataset. \r\nThe relative importance of each attribute is considered to provide a highly relevent metric. \r\nSee README.md for more details";
            distancelabel.ToolTip = distanceTT;

            ToolTip recommendationTT = new ToolTip();
            recommendationTT.Content = "It is vital you use your clinical expertise to inform your decision before this recommendation. \r\nOur recommendation considers the above metrics in order of severity. \r\nProviding a recommendation that minimises false negatives.";
            recommendationLabel.ToolTip = recommendationTT;

            ToolTip givenDiagnosisTT = new ToolTip();
            givenDiagnosisTT.Content = "The provided diagnosis for this patient is as follows. \r\nYou can use this to check the validity of our recommendations.";
            givenDiagnosisLabel.ToolTip = givenDiagnosisTT;

            ToolTip perfectMatchTT = new ToolTip();
            perfectMatchTT.Content = "The inputted patient perfectly matches a patient in our database. \r\nThis may be indicative of a mistake when entering your patient into the system";
            perfectMatchLabel.ToolTip = perfectMatchTT;
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearUI();
            classificationDistances.Clear();
            Patient.Classification distanceClassification = WeightedEuclidClassify();
            Patient.Classification perfectMatchClassification = PerfectMatch();
            List<Patient.Classification> wekaClassifications = WekaClassify();
            Patient.Classification recommendation = RecommendedClassification(distanceClassification, perfectMatchClassification, wekaClassifications);
            Patient.Classification givenClassification = Patient.Classification.Unknown;

            if (inputList[int.Parse(txtNum.Text) - 1].GivenDiagnosis != Patient.Classification.Unknown)
            {
                givenClassification = inputList[int.Parse(txtNum.Text) - 1].GivenDiagnosis;
                givenDiagnosisLabel.Visibility = Visibility.Visible;
                givenDiagnosis.Visibility = Visibility.Visible;
            }

            UpdateUI(distanceClassification, perfectMatchClassification, wekaClassifications[0], wekaClassifications[1], recommendation, givenClassification);
        }
        private void MoreDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecommendedDiagnosis.Content.ToString() == "None")
            {
                MessageBox.Show("Please generate a diagnosis. More detail cannot be viewed if a diagnosis is not present");
            }
            else
            {
                More_Detail moreDetail = new More_Detail(int.Parse(txtNum.Text), classificationDistances);
                moreDetail.Show();
            }
        }

        //--------------------------------Patient Selection-------------------------------\\
        private int _numValue = 0;
        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                if (_numValue >= inputList.Count())
                {
                    _numValue = 0;
                }
                else if (_numValue < 0)
                {
                    _numValue = inputList.Count() - 1;
                }
                txtNum.Text = inputList[_numValue].ID.ToString();
            }
        }

        public void NumberUpDown()
        {
            InitializeComponent();
            txtNum.Text = _numValue.ToString();
        }

        private void CmdUp_Click(object sender, RoutedEventArgs e) => NumValue++;

        private void CmdDown_Click(object sender, RoutedEventArgs e) => NumValue--;

        #endregion


    }
}
