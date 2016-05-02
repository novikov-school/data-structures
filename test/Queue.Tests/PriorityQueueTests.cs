using NUnit.Framework;
using Queue.Priority;

namespace Queue.Tests
{
    [TestFixture]
    public class PriorityQueueTests
    {
        [Test]
        public void Enqueue_Simple()
        {
            PriorityQueue<int> q = new PriorityQueue<int>();
            for (int i = 0; i < 10; i++)
            {
                q.Enqueue(i);
            }

            Assert.AreEqual(q.Count, 10, "The wrong number of items are in the queue");

            int expected = 9;
            while (q.Count > 0)
            {
                Assert.AreEqual(expected, q.Dequeue(), "The expected priority value was not dequeued");
                expected--;
            }
        }

        [Test]
        public void Enqueue_Specific()
        {
            PriorityQueue<int> q = new PriorityQueue<int>();

            q.Enqueue(5);
            q.Enqueue(4);
            q.Enqueue(2);
            q.Enqueue(4);
            q.Enqueue(6);
            q.Enqueue(3);

            Assert.AreEqual(6, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(5, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(4, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(4, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(3, q.Dequeue(), "Unexpected pq value");
            Assert.AreEqual(2, q.Dequeue(), "Unexpected pq value");
        }

        [Test]
        public void Enumeration_Simple()
        {
            int[] input =    { 2, 4, 7, 4, 2, 8, 1 };
            int[] expected = { 8, 7, 4, 4, 2, 2, 1 };

            PriorityQueue<int> queue = new PriorityQueue<int>();

            foreach (int i in input)
            {
                queue.Enqueue(i);
            }

            int index = 0;

            foreach (int i in queue)
            {
                Assert.AreEqual(expected[index], i, "The enumerated value was unexpected");
                index++;
            }
        }
    }
}
