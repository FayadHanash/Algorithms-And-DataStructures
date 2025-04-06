using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSet
{
    /// <summary>
    /// interface that defines the key of type T 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IKey<T> : IComparable
    {
        T TKey { get; }
    }

    /// <summary>
    /// a generic class that implements IKey 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Item<T> : IKey<T>
    {
        public Item(T tKey)
        {
            TKey = tKey;
        }
        public T TKey { get; set; }
        public int CompareTo(object? obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is Item<T>))
                throw new ArgumentException("Object is not Item<T>");
            return Comparer<T>.Default.Compare(TKey, ((Item<T>)obj).TKey);
        }
    }
}
