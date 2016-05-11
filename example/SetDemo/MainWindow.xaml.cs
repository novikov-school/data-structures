using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Slant.Collections.Generic;

namespace SetDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        readonly Set<Student> _men = new Set<Student>();
        readonly Set<Student> _women = new Set<Student>();

        readonly Set<Student> _reading = new Set<Student>();
        readonly Set<Student> _writing = new Set<Student>();
        readonly Set<Student> _arithmetic = new Set<Student>();

        readonly Dictionary<string, Set<Student>> allSets = new Dictionary<string, Set<Student>>();

        public MainWindow()
        {
            Student james = new Student(1, "James", Gender.Male);
            Student robert = new Student(2, "Robert", Gender.Male);
            Student john = new Student(3, "John", Gender.Male);
            Student mark = new Student(4, "Mark", Gender.Male);
            Student otherMark = new Student(5, "Mark", Gender.Male);
            _men.AddRange(new[] { james, robert, john, mark, otherMark });

            Student liz = new Student(6, "Elizabeth", Gender.Female);
            Student amy = new Student(7, "Amy", Gender.Female);
            Student eve = new Student(8, "Evelyn", Gender.Female);
            _women.AddRange(new[] { liz, amy, eve });

            _reading.AddRange(new[] { james, robert, liz });
            _writing.AddRange(new[] { robert, mark, amy, eve, liz });
            _arithmetic.AddRange(new[] { john, mark, otherMark, amy });

            allSets.Add("Men", _men);
            allSets.Add("Women", _women);
            allSets.Add("Reading", _reading);
            allSets.Add("Writing", _writing);
            allSets.Add("Arithmetic", _arithmetic);

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string name in allSets.Keys)
            {
                leftSet.Items.Add(name);
                rightSet.Items.Add(name);
            }

            operation.Items.Add("UNION");
            operation.Items.Add("INTERSECTION");
            operation.Items.Add("DIFFERENCE");
            operation.Items.Add("SYMETRIC DIFF");
        }

        private void leftSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            leftMembers.Items.Clear();

            if (e.AddedItems.Count > 0)
            {
                DisplaySetData(GetSetByName(e.AddedItems[0].ToString()), leftMembers);
            }
        }

        private void rightSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rightMembers.Items.Clear();

            if (e.AddedItems.Count > 0)
            {
                DisplaySetData(GetSetByName(e.AddedItems[0].ToString()), rightMembers);
            }
        }

        Set<Student> GetSetByName(string name)
        {
            return allSets[name];
        }

        void DisplaySetData(Set<Student> set, ItemsControl list)
        {
            list.Items.Clear();

            foreach (Student s in set.OrderBy(student => student.StudentId))
            {
                list.Items.Add($"{s.StudentId}: {s.Name}");
            }
        }

        private void evaluateButton_Click(object sender, RoutedEventArgs e)
        {
            resultSet.Items.Clear();
            if (operation.SelectedItem != null)
            {
                Set<Student> results = UpdateResultSet(GetSetByName(leftSet.SelectedItem.ToString()), GetSetByName(rightSet.SelectedItem.ToString()), operation.SelectedItem.ToString());
                DisplaySetData(results, resultSet);
            }
        }

        private Set<Student> UpdateResultSet(Set<Student> left, Set<Student> right, string op)
        {
            switch(op)
            {
                case "UNION":
                    return left.Union(right);
            	case "INTERSECTION":
                    return left.Intersection(right);
                case "DIFFERENCE":
                    return left.Difference(right);
                case "SYMETRIC DIFF":
                    return left.SymmetricDifference(right);
                default:
                    return new Set<Student>
                    {
                        new Student(-1, "ERROR", Gender.Unknown)
                    };
            }
        }
    }
}
