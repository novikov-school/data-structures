using System;
using Tracker;

namespace Sorting
{
    public class InsertionSort<T> : Tracker<T>, ISorter<T>
        where T : IComparable<T>
    {
        public void Sort(T[] items)
        {
            int sortedRangeEndIndex = 1;

            while (sortedRangeEndIndex < items.Length)
            {
                if (Compare(items[sortedRangeEndIndex], items[sortedRangeEndIndex - 1]) < 0)
                {
                    int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex]);
                    Insert(items, insertIndex, sortedRangeEndIndex);
                }

                sortedRangeEndIndex++;
            }
        }

        private int FindInsertionIndex(T[] items, T valueToInsert)
        {
            for (int index = 0; index < items.Length; index++)
            {
                if (Compare(items[index], valueToInsert) > 0)
                {
                    return index;
                }
            }

            throw new InvalidOperationException("The insertion index was not found");
        }

        private void Insert(T[] itemArray, int indexInsertingAt, int indexInsertingFrom)
        {
            // itemArray =       0 1 2 4 5 6 3 7
            // insertingAt =     3
            // insertingFrom =   6
            // actions
            //  1: store index at in temp     temp = 4
            //  2: set index at to index from  -> 0 1 2 3 5 6 3 7   temp = 4
            //  3: walking backwards from index from to index at + 1
            //     shift values from left to right once
            //     0 1 2 3 5 6 6 7   temp = 4
            //     0 1 2 3 5 5 6 7   temp = 4
            //  4: write temp value to index at + 1
            //     0 1 2 3 4 5 6 7   temp = 4

            // Step 1
            T temp = itemArray[indexInsertingAt];

            // Step 2

            Assign(itemArray, indexInsertingAt, itemArray[indexInsertingFrom]);

            // Step 3
            for (int current = indexInsertingFrom; current > indexInsertingAt; current--)
            {
                Assign(itemArray, current, itemArray[current - 1]);
            }

            // Step 4
            Assign(itemArray, indexInsertingAt + 1, temp);
        }
    }
}
