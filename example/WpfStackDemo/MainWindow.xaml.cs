using System.Windows;
using Stack.List;
using System.Windows.Media;
using System;

namespace WpfStackDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stack<UndoAction> undoOps = new Stack<UndoAction>();
        Random _rng = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private Brush GetRandomBrush()
        {
            byte[] rgb = new byte[3];
            _rng.NextBytes(rgb);

            return new SolidColorBrush(Color.FromRgb(rgb[0], rgb[1], rgb[2]));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            undoOps.Push(new UndoAction(button1));
            button1.Background = GetRandomBrush();
            UpdateList();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            undoOps.Push(new UndoAction(button2));
            button2.Background = GetRandomBrush();
            UpdateList();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            undoOps.Push(new UndoAction(button3));
            button3.Background = GetRandomBrush();
            UpdateList();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (undoOps.Count > 0)
            {
                undoOps.Pop().Execute();
                UpdateList();
            }
        }

        private void UpdateList()
        {
            listBox1.Items.Clear();

            foreach (UndoAction action in undoOps)
            {
                listBox1.Items.Add(action.ToString());
            }
        }
    }
}
