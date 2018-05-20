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
using System.Windows.Shapes;
using Data_Mining_Over_Cancer_Data;
namespace Data_Mining_Over_Cancer_Data_V2
{
    /// <summary>
    /// Interaction logic for More_Detail.xaml
    /// </summary>
    public partial class More_Detail : Window
    {
        int id = 0;
        public More_Detail(int patientID, Dictionary<Patient.Classification, List<double>> classificationDistances)
        {            
            InitializeComponent();
            id = patientID;
            PatientID.Content = patientID;

            //Displaying Min, Max and Mean distances to each target label
            foreach (var item in classificationDistances)
            {
                if (item.Key == Patient.Classification.Cancer)
                {
                    CancerMin.Content = Math.Round(item.Value.Min(), 6).ToString();
                    CancerMax.Content = Math.Round(item.Value.Max(), 6).ToString();
                    CancerMean.Content = Math.Round(item.Value.Average(), 6).ToString();
                }
                else if (item.Key == Patient.Classification.Polyp)
                {
                    PolypMin.Content = Math.Round(item.Value.Min(), 6).ToString();
                    PolypMax.Content = Math.Round(item.Value.Max(), 6).ToString();
                    PolypMean.Content = Math.Round(item.Value.Average(), 6).ToString();
                }
                else if (item.Key == Patient.Classification.Infection)
                {
                    InfectionMin.Content = Math.Round(item.Value.Min(), 6).ToString();
                    InfectionMax.Content = Math.Round(item.Value.Max(), 6).ToString();
                    InfectionMean.Content = Math.Round(item.Value.Average(), 6).ToString();
                }
                else if (item.Key == Patient.Classification.Normal)
                {
                    NormalMin.Content = Math.Round(item.Value.Min(), 6).ToString();
                    NormalMax.Content = Math.Round(item.Value.Max(), 6).ToString();
                    NormalMean.Content = Math.Round(item.Value.Average(), 6).ToString();
                }
            }
        }

        /// <summary>
        /// Displays Box Plot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxPotButton_Click(object sender, RoutedEventArgs e)
        {
            DistancePlots plots = new DistancePlots(id);
            plots.Show();
        }

        private void DensityPlotButton_Click(object sender, RoutedEventArgs e)
        {            
            DensityPlots plots = new DensityPlots(id);
            plots.Show();
        }
    }


}
