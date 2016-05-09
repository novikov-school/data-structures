using System;
using System.Linq;
using System.Text;
using System.Windows;
using StringSearching;
using StringSearching.BoyerMoore;
using Tracker;

namespace SimpleTextReplacement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            txtNaiveOutput.Clear();
            txtBoyerMooreOutput.Clear();


            string input = txtContent.Text;
            string find = txtFind.Text;
            string replace = txtReplace.Text;

            IStringSearchAlgorithm boyerMoore = new BoyerMoore();
            StringBuilder boyerMooreResult = PerformSearchAndReplace(boyerMoore, input, find, replace);
            txtBoyerMooreOutput.Text = boyerMooreResult.ToString();
            lblBoyerMooreComparisonsOutput.Content = ((IPerformanceTracker)boyerMoore).Comparisons;

            IStringSearchAlgorithm Naive = new NaiveStringSearch();
            StringBuilder NaiveResult = PerformSearchAndReplace(Naive, input, find, replace);
            txtNaiveOutput.Text = NaiveResult.ToString();
            lblNaiveComparisonsValue.Content = ((IPerformanceTracker)Naive).Comparisons;
        }

        private static StringBuilder PerformSearchAndReplace(IStringSearchAlgorithm algorithm, string input, string find, string replace)
        {
            StringBuilder result = new StringBuilder();
            int previousStart = 0;
            foreach (var match in algorithm.Search(find, input))
            {
                result.Append(input.Substring(previousStart, match.Start - previousStart));
                result.Append(replace);
                previousStart = match.Start + match.Length;
            }

            result.Append(input.Substring(previousStart));
            return result;
        }
    }
}
