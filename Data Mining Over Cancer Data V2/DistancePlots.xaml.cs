using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using Data_Mining_Over_Cancer_Data_V2;

namespace Data_Mining_Over_Cancer_Data
{
    /// <summary>
    /// Interaction logic for DistancePlots.xaml
    /// </summary>
    public partial class DistancePlots : Window
    {
        public DistancePlots(int patientID)
        {
            InitializeComponent();
            DataContext = this;            

            DistanceBoxPlotSeries plots = new DistanceBoxPlotSeries();
            this.MyModel = plots.CreateBoxPlot(patientID);            
        }
        public PlotModel MyModel { get; private set; }
    }
    public class Item
    {
        #region Public Properties

        public string Label { get; set; }

        #endregion Public Properties
    }

    public class DistanceBoxPlotSeries
    {      
        #region Public Methods

        public PlotModel CreateBoxPlot(int patientID)
        {
            var plot = new PlotModel
            {
                Title = "Box Plot",
                Subtitle = "Patient ID: " + patientID
            };
            var items = new Collection<Item>();

            foreach (var item in MainWindow.classificationDistances)
            {
                items.Add(new Item { Label = item.Key.ToString() });
            }

            plot.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                MajorStep = 0.025,
                MinorStep = 0.005,
                Key = "Distance",
                TickStyle = TickStyle.Crossing,
                AbsoluteMaximum = 1,
                AbsoluteMinimum = -0.25
            });

            plot.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                ItemsSource = items,
                LabelField = "Label",                 
                Key = "Classification",
                IsTickCentered = true,
                TickStyle = TickStyle.None,
                IsZoomEnabled = false
            });
          

            var s1 = new BoxPlotSeries
            {
                Fill = OxyColor.FromRgb(0x1e, 0xb4, 0xda),
                StrokeThickness = 1.1,
                WhiskerWidth = 1
            };

            double x = 0;
            foreach (var item in MainWindow.classificationDistances)
            {                
                var values = new List<double>();
                foreach (var value in item.Value)
                {
                    values.Add(value);
                }

                values.Sort();
                var median = getMedian(values);
                int r = values.Count % 2;
                double firstQuartil = getMedian(values.Take((values.Count + r) / 2)); // 25%-Quartil
                double thirdQuartil = getMedian(values.Skip((values.Count - r) / 2)); // 75%-Quartil

                var iqr = thirdQuartil - firstQuartil; // Quartilabstand
                var step = 1.5 * iqr;
                var upperWhisker = thirdQuartil + step;
                upperWhisker = values.Max();
                var lowerWhisker = firstQuartil - step;
                lowerWhisker = values.Min();
                var outliers = values.Where(v => v > upperWhisker || v < lowerWhisker).ToList();

                s1.Items.Add(new BoxPlotItem(x, lowerWhisker, firstQuartil, median, thirdQuartil, upperWhisker));
                x++;
            }
            
            plot.Series.Add(s1);
            return plot;
        }

        #endregion Public Methods

        #region Private Methods

        private static double getMedian(IEnumerable<double> values)
        {
            var sortedInterval = new List<double>(values);
            sortedInterval.Sort();
            var count = sortedInterval.Count;
            if (count % 2 == 1)
            {
                return sortedInterval[(count - 1) / 2];
            }

            return 0.5 * sortedInterval[count / 2] + 0.5 * sortedInterval[(count / 2) - 1];
        }

        #endregion Private Methods
    }
}
