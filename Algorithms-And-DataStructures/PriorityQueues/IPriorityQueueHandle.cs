using System;
using System.Collections.Generic;


namespace PriorityQueues
{
    public interface IPriorityQueueHandle <TElement, TPriority>
    {
        /// <summary>
        /// get the element
        /// </summary>
        TElement Element { get; }

        /// <summary>
        /// gets or sets the priority
        /// changing the priority should cause the associated priority queue to reprioritize
        /// </summary>
        TPriority Priority { get; set; }
    }
}
