using System;

namespace Queue.Client.Locking
{
    public class Job
    {
        bool performed = false;
        object synclock = new object();

        public Job()
        {
        }

        public void Perform()
        {
            lock (synclock)
            {
                if (!performed)
                {
                    performed = true;
                }
                else
                {
                    throw new InvalidOperationException("Action performed twice");
                }
            }
        }
    }
}
