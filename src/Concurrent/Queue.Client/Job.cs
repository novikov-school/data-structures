using System;
using System.Threading;

namespace Queue.Client
{
    public class Job
    {
        object synclock = new object();
        int performed = 0;

        public Job()
        {
        }

        public void Perform()
        {
            if (Interlocked.Increment(ref performed) > 1)
            {
                throw new InvalidOperationException("Action performed multiple times");
            }
        }
    }
}
