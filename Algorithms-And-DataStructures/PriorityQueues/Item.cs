using System;
using System.Collections.Generic;

namespace PriorityQueues
{
    public class Item<TElement, TPriority> : IPriorityQueueHandle<TElement, TPriority>
    {
        /// <summary>
        /// privat instances
        /// </summary>
        private TElement element;
        private TPriority priority;

        /// <summary>
        /// Constructor with two parameters
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        public Item(TElement element, TPriority priority)
        {
            this.element = element;
            this.priority = priority;
        }

        /// <summary>
        /// Property related to element field.
        /// Read access
        /// </summary>
        public TElement Element => element;

        /// <summary>
        /// Property related to priority field.
        /// Both rerad and write access
        /// </summary>
        public TPriority Priority { get => priority; set => priority = value; }

        /// <summary>
        /// Prepares a format string is in sync with the ToString method (base)
        /// </summary>
        /// <returns>A formatted string</returns>
        public override string ToString() => $"{element} : ({priority})";
    }
}
