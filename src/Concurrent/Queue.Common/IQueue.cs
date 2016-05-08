using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queue.Common
{
    public interface IQueue<T> : System.Collections.Generic.IEnumerable<T>
    {
        /// <summary>
        /// Adds an item to the back of the queue
        /// </summary>
        /// <param name="item">The item to place in the queue</param>
        void Enqueue(T item);

        /// <summary>
        /// Removes and returns the front item from the queue
        /// </summary>
        /// <returns>The front item from the queue</returns>
        T Dequeue();

        /// <summary>
        /// Returns the front item from the queue without removing it from the queue
        /// </summary>
        /// <returns>The front item from the queue</returns>
        T Peek();

        /// <summary>
        /// The number of items in the queue
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// Removes all items from the queue
        /// </summary>
        void Clear();
    }
}
