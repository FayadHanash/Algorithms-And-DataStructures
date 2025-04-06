using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSet
{
    public interface IOrderedSet<TKey, TItem> : IEnumerable<TItem> where TItem : class, TKey
    {
        /// <summary>
        /// returns the number of items in the set
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Method that inserts an item into the set
        /// </summary>
        /// <param name="item">the item to be inserted</param>
        void Insert(TItem item);
        /// <summary>
        /// Method that searchs for a given key and returns its item
        /// </summary>
        /// <param name="key">the key to be searched</param>
        /// <returns>an item if found</returns>
        TItem? Search(TKey key);
        /// <summary>
        /// Method that returns minimum item in the set collection
        /// </summary>
        /// <returns>minimum item</returns>
        TItem? Minimum();
        /// <summary>
        /// Method that returns maximum item in the set colletion
        /// </summary>
        /// <returns>maximum item</returns>
        TItem? Maximum();
        /// <summary>
        /// Method that returns the successor of a given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>successor of a given item</returns>
        TItem? Successor(TItem item);

        /// <summary>
        /// Method that returns the predecessor of a given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>predecessor of a given item</returns>
        TItem? Predecessor(TItem item);
        /// <summary>
        ///  adds the items in a tree(collection) to other collection 
        ///  the complexity is O(n)
        /// </summary>
        /// <param name="other">other collection to be unioned</param>
        /// <exception cref="Exception">throw exception if other is null</exception>
        void UnionWith(IEnumerable<TItem> other);
    }
}
