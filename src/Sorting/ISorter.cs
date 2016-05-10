using System;
using Tracker;

namespace Sorting
{
    public interface ISorter<in T> : IPerformanceTracker
        where T : IComparable<T>
    {
        void Sort(T[] items);
    }
}
