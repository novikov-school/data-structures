using System;
using Queue.Common;
using System.Threading;
using System.Collections.Generic;

namespace Queue.Client
{
    public class Client
    {
        static readonly WaitHandle errorOccurred = new ManualResetEvent(false);

        public static int Main()
        {
            List<IQueue<Job>> queues = new List<IQueue<Job>>()
            {
                new Queue.Single.Queue<Job>(),
                new Queue.Locking.MonitorLockingQueue<Job>(),
                new Queue.ReadWriteLock.ReaderWriterLockQueue<Job>(),
            };

            foreach (IQueue<Job> queue in queues)
            {
                System.Diagnostics.Stopwatch enumerateTimer = new System.Diagnostics.Stopwatch();
                System.Diagnostics.Stopwatch processTimer = new System.Diagnostics.Stopwatch();

                JobProcessor processor = new JobProcessor(queue);
                processor.Load();
                
                enumerateTimer.Start();
                processor.EnumerateInParallel();
                processor.Wait();
                enumerateTimer.Stop();

                processTimer.Start();
                processor.ProcessInParallel();
                processor.Wait();
                processTimer.Stop();

                if (processor.Exception != null)
                {
                    Console.WriteLine("ERROR {0}: {1}", queue.GetType().Name, processor.Exception.Message);
                }
                else
                {
                    Console.WriteLine("SUCCESS ({0}): Enumerate: {1}ms Process: {2}", queue.GetType().Name, enumerateTimer.ElapsedMilliseconds, processTimer.ElapsedMilliseconds);
                }
            }

            return 0;
        }
    }

    class JobProcessor
    {
        IQueue<Job> queue;
        List<Thread> allThreads = new List<Thread>();
        List<ManualResetEvent> allEvents = new List<ManualResetEvent>();
        object syncLock = new object();

        private volatile bool aborted = false;

        public Exception Exception
        {
            get;
            private set;
        }

        public JobProcessor(IQueue<Job> queue)
        {
            this.queue = queue;
        }

        public void Wait()
        {
            foreach (Thread t in allThreads)
            {
                t.Join();
            }
        }

        private void Abort(Exception ex)
        {
            if (Exception == null)
            {
                lock (syncLock)
                {
                    if (Exception == null)
                    {
                        Exception = ex;
                        aborted = true;
                    }
                }
            }
        }

        public void Load()
        {
            queue.Clear();
            for (int i = 0; i < 2000000; i++)
            {
                queue.Enqueue(new Job());
            }
        }

        public void ProcessInParallel()
        {
            allThreads.Clear();

            for (int i = 0; i < 2; i++)
            {
                allThreads.Add(new Thread(ProcessItems));
            }

            foreach (Thread t in allThreads)
            {
                t.Start(queue);
            }
        }

        private void ProcessItems(object queueParam)
        {
            int count = 0;
            IQueue<Job> queue = queueParam as IQueue<Job>;

            try
            {
                while (queue.Count > 0)
                {
                    Job j = queue.Dequeue();
                    j.Perform();
                    count++;
                    if (count % 1000 == 0)
                    {
                        if (aborted)
                        {
                            return;
                        }
                    }
                }
            }
            catch (QueueEmptyException)
            {
                // ignore
            }
            catch (Exception ex)
            {
                Abort(ex);
            }
            finally
            {
                Console.WriteLine("{0} - Processed {1} jobs", Thread.CurrentThread.ManagedThreadId, count);
            }
        }

        internal void EnumerateInParallel()
        {
            allThreads.Clear();

            for (int i = 0; i < 2; i++)
            {
                allThreads.Add(new Thread(EnumerateItems));
            }

            foreach (Thread t in allThreads)
            {
                t.Start(queue);
            }
        }

        private void EnumerateItems(object queueParam)
        {
            IQueue<Job> queue = queueParam as IQueue<Job>;

            int count = 0;

            foreach(Job j in queue)
            {
                count++;
            }

            if (count != queue.Count)
            {
                throw new InvalidOperationException("this will never happen");
            }
        }

    }
}
