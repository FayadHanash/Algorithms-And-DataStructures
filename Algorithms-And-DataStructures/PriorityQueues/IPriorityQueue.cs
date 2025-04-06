using System;
using System.Collections.Generic;

namespace PriorityQueues
{
    public interface IPriorityQueue <TElement, TPriority>
    {
        /// <summary>
        /// returns the number of elements in the queue
        /// </summary>
        int Count { get; }
        /// <summary>
        /// inserts element with the given priority
        /// returns a priority queue element that can be used to
        /// </summary>
        /// <param name="element">   </param>
        /// <param name="priority"></param>
        /// <returns></returns>
        IPriorityQueueHandle<TElement, TPriority> Enqueue(TElement element, TPriority priority);

        /// <summary>
        /// removes and returns the smallest element in the queue
        /// throws an InvalidOperationException if the queue is empty
        /// </summary>
        /// <returns></returns>
        IPriorityQueueHandle<TElement, TPriority> Dequeue();

        /// <summary>
        /// removes the smalles element in the queue passing out the element and the priority using the out parameters
        /// returns true if the queue is not empty and false otherwise
        /// in case the queue is empty and priority are given default values
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        bool TryDequeue(out TElement element, out TPriority priority);

        /// <summary>
        /// returns the smalles element in the queue without removing it passing out the element and the priority using the out parameters
        /// returns true if the queue is not empty and false otherwise
        /// in case the queue is empty element and priority are given default values
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        bool TryPeek(out TElement element, out TPriority priority);

    }
}
