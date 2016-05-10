using System;
using NUnit.Framework;
using Sorting;

namespace SortingTests
{
    [TestFixture]
    public class BubbleSortTests
    {
        [Test]
        public void PreSortedDoesNotSwap()
        {
            BubbleSort<int> sort = new BubbleSort<int>();

            int[] presorted = { 0,1,2,3,4,5,6,7 };
            sort.Sort(presorted);

            Assert.AreEqual(7, sort.Comparisons, "Unexpected number of comparisons");
            Assert.AreEqual(0, sort.Swaps, "Unexpected number of swaps");
        }

        [Test]
        public void EmptyDoesNotCompareOrSwap()
        {
            BubbleSort<int> sort = new BubbleSort<int>();

            int[] empty = { };
            sort.Sort(empty);

            Assert.AreEqual(0, sort.Comparisons, "Unexpected number of comparisons");
            Assert.AreEqual(0, sort.Swaps, "Unexpected number of swaps");
        }

        [Test]
        public void SingleDoesNotCompareOrSwap()
        {
            BubbleSort<int> sort = new BubbleSort<int>();

            int[] single = { 1 };
            sort.Sort(single);

            Assert.AreEqual(0, sort.Comparisons, "Unexpected number of comparisons");
            Assert.AreEqual(0, sort.Swaps, "Unexpected number of swaps");
        }

        [Test]
        public void SingleOutOfOrder()
        {
            BubbleSort<int> sort = new BubbleSort<int>();

            int[] items = { 1, 0, 2, 3, 4 };
            sort.Sort(items);

            Assert.AreEqual(8, sort.Comparisons, "Unexpected number of comparisons");
            Assert.AreEqual(1, sort.Swaps, "Unexpected number of swaps");
        }

        [Test]
        public void MultipleOutOfOrder()
        {
            BubbleSort<int> sort = new BubbleSort<int>();

            int[] items = { 4, 3, 1, 2 };
            sort.Sort(items);

            Assert.AreEqual(9, sort.Comparisons, "Unexpected number of comparisons");
            Assert.AreEqual(5, sort.Swaps, "Unexpected number of swaps");
        }

        [Test]
        public void LargeGeneratedArray()
        {
            int[] items = new int[1000];
            Random rng = new Random();

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = rng.Next();
            }

            BubbleSort<int> sort = new BubbleSort<int>();
            sort.Sort(items);

            Assert.IsTrue(sort.Comparisons >= items.Length, "At least items.Length comparisons should have been done");

            sort.Reset();

            sort.Sort(items);

            Assert.AreEqual(items.Length - 1, sort.Comparisons, "A pre-sorted array should have an exact number of comparisons performed");
            Assert.AreEqual(0, sort.Swaps, "A pre-sorted array should have no swaps performed");
        }
    }
}
