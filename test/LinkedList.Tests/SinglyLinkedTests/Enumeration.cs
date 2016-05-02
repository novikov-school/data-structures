using NUnit.Framework;

namespace LinkedList.Tests
{
    [TestFixture]
    public class Enumeration
    {
        [Test]
        public void Enumerate_Empty()
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach(int value in list)
            {
                Assert.Fail("There should be nothing to enumerate");
            }
        }

        [Test, TestCaseSource("Enumeration_Success_Cases")]
        public void Enumerate_Various(int[] testCase)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int value in testCase)
            {
                list.AddLast(new LinkedListNode<int>(value));
            }

            // repeat enumeration multiple times
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(testCase.Length, list.Count,
                                "The list length was not the expected value.");

                int expectedIndex = 0;
                foreach (int value in list)
                {
                    Assert.AreEqual(testCase[expectedIndex], value,
                                    "The enumerated node value was not the expected value.");

                    expectedIndex++;
                }
            }
        }

        [Test, TestCaseSource("Enumeration_Success_Cases")]
        public void Enumerate_Raw_Various(int[] testCase)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int value in testCase)
            {
                list.AddLast(new LinkedListNode<int>(value));
            }

            // repeat enumeration multiple times
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(testCase.Length, list.Count,
                                "The list length was not the expected value.");

                int expectedIndex = 0;

                System.Collections.Generic.IEnumerable<int> rawEnum = list;

                foreach (int value in rawEnum)
                {
                    Assert.AreEqual(testCase[expectedIndex], value,
                                    "The enumerated value was not the expected value.");

                    expectedIndex++;
                }
            }
        }

        static object[] Enumeration_Success_Cases =
                     {
                            new int[] { 0 }, 
                            new int[] { 0, 1 }, 
                            new int[] { 0, 1, 2 }, 
                            new int[] { 0, 1, 2, 3 }, 
                     };

    }
}
