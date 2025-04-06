using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace OrderedSet
{
    public class RBTEnumerator<TKey, TItem> : IEnumerator<TItem> where TItem : TKey
    {
        RBTNode<TItem> current; // current node
        RBTNode<TItem> Root; // root node
        LinkedList<RBTNode<TItem>> stack; // stack for traversal

        /// <summary>
        /// Constructor with one parameter
        /// Calls the Reset and Add methods
        /// </summary>
        /// <param name="root">root</param>
        public RBTEnumerator(RBTNode<TItem> root)
        {
            Root = root;
            Reset();
            Add(Root);
        }

        /// <summary>
        /// Current property that returns current item
        /// </summary>
        public TItem Current => current.Key;

        /// <summary>
        /// Current property that returns current item (non genric)
        /// </summary>
        object IEnumerator.Current => current.Key;

        /// <summary>
        /// Dispose method not needed here
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// Method that iterates next in the collection 
        /// </summary>
        /// <returns>trur if stack not empty, otherwise false</returns>
        public bool MoveNext()
        {
            if (stack.Count == 0) return false;
            current = stack.First.Value;
            stack.RemoveFirst();
            Add(current.Right);
            return true;
        }

        /// <summary>
        /// Method that adds the left subtree in stack 
        /// </summary>
        /// <param name="node"></param>
        public void Add(RBTNode<TItem> node)
        {
            while (node != null)
            {
                if (node.Left != null)
                    stack.AddFirst(node);
                node = node.Left;
            }
        }
        /// <summary>
        /// Reset the stack
        /// </summary>
        public void Reset()
        {
            stack = new LinkedList<RBTNode<TItem>>();
        }
    }
}
