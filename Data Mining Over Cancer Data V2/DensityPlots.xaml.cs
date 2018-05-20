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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Data_Mining_Over_Cancer_Data_V2;

namespace Data_Mining_Over_Cancer_Data
{
    /// <summary>
    /// Interaction logic for DensityPlots.xaml
    /// </summary>
    public partial class DensityPlots : Window
    {
        public DensityPlots(int patientID)
        {
            this.InitializeComponent();
            this.Model = CreateNormalDistributionModel(patientID);
            this.DataContext = this;
        }

        public PlotModel Model { get; set; }

        private static PlotModel CreateNormalDistributionModel(int patientID)
        {

            var plot = new PlotModel
            {
                Title = "Probability density function",
                Subtitle = "Patient ID: " + patientID
            };

            plot.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -0.05,
                Maximum = 20,
                MajorStep = 2,
                MinorStep = 0.5,
                TickStyle = TickStyle.Inside,
                Title = "Probability Density"                
            });
            plot.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = -0.025,
                Maximum = 0.2,
                MajorStep = 0.05,
                MinorStep = 0.005,
                TickStyle = TickStyle.Inside,
                Title = "Weighted Distances"
            });

            foreach (var item in MainWindow.classificationDistances)
            {
                string diagnosis = item.Key.ToString();
                double minValue = item.Value.Min();
                double maxValue = item.Value.Max();
                int sum = item.Value.Count();
                double mean = item.Value.Average();

                double distanceFromMeanSum = 0;
                foreach (var value in item.Value)
                {
                    distanceFromMeanSum += Math.Pow((value - mean), 2);
                }
                double variance = distanceFromMeanSum / (item.Value.Count);

                plot.Series.Add(CreateNormalDistributionSeries(diagnosis, minValue, maxValue, mean, variance, sum));
            }
            return plot;
        }

        private static LineSeries CreateNormalDistributionSeries(string diagnosis, double x0, double x1, double mean, double variance, int n)
        {
            var ls = new LineSeries
            {
                Title = string.Format("{0}:   μ={1}, σ²={2}", diagnosis, Math.Round(mean,5), Math.Round(variance, 5))
            };

            for (int i = 0; i < n; i++)
            {
                double x = x0 + ((x1 - x0) * i / (n - 1));
                double f = 1.0 / Math.Sqrt(2 * Math.PI * variance) * Math.Exp(-(x - mean) * (x - mean) / 2 / variance);
                ls.Points.Add(new DataPoint(x, f));
            }

            return ls;
        }
    }

   
}
