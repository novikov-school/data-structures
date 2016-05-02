using System.Linq;
using NUnit.Framework;

namespace LinkedList.Tests
{
    [TestFixture]
    public class Add
    {
        [Test, TestCaseSource("Add_Success_Cases")]
        public void Add_Raw_Value_Success_Cases(int[] testCase)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int value in testCase)
            {
                list.Add(value);
            }

            Assert.AreEqual(testCase.Length, list.Count, 
                "There was an unexpected number of list items");

            Assert.AreEqual(testCase.Last(), list.Head.Value, 
                "The first item value was incorrect");
            
            Assert.AreEqual(testCase.First(), list.Tail.Value, 
                "The last item value was incorrect");

            int[] reversed = testCase.Reverse().ToArray();
            int current = 0;

            foreach (int value in list)
            {
                Assert.AreEqual(reversed[current], value, "The list value at index {0} was incorrect.", current);
                current++;
            }
        }

        [Test, TestCaseSource("Add_Success_Cases")]
        public void AddFirst_Node_Value_Success_Cases(int[] testCase)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int value in testCase)
            {
                list.AddFirst(new LinkedListNode<int>(value));
            }

            Assert.AreEqual(testCase.Length, list.Count,
                "There was an unexpected number of list items");

            Assert.AreEqual(testCase.Last(), list.Head.Value,
                "The first item value was incorrect");

            Assert.AreEqual(testCase.First(), list.Tail.Value,
                "The last item value was incorrect");

            int[] reversed = testCase.Reverse().ToArray();
            int current = 0;

            foreach (int value in list)
            {
                Assert.AreEqual(reversed[current], value, "The list value at index {0} was incorrect.", current);
                current++;
            }
        }

        [Test, TestCaseSource("Add_Success_Cases")]
        public void AddLast_Raw_Value_Success_Cases(int[] testCase)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int value in testCase)
            {
                list.AddLast(value);
            }

            Assert.AreEqual(testCase.Length, list.Count,
                "There was an unexpected number of list items");

            Assert.AreEqual(testCase.First(), list.Head.Value,
                "The first item value was incorrect");

            Assert.AreEqual(testCase.Last(), list.Tail.Value,
                "The last item value was incorrect");

            int current = 0;
            foreach (int value in list)
            {
                Assert.AreEqual(testCase[current], value, "The list value at index {0} was incorrect.", current);
                current++;
            }
        }

        [Test, TestCaseSource("Add_Success_Cases")]
        public void AddLast_Node_Value_Success_Cases(int[] testCase)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int value in testCase)
            {
                list.AddLast(new LinkedListNode<int>(value));
            }

            Assert.AreEqual(testCase.Length, list.Count,
                "There was an unexpected number of list items");

            Assert.AreEqual(testCase.First(), list.Head.Value,
                "The first item value was incorrect");

            Assert.AreEqual(testCase.Last(), list.Tail.Value,
                "The last item value was incorrect");

            int current = 0;
            foreach (int value in list)
            {
                Assert.AreEqual(testCase[current], value, "The list value at index {0} was incorrect.", current);
                current++;
            }
        }


        static object[] Add_Success_Cases =
                     {
                            new int[] { 0 }, 
                            new int[] { 0, 1 }, 
                            new int[] { 0, 1, 2 }, 
                            new int[] { 0, 1, 2, 3 }, 
                     };
    }
}
