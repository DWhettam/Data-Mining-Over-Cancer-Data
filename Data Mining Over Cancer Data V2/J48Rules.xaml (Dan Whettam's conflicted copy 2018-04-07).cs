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
using System.IO;

namespace Data_Mining_Over_Cancer_Data
{
    /// <summary>
    /// Interaction logic for J48Rules.xaml
    /// </summary>
    public partial class J48Rules : Window
    {
        public J48Rules()
        {
            InitializeComponent();
            Print();
        }

        public void Print()
        {
            string text = File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, @"files\", "j48 -C 0.1 -M 7.txt"));
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(text);

            FlowDocument document = new FlowDocument(paragraph);
            document.IsHyphenationEnabled = false;

            flowReader.Document = document;
            flowReader.IsScrollViewEnabled = true;
            flowReader.ViewingMode = FlowDocumentReaderViewingMode.Scroll;
            flowReader.IsPrintEnabled = true;
            flowReader.IsPageViewEnabled = false;
            flowReader.IsTwoPageViewEnabled = false;
        }
    }
}
