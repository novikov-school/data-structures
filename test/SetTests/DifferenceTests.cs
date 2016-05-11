using NUnit.Framework;
using System.Linq;
using System;
using Slant.Collections.Generic;

namespace SetTests
{
    [TestFixture]
    public class DifferenceTests
    {
        [Test, TestCaseSource(typeof(DifferenceTestData), nameof(DifferenceTestData.IntCases))]
        public void IntTests(TestCaseData<int> data)
        {
            DifferenceTest<int>(new Set<int>(data.Left), new Set<int>(data.Right), data.Expected);
        }

        [Test, TestCaseSource(typeof(DifferenceTestData), nameof(DifferenceTestData.StringCases))]
        public void StringTests(TestCaseData<string> data)
        {
            DifferenceTest<string>(new Set<string>(data.Left), new Set<string>(data.Right), data.Expected);
        }

        public void DifferenceTest<T>(Set<T> left, Set<T> right, T[] expected)
            where T : IComparable<T>
        {
            Set<T> actual = left.Difference(right);

            T[] actualAsSortedArray = actual.OrderBy(i => i).ToArray();

            CollectionAssert.AreEqual(expected, actualAsSortedArray, "The Difference set does not match the expected set");
        }
    }

    public class DifferenceTestData
    {
        public static System.Collections.Generic.IEnumerable<TestCaseData<int>> IntCases
        {
            get
            {
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3, 4 },
                    Right = new int[] { 5, 6, 7, 8 },
                    Expected = new int[] { 1, 2, 3, 4}
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3 },
                    Right = new int[] { 1, 7, 8 },
                    Expected = new int[] { 2, 3 }
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3, 4 },
                    Right = new int[] { 1, 2, 5, 6 },
                    Expected = new int[] { 3, 4 }
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3, 4 },
                    Right = new int[] { },
                    Expected = new int[] { 1, 2, 3, 4}
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { },
                    Right = new int[] { 1, 2, 3, 4 },
                    Expected = new int[] { }
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { },
                    Right = new int[] { },
                    Expected = new int[] { }
                };
            }
        }

        public static System.Collections.Generic.IEnumerable<TestCaseData<string>> StringCases
        {
            get
            {
                yield return new TestCaseData<string>
                {
                    Left = new string[] { "James", "Robert", "John", "Mark" },
                    Right = new string[] { "Elizabeth", "Amy" },
                    Expected = new string[] { "James", "John", "Mark", "Robert" }
                };
                yield return new TestCaseData<string>
                {
                    Left = new string[] { "James", "Robert", "John", "Mark" },
                    Right = new string[] { "John", "Steven", "James", "Reba" },
                    Expected = new string[] { "Mark", "Robert" }
                };
            }
        }
    }
}
