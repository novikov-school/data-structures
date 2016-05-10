using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Sorting;

namespace SortingVisualizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            comboBox1.Items.Add("Random");
            comboBox1.Items.Add("Sorted");
            comboBox1.Items.Add("Reversed");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                chart1.Series.Clear();

                Cursor = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    uint counts = 0;
                    if (uint.TryParse(textBox1.Text, out counts))
                    {
                        GraphPoints(counts);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("The value could not be parsed as an integer: {0}", textBox1.Text));
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        int[] GetRandomPoints(uint count)
        {
            Random r = new Random();
            int[] points = new int[count];

            for (uint i = 0; i < count; i++)
            {
                points[i] = r.Next();
            }

            return points;
        }

        private void GraphPoints(uint count)
        {
            chart1.ResetAutoValues();

            int[] points;
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Random":
                    points = GetRandomPoints(count);
                    break;
                case "Sorted":
                    points = GetSortedPoints(count);
                    break;
                case "Reversed":
                    points = GetReversedPoints(count);
                    break;
                default:
                    points = new int[0];
                    break;
            }

            ISorter<int>[] algorithms =
                {
                    new BubbleSort<int>(),
                    new InsertionSort<int>(),
                    new MergeSort<int>(),
                    new SelectionSort<int>(),
                    new QuickSort<int>(),
                };

            foreach (ISorter<int> algorithm in algorithms)
            {
                this.Text = $"Running algorithm: {algorithm.GetType().Name}";

                int[] cloned = new int[points.Length];
                Array.Copy(points, cloned, points.Length);

                algorithm.Sort(cloned);

                Series series = chart1.Series.Add(algorithm.GetType().Name);

                if (cbOperation.SelectedItem.ToString() == "Comparisons")
                {
                    series.Points.Add(new double[] { algorithm.Comparisons });
                }
                else
                {
                    series.Points.Add(new double[] { algorithm.Swaps });
                }
            }

            this.Text = string.Format("Ready");
        }

        private int[] GetSortedPoints(uint count)
        {
            int[] points = new int[count];

            for (int i = 0; i < count; i++)
            {
                points[i] = i;
            }

            return points;
        }

        private int[] GetReversedPoints(uint count)
        {
            int[] points = new int[count];

            int current = 0;
            for (int i = (int)count - 1; i >= 0; i--)
            {
                points[current++] = i;
            }

            return points;
        }
    }
}
