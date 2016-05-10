using System;
using Tracker;

namespace Sorting
{
    public class QuickSort<T> : Tracker<T>, ISorter<T>
        where T : IComparable<T>
    {
        public void Sort(T[] items)
        {
            QSort(items, 0, items.Length - 1);
        }

        private void QSort(T[] items, int left, int right)
        {
            if (left >= right)
                return;

            int pivotIndex = _pivotRng.Next(left, right);
            int newPivot = Partition(items, left, right, pivotIndex);

            QSort(items, left, newPivot - 1);
            QSort(items, newPivot + 1, right);
        }

        private int Partition(T[] items, int left, int right, int pivotIndex)
        {
            T pivotValue = items[pivotIndex];

            Swap(items, pivotIndex, right);

            int storeIndex = left;

            for (int i = left; i < right; i++)
            {
                if (Compare(items[i], pivotValue) >= 0)
                    continue;
                Swap(items, i, storeIndex);
                storeIndex += 1;
            }

            Swap(items, storeIndex, right);
            return storeIndex;
        }

        readonly Random _pivotRng = new Random();
    }
}
