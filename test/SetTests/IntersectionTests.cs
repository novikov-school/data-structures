using NUnit.Framework;
using System.Linq;
using System;
using Slant.Collections.Generic;

namespace SetTests
{
    [TestFixture]
    public class IntersectionTests
    {
        [Test, TestCaseSource(typeof(IntersectionTestData), nameof(IntersectionTestData.IntCases))]
        public void IntTests(TestCaseData<int> data)
        {
            intersectionTest<int>(new Set<int>(data.Left), new Set<int>(data.Right), data.Expected);
        }

        [Test, TestCaseSource(typeof(IntersectionTestData), nameof(IntersectionTestData.StringCases))]
        public void StringTests(TestCaseData<string> data)
        {
            intersectionTest<string>(new Set<string>(data.Left), new Set<string>(data.Right), data.Expected);
        }

        public void intersectionTest<T>(Set<T> left, Set<T> right, T[] expected)
            where T : IComparable<T>
        {
            Set<T> actual = left.Intersection(right);

            T[] actualAsSortedArray = actual.OrderBy(i => i).ToArray();

            CollectionAssert.AreEqual(expected, actualAsSortedArray, "The Intersection set does not match the expected set");
        }
    }

    public class IntersectionTestData
    {
        public static System.Collections.Generic.IEnumerable<TestCaseData<int>> IntCases
        {
            get
            {
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3, 4 },
                    Right = new int[] { 5, 6, 7, 8 },
                    Expected = new int[] {  }
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3, 4 },
                    Right = new int[] { 1, 2, 5, 6 },
                    Expected = new int[] { 1, 2 }
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3, 4 },
                    Right = new int[] { },
                    Expected = new int[] { }
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
                    Expected = new string[] { }
                };
                yield return new TestCaseData<string>
                {
                    Left = new string[] { "James", "Robert", "John", "Mark" },
                    Right = new string[] { "John", "Steven", "James", "Reba" },
                    Expected = new string[] { "James", "John" }
                };
            }
        }
    }
}
