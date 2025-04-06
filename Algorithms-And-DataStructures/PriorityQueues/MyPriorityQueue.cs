using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace PriorityQueues
{
    public class MyPriorityQueue<TElement, TPriority> : IPriorityQueue<TElement, TPriority>
    {
        /// <summary>
        /// intializes the heap of List of Item 
        /// </summary>
        List<Item<TElement, TPriority>> heap;
        /// <summary>
        /// comparer object
        /// </summary>
        IComparer<TPriority> comparer; 
        /// <summary>
        /// Default constructor
        /// creates a instance of the heap
        /// </summary>
        public MyPriorityQueue() : this(Comparer<TPriority>.Default)
        {
            
        }

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="comparer">comparere object of type IComparere<TItem></param>
        /// <exception cref="Exception">throw a exception if comparer is null</exception>
        public MyPriorityQueue(IComparer<TPriority> comparer)
        {
            if (comparer == null) throw new Exception("comparer is null");
            heap = new List<Item<TElement, TPriority>>();
            this.comparer = comparer;
        }

        /// <summary>
        /// Inserts a element with given priority.
        /// Creates a new instance of Item for element and its priority
        /// Adds the item at last in the heap and heapifys up it 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns>Element with highest priority in the queue</returns>
        public IPriorityQueueHandle<TElement, TPriority> Enqueue(TElement element, TPriority priority)
        {
            Item<TElement, TPriority> item = new Item<TElement, TPriority>(element, priority);
            heap.Add(item);
            HeapifyUp(Count - 1);
            return heap[0];    
        }

        /// <summary>
        /// Heapifys up the heap at a given index
        /// </summary>
        /// <param name="index"></param>
        void HeapifyUp(int index)
        {
            if (index <= Count && index > 0)
            {
                int parent = (index - 1) / 2;
                if (comparer.Compare(heap[index].Priority, heap[parent].Priority) < 0)
                {
                    (heap[index], heap[parent]) = (heap[parent], heap[index]);
                    HeapifyUp(parent);
                }
            }
        }
        
        /// <summary>
        /// Heapifys down the heap at a given index
        /// </summary>
        /// <param name="index"></param>
        void HeapifyDown(int index)
        {
            int left = index * 2 + 1;
            int right = index * 2 + 2;
            int smallest = index;
            if (left < Count && comparer.Compare(heap[left].Priority, heap[index].Priority) < 0)
                smallest = left;
            if (right < Count && comparer.Compare(heap[right].Priority, heap[smallest].Priority) < 0)
                smallest = right;
            if (smallest != index)
            {
                (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
                HeapifyDown(smallest);
            }
        }

        /// <summary>
        /// Removes the smallest element in the queue
        /// Calls IsEmpty method to check if the queue is empty or not, throws an InvalidOperationException in case the queue is empty
        /// Keeps a reference of the smallest element, then Calls ExtractMin method to delete it 
        /// </summary>
        /// <returns>The smallest element which has been removed</returns>
        public IPriorityQueueHandle<TElement, TPriority> Dequeue()
        {
            IsEmpty();
            Item<TElement, TPriority> item = heap[0];
            ExtractMin();
            
            return item;
        }


#nullable disable


        /// <summary>
        /// Finds index of the element in the queue
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns>Index</returns>
        int FindIndex(TElement element, TPriority priority) => heap.FindIndex(x => x.Element.Equals(element) && x.Priority.Equals(priority));

        /// <summary>
        /// Removes a specific element in the queue.
        /// Calls IsEmpty method to check if the queue is empty or not, throws an InvalidOperationException in case the queue is empty.
        /// Calls FindIndex method to find the index of the element in the queue, throws an InvalidOperationException in case the element is not found in the queue.
        /// Keeps a reference of the element to be removed, then replaces the element with the last element in the queue,
        /// removes the last element in the queue, and heapifys down the queue from the index of the element.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns>Element which has been removed</returns>
        /// <exception cref="InvalidOperationException">If the queue is empty or the element is not found in the queue</exception>
        public IPriorityQueueHandle<TElement, TPriority> DequeueAt( TElement element, TPriority priority)
        {
            IsEmpty();
            int index = FindIndex(element, priority);
            if (index == -1) throw new InvalidOperationException("Element not found");
            Item<TElement, TPriority> item = heap[index];
            heap[index] = heap[Count - 1];
            heap.RemoveAt(Count - 1);
            HeapifyDown(index);
            return item;
        }
        
        /// <summary>
        /// Displays the queue
        /// </summary>
        public void DisplayQueue()
        {
            foreach (var item in heap)
                Console.WriteLine($"{item.Element} {item.Priority}");
        }

        /// <summary>
        /// Increase priority at the given element.
        /// Calls IsEmpty method to check if the queue is empty or not, throws an InvalidOperationException in case the queue is empty.
        /// Calls FindIndex method to find the index of the element in the queue, throws an InvalidOperationException in case the element is not found in the queue.
        /// Compares both priorities if the new one higher than old priority, then replaces the new priority, otherwise throw an InvalidOperationException
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <param name="newPriority"></param>
        /// <returns>Element with new associated priority </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IPriorityQueueHandle<TElement, TPriority> IncreasePriority(TElement element, TPriority priority, TPriority newPriority)
        {
            Item<TElement, TPriority> temp;
            IsEmpty();
            int index = FindIndex(element, priority);
            if (index == -1) throw new InvalidOperationException("Element not found");
            if (comparer.Compare(newPriority, heap[index].Priority) > 0 && index != -1)
            {
                heap[index].Priority = newPriority;
                temp = heap[index];
                HeapifyDown(index);
                return temp;
            }
            else throw new InvalidOperationException("New priority is not higher than the old priority");
            
        }

        /// <summary>
        /// Decrease priority at the given element.
        /// Calls IsEmpty method to check if the queue is empty or not, throws an InvalidOperationException in case the queue is empty.
        /// Calls FindIndex method to find the index of the element in the queue, throws an InvalidOperationException in case the element is not found in the queue.
        /// Compares both priorities if the new one lower than old priority, then replaces the new priority, otherwise throw an InvalidOperationException
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <param name="newPriority"></param>
        /// <returns>Element with new associated priority </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IPriorityQueueHandle<TElement, TPriority> DecreasePriority(TElement element, TPriority priority, TPriority newPriority)
        {
            Item<TElement, TPriority> temp;
            IsEmpty();
            int index = FindIndex(element, priority);
            if (index == -1) throw new InvalidOperationException("Element not found");
            if (comparer.Compare(newPriority, heap[index].Priority) < 0 && index != -1)
            {
                heap[index].Priority = newPriority;
                temp = heap[index];
                HeapifyUp(index);
                return temp;
            }
            else throw new InvalidOperationException("New priority is not lower than the old priority");
        }

        /// <summary>
        /// removes the smalles element in the queue passing out the element and the priority using the out parameters
        /// in case the queue is empty and priority are given default values
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns>Returns true if the queue is not empty and false otherwise</returns>
        public bool TryDequeue( out TElement element, out TPriority priority)
        {
            if (Count == 0)
            {
                element = default;
                priority = default;
                return false;
            }
            element = heap[0].Element;
            priority = heap[0].Priority;
            ExtractMin();
            return true;
        }

        /// <summary>
        /// Returns the smalles element in the queue without removing it passing out the element and the priority using the out parameters
        /// in case the queue is empty element and priority are given default values
        /// </summary>
        /// <param name="element"></param>
        /// <param name="priority"></param>
        /// <returns>Returns true if the queue is not empty and false otherwise</returns>
        public bool TryPeek(out TElement element, out TPriority priority)
        {
            if (Count == 0)
            {
                element = default(TElement);
                priority = default(TPriority);
                return false;
            }
            element = heap[0].Element;
            priority = heap[0].Priority;
            return true;
        }
#nullable enable


        /// <summary>
        /// A help method that removes the smallest element in the queue.
        /// Calls IsEmpty method to check if the queue is empty or not, throws an InvalidOperationException in case the queue is empty.
        /// Replaces the smallest element with the last element in the queue,
        /// removes the last element in the queue, and heapifys down the queue.
        /// </summary>
        void ExtractMin()
        {
            IsEmpty();
            heap[0] = heap[Count - 1];
            heap.RemoveAt(Count - 1);
            HeapifyDown(0);
        }

        /// <summary>
        /// returns number of elements in the queue.
        /// </summary>
        public int Count => heap.Count;

        /// <summary>
        /// Checks if the queue is empty or not, throws an InvalidOperationException in case the queue is empty.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        bool IsEmpty() => Count == 0 ? throw new InvalidOperationException("Queue is empty") : false;

    }
}
