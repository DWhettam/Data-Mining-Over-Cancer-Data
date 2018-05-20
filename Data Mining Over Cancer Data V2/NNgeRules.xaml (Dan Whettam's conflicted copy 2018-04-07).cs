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
using System.Drawing;
using System.Drawing.Printing;

namespace Data_Mining_Over_Cancer_Data
{
    /// <summary>
    /// Interaction logic for NNgeRules.xaml
    /// </summary>
    public partial class NNgeRules : Window
    {
        public NNgeRules()
        {
            InitializeComponent();
            Print();
        }

        public void Print()
        {
            string text = File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, @"files\", "NNge -unchanged.txt"));
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
