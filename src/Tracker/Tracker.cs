using System;
using System.Threading;

namespace Tracker
{
    public interface IPerformanceTracker
    {
        long Comparisons
        {
            get;
        }

        long Swaps
        {
            get;
        }

        void Reset();
    }

    public class Tracker<T> : IPerformanceTracker
        where T: IComparable<T>
    {
        public long Comparisons
        {
            get
            {
                return Interlocked.Read(ref _comparisons);
            }
        }

        public long Swaps
        {
            get
            {
                return Interlocked.Read(ref _swaps);
            }
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _comparisons, 0);
            Interlocked.Exchange(ref _swaps, 0);
        }

        protected void Swap(T[] items, int left, int right)
        {
            if (left != right)
            {
                Interlocked.Increment(ref _swaps);

                T temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
        }

        protected void Assign(T[] items, int index, T value)
        {
            items[index] = value;
            Interlocked.Increment(ref _swaps);
        }

        protected int Compare(T lhs, T rhs)
        {
            Interlocked.Increment(ref _comparisons);

            return lhs.CompareTo(rhs);
        }

        long _comparisons;
        long _swaps;
    }
}
