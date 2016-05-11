using NUnit.Framework;
using System.Linq;
using System;
using Slant.Collections.Generic;

namespace SetTests
{
    [TestFixture]
    public class UnionTests
    {
        [Test, TestCaseSource(typeof(UnionTestData), nameof(UnionTestData.IntCases))]
        public void IntTests(TestCaseData<int> data)
        {
            UnionTest(Set.Create(data.Left), Set.Create(data.Right), data.Expected);
        }

        [Test, TestCaseSource(typeof(UnionTestData), nameof(UnionTestData.StringCases))]
        public void StringTests(TestCaseData<string> data)
        {
            UnionTest(Set.Create(data.Left), Set.Create(data.Right), data.Expected);
        }

        public void UnionTest<T>(Set<T> left, Set<T> right, T[] expected)
            where T: IComparable<T>
        {
            Set<T> actual = left.Union(right);

            T[] actualAsSortedArray = actual.OrderBy(i => i).ToArray();

            CollectionAssert.AreEqual(expected, actualAsSortedArray, "The union set does not match the expected set");
        }
    }

    public class UnionTestData
    {
        public static System.Collections.Generic.IEnumerable<TestCaseData<int>> IntCases
        {
            get
            {
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3, 4 },
                    Right = new int[] { 5, 6, 7, 8 },
                    Expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3, 4 },
                    Right = new int[] { 1, 2, 3, 5 },
                    Expected = new int[] { 1, 2, 3, 4, 5 },
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { 1, 2, 3, 4 },
                    Right = new int[] { },
                    Expected = new int[] { 1, 2, 3, 4 }
                };
                yield return new TestCaseData<int>
                {
                    Left = new int[] { },
                    Right = new int[] { 1, 2, 3, 4 },
                    Expected = new int[] { 1, 2, 3, 4 }
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
                    Expected = new string[] { "Amy", "Elizabeth", "James", "John", "Mark", "Robert" }
                };
            }
        }
    }
}
