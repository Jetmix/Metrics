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
using Microsoft.Win32;
using Metrics.Core;

namespace Metrics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetReport(Action<String> getReportFunction)
        {
            if (!String.IsNullOrWhiteSpace(FilePath.Text))
            {
                ContentTextBlock.Text = String.Empty;
                var result = new MethodParser().Parse(new FileParser(FilePath.Text).GetContent());
                foreach (var item in result.Methods)
                {
                    getReportFunction(item);
                    ContentTextBlock.Text += "\r\n";
                }
            }
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Java code file(.java)|*.java";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath.Text = openFileDialog.FileName;
            }
        }

        private void ChepinCalculate_Click(object sender, RoutedEventArgs e)
        {
            GetReport(GenerateChepinReport);
        }

        private void GenerateChepinReport(String source)
        {
            ContentTextBlock.Text += "Chepin metrics result:\n\tUsing variables:\n";
            var metrics = new ChepinMetrics();
            double result = metrics.Calculate(source);
            ContentTextBlock.Text += metrics.GetAllVariables();
            ContentTextBlock.Text += "\nChepin index = " + result;
        }

        private void Jilb_Click(Object sender, RoutedEventArgs e)
        {
            GetReport(GenerateJilbReport);
        }

        private void GenerateJilbReport(String source)
        {
            ContentTextBlock.Text += "Jilb metrics result:\n";
            var metrics = new JilbMetrics();
            metrics.Initialize(source);
            ContentTextBlock.Text += "Conditional Operators : " + metrics.ConditionalOperators + "\n";
            ContentTextBlock.Text += "Total Operators : " + metrics.TotalOperators + "\n";
            ContentTextBlock.Text += "Relative complexity : " + (double)metrics.ConditionalOperators / metrics.TotalOperators + "\n";
        }

        private void Meyers_Click(Object sender, RoutedEventArgs e)
        {
            GetReport(GenerateMeyersReport);
        }

        private void GenerateMeyersReport(String source)
        {
            ContentTextBlock.Text += "Meyers metrics result:\n";
            var metrics = new MeyersMetrics();
            metrics.Initialize(source);
            ContentTextBlock.Text += String.Format("Cyclomatic complexity by Meyers : [{0}, {1}]", metrics.CyclomaticComplexity, metrics.CyclomaticComplexity + metrics.PredicateDepth);
        }
    }
}
