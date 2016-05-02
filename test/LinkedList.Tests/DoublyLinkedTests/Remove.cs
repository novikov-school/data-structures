using NUnit.Framework;

namespace DoublyLinkedList.Tests
{
    [TestFixture]
    public class Remove
    {
        [Test]
        public void RemoveFirstLast_Empty_Lists()
        {
            LinkedList<int> list = new LinkedList<int>();
            Assert.AreEqual(0, list.Count);
            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);

            list.RemoveFirst();
            Assert.AreEqual(0, list.Count);
            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);

            list.RemoveLast();
            Assert.AreEqual(0, list.Count);
            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
        }

        [Test]
        public void RemoveFirstLast_One_Node()
        {
            LinkedList<int> list = new LinkedList<int>();

            list.AddFirst(10);
            list.RemoveFirst();
            Assert.AreEqual(0, list.Count);
            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);

            list.AddFirst(10);
            list.RemoveLast();
            Assert.AreEqual(0, list.Count);
            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);            
        }

        [Test]
        public void RemoveFirstLast_Two_Node()
        {
            LinkedList<int> list = new LinkedList<int>();

            list.AddFirst(10);
            list.AddFirst(20);

            list.RemoveFirst();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(10, list.Tail.Value);

            list.AddFirst(10);
            list.RemoveLast();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(10, list.Tail.Value);
        }


        [Test]
        public void RemoveFirst_Ten_Nodes()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                list.AddFirst(i);
            }

            for (int i = 10; i > 0; i--) 
            {
                Assert.AreEqual(i, list.Count, "Unexpected list count");
                list.RemoveFirst();
            }

            Assert.AreEqual(0, list.Count);
            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
        }

        [Test]
        public void RemoveLast_Ten_Nodes()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                list.AddFirst(i);
            }

            for (int i = 10; i > 0; i--)
            {
                Assert.AreEqual(i, list.Count, "Unexpected list count");
                list.RemoveLast();
            }

            Assert.AreEqual(0, list.Count);
            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
        }

        [Test, TestCaseSource("Remove_Missing_Cases")]
        public void Remove_Missing(int[] testData, int value)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int data in testData)
            {
                list.AddLast(new LinkedListNode<int>(data));
            }

            Assert.IsFalse(list.Remove(value), "Nothing should have been removed");
            Assert.AreEqual(testData.Length, list.Count, "The expected list count was incorrect");
        }

        [Test, TestCaseSource("Remove_Found_Cases")]
        public void Remove_Found(int[] testData, int value)
        {
            LinkedList<int> list = new LinkedList<int>();
            foreach (int data in testData)
            {
                list.AddLast(new LinkedListNode<int>(data));
            }

            Assert.IsTrue(list.Remove(value), "A node should have been removed");
            Assert.AreEqual(testData.Length - 1, list.Count, "The expected list count was incorrect");
        }

        static object[] Remove_Missing_Cases =
                     {
                       new object[] { new int[] { 0 }, 10 },
                       new object[] { new int[] { 0, 1 }, 10 },
                       new object[] { new int[] { 0, 1, 2 }, 10 },
                       new object[] { new int[] { 0, 1, 2, 3 }, 10 }
                     };

        static object[] Remove_Found_Cases =
                     {
                       new object[] { new int[] { 10 }, 10 },
                       new object[] { new int[] { 10, 0 }, 10 },
                       new object[] { new int[] { 0, 10 }, 10 },
                       new object[] { new int[] { 0, 0, 10 }, 10 },
                       new object[] { new int[] { 0, 10, 0 }, 10 },
                       new object[] { new int[] { 10, 0, 0}, 10 },
                     };

    }
}
