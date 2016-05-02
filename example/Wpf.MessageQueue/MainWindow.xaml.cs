using System;
using System.Windows;
using Queue.Array;

namespace Wpf.VisualQueue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Queue<int> _queue = new Queue<int>();
        Random _rng = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            _queue.Enqueue(_rng.Next(0, 200));
            UpdateGrid();
        }

        private void button_Process_Click(object sender, RoutedEventArgs e)
        {
            if (_queue.Count > 0)
            {
                listBox1.Items.Add(_queue.Dequeue().ToString());
                UpdateGrid();
            }
        }

        private void UpdateGrid()
        {
            queue_label_1.Content = string.Empty;
            queue_label_2.Content = string.Empty;
            queue_label_3.Content = string.Empty;
            queue_label_4.Content = string.Empty;
            queue_label_5.Content = string.Empty;
            queue_label_6.Content = string.Empty;

            int index = 0;
            foreach (int message in _queue)
            {
                switch (index)
                {
                    case 0:
                        queue_label_1.Content = message.ToString();
                        break;
                    case 1:
                        queue_label_2.Content = message.ToString();
                        break;
                    case 2:
                        queue_label_3.Content = message.ToString();
                        break;
                    case 3:
                        queue_label_4.Content = message.ToString();
                        break;
                    case 4:
                        queue_label_5.Content = message.ToString();
                        break;
                    case 5:
                        queue_label_6.Content = message.ToString();
                        break;
                    default: 
                        break;
                }

                index++;

                if (index > 5)
                {
                    break;
                }
            }
        }
    }
}
