using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace OrderedSet
{
    public class OrderedSet<TKey, TItem> : IOrderedSet<TKey, TItem> where TItem : class, TKey
    {
        RBTree<TKey, TItem> tree; // the red-black tree
        public RBTree<TKey, TItem> Tree { get => tree; set => tree = value; } // the red-black tree property getter and setter access

        /// <summary>
        /// Default constructor
        /// </summary>
        public OrderedSet()
        {
            tree = new RBTree<TKey, TItem>();
        }

        /// <summary>
        /// Property returns the number of items in the set
        /// </summary>
        public int Count => tree.Count;

        /// <summary>
        /// Method that inserts an item into the set
        /// </summary>
        /// <param name="item">the item to be inserted</param>
        public void Insert(TItem item) => tree.Insert(item);

        /// <summary>
        /// Method that returns maximum item in the set colletion
        /// </summary>
        /// <returns>maximum item</returns>
        public TItem Maximum() => tree.Maximum();
        /// <summary>
        /// Method that returns minimum item in the set collection
        /// </summary>
        /// <returns>minimum item</returns>
        public TItem Minimum() => tree.Minimum();

        /// <summary>
        /// Method that returns the predecessor of a given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>predecessor of a given item</returns>
        public TItem Predecessor(TItem item) => tree.Predecessor(item);

        /// <summary>
        /// Method that searchs for a given key and returns its item
        /// </summary>
        /// <param name="key">the key to be searched</param>
        /// <returns>an item if found</returns>
        public TItem Search(TKey key) => tree.Search(key);

        /// <summary>
        /// Method that returns the successor of a given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>successor of a given item</returns>
        public TItem Successor(TItem item) => tree.Successor(item);

        /// <summary>
        ///  adds the items in a tree(collection) to other collection 
        ///  the complexity is O(n)
        /// </summary>
        /// <param name="other">other collection to be unioned</param>
        public void UnionWith(IEnumerable<TItem> other) => tree.UnionWith(other);
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// the items are enumerator in ascending order( in-order traversal)
        /// </summary>
        /// <returns>items in the collection</returns>
        public IEnumerator<TItem> GetEnumerator() => tree.GetEnumerator();
        /// <summary>
        /// Calls GetEnumerator method to iterates through the collection.
        /// </summary>
        /// <returns>items in the collection</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Method that calculates and returns the hight of the tree at a given node
        /// </summary>
        /// <param name="r">the node to be calculated</param>
        /// <returns>Hight of a tree</returns>
        public int Height(RBTNode<TItem> n) => tree.Height(n);

        /// <summary>
        /// Method that calculates and returns the hight of the tree at a given item
        /// </summary>
        /// <param name="r">the item to be calculated</param>
        /// <returns>Hight of a tree</returns>
        public int Height(TItem n) => tree.Height(n);

        /// <summary>
        /// Method that calculates and returns the black hight of the tree at a given node
        /// </summary>
        /// <param name="r">the node to be calculated</param>
        /// <returns>Black hight of a tree</returns>
        public int BlachHeight(RBTNode<TItem> n) => tree.BlackHeight(n);

        /// <summary>
        /// Method that calculates and returns the black hight of the tree at a given item
        /// </summary>
        /// <param name="r">the item to be calculated</param>
        /// <returns>Black hight of a tree</returns>
        public int BlachHeight(TItem n) => tree.BlackHeight(n);

        /// <summary>
        /// Method that calculate shortest path between two nodes
        /// </summary>
        /// <param name="from">source</param>
        /// <param name="to">destination</param>
        /// <returns>shortest path</returns>
        public int ShortestPath(RBTNode<TItem> x, RBTNode<TItem> y) => tree.ShortestPath(x, y);

        /// <summary>
        /// Method that calculate shortest path between two items
        /// </summary>
        /// <param name="from">source</param>
        /// <param name="to">destination</param>
        /// <returns>shortest path</returns>
        public int ShortestPath(TItem x, TItem y) => tree.ShortestPath(x, y);

        /// <summary>
        /// Method that calculate longest path between two nodes
        /// </summary>
        /// <param name="from">source</param>
        /// <param name="to">destination</param>
        /// <returns>longest path</returns>
        public int LongestPath(RBTNode<TItem> x, RBTNode<TItem> y) => tree.LongestPath(x, y);

        /// <summary>
        /// Method that calculate longest path between two items
        /// </summary>
        /// <param name="from">source</param>
        /// <param name="to">destination</param>
        /// <returns>longest path</returns>
        public int LongestPath(TItem x, TItem y) => tree.LongestPath(x, y);
    }
}
