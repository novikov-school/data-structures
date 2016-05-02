using System;
using NUnit.Framework;

namespace DoublyLinkedList.Tests
{
    [TestFixture]
    public class CopyTo
    {
        [Test]
        public void CopyTo_Empty_List()
        {
            LinkedList<int> list = new LinkedList<int>();
            int[] array = new int[1];
            list.CopyTo(array, 0);
        }

        [Test, TestCaseSource("CopyTo_Success_Cases")]
        public void CopyTo_Zero_Index(int[] testCase)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int data in testCase)
            {
                list.AddLast(data);
            }

            int[] newArray = new int[testCase.Length];
            list.CopyTo(newArray, 0);

            Assert.AreEqual(testCase, newArray, "The resulting array was not correct");
        }

        [Test, TestCaseSource("CopyTo_Success_Cases")]
        public void CopyTo_Nth_Index(int[] testCase)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int data in testCase)
            {
                list.AddLast(data);
            }

            int preOffset = (DateTime.Now.Millisecond % 20) + 1;
            int postOffset = preOffset;

            int[] newArray = new int[preOffset + testCase.Length + postOffset];
            list.CopyTo(newArray, preOffset);

            for (int i = preOffset, x = 0; i < (preOffset + testCase.Length); i++, x++)
            {
                Assert.AreEqual(testCase[x], newArray[i], "The expected value was not correct");
            }
        }


        static object[] CopyTo_Success_Cases =
                        {
                            new int[] { 0 }, 
                            new int[] { 0, 1 }, 
                            new int[] { 0, 1, 2 }, 
                            new int[] { 0, 1, 2, 3 }, 
                        };
    }
}
