using System;
using Queue.Common;
using System.Threading;
using System.Collections.Generic;

namespace Queue.Client.Locking
{
    public class Client
    {
        static readonly object syncLock = new object();

        public static int Main()
        {
            IQueue<Job> queue = new Queue.Single.Queue<Job>();

            List<Thread> allThreads = new List<Thread>();
            for (int i = 0; i < 2; i++)
            {
                allThreads.Add(new Thread(AddItems));
            }

            for (int i = 0; i < 2; i++)
            {
                allThreads.Add(new Thread(ProcessItems));
            }

            foreach (Thread t in allThreads)
            {
                t.Start(queue);
            }

            foreach (Thread t in allThreads)
            {
                while (t.ThreadState != ThreadState.Stopped)
                {
                    Thread.Sleep(100);
                }
            }

            return 0;
        }

        private static void AddItems(object queueParam)
        {
            IQueue<Job> queue = queueParam as IQueue<Job>;

            for (int actionId = 0; actionId < 100000; actionId++)
            {
                lock (syncLock)
                {
                    queue.Enqueue(new Job());
                }
            }
        }

        private static void ProcessItems(object queueParam)
        {
            int count = 0;
            IQueue<Job> queue = queueParam as IQueue<Job>;

            while (queue.Count > 0)
            {
                Job j;
                lock (syncLock)
                {
                    j = queue.Dequeue();
                }

                j.Perform();
                count++;
                if (count % 100 == 0)
                {
                    Console.WriteLine("{0} - Processed {1} jobs", Thread.CurrentThread.ManagedThreadId, count);
                }
            }
        }
    }
}
