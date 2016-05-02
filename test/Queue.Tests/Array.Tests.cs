using NUnit.Framework;
using Queue.Array;

namespace Queue.Tests
{
    [TestFixture]
    public class QueueArrayTests
    {
        [Test]
        public void Enqueue_Updates_Count()
        {
            Queue<int> queue = new Queue<int>();

            Assert.AreEqual(0, queue.Count, "The count should start at 0");

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, queue.Count, "The count was off before calling Enqueue...");
                queue.Enqueue(i);
                Assert.AreEqual(i+1, queue.Count, "The count was off after calling Enqueue...");
            }
        }

        [Test]
        public void Dequeue_Peek_Correct_Order()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }

            int expectedCount = queue.Count;

            for (int expected = 0; expected < 10; expected++)
            {
                Assert.AreEqual(expectedCount, queue.Count, "The count was off before Peek");

                Assert.AreEqual(expected, queue.Peek(), "Peek returned an unexpected result");

                Assert.AreEqual(expectedCount, queue.Count, "The count was off after Peek");
                
                Assert.AreEqual(expected, queue.Dequeue(), "Dequeue returned an unexpected result");
                
                Assert.AreEqual(expectedCount-1, queue.Count, "The count was off after Dequeue");

                expectedCount--;
            }
        }

        [Test]
        public void Enqueue_Wrapping()
        {
            Queue<int> queue = new Queue<int>();
            // fills the array (growth is 0 -> 4 -> 8)
            for (int i = 0; i < 8; i++)
            {
                queue.Enqueue(i);
            }

            int expected = 0;
            foreach (int actual in queue)
            {
                Assert.AreEqual(expected++, actual, "The enumerated value was not what was expected");
            }

            // now remove three items 
            Assert.AreEqual(0, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(1, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(2, queue.Dequeue(), "Unexpected dequeue value");

            // now 3..7 are left

            // put three more items back in to cause wrapping without growth
            for (int i = 0; i < 4; i++)
            {
                queue.Enqueue(i);
            }

            Assert.AreEqual(3, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(4, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(5, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(6, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(7, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(0, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(1, queue.Dequeue(), "Unexpected dequeue value");
            Assert.AreEqual(2, queue.Dequeue(), "Unexpected dequeue value");
        }

        [Test]
        public void Enumeration_Simple()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }

            int expected = 0;

            foreach (int i in queue)
            {
                Assert.AreEqual(expected, i, "The enumerated value was not expected");
                expected++;
            }
        }
    }
}
