using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSet
{
    public class RBTree<TKey, TItem> where TItem : TKey
    {
        public RBTNode<TItem> Root; // root of the tree
        public RBTNode<TItem> NIL = new RBTNode<TItem>(); // sentinel node
        IComparer<TItem> comparer; // comparer object
        int count; // number of elements in the tree

        /// <summary>
        /// Default constructor
        /// </summary>
        public RBTree() : this(Comparer<TItem>.Default)
        {
            Root = NIL;
        }
        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="comparer">comparere object of type IComparere<TItem></param>
        /// <exception cref="Exception">throw a exception if comparer is null</exception>
        public RBTree(IComparer<TItem> comparer)
        {
            if (comparer == null) throw new Exception("comparer is null");
            this.comparer = comparer;
        }

        /// <summary>
        /// Property returns the number of items in the set
        /// </summary>
        public int Count => count;
        /// <summary>
        /// Method that inserts an item into the set
        /// </summary>
        /// <param name="item">the item to be inserted</param>
        /// <exception cref="ArgumentNullException">throw a exception if a item is default</exception>
        public void Insert(TItem item)
        {
            if (item.Equals(default(TItem))) throw new ArgumentNullException("Item cannot be default");
            RBTNode<TItem> n = new RBTNode<TItem>() { Key = item, IsRed = true, Left = NIL, Right = NIL, Parent = NIL };

            var y = NIL;
            var x = Root;
            while (x != NIL)
            {
                y = x;
                x = comparer.Compare(item, x.Key) < 0 ? x.Left : x.Right;
            }
            n.Parent = y;
            _ = y == NIL ? Root = n : comparer.Compare(item, y.Key) < 0 ? y.Left = n : y.Right = n;
            RBTInsertFixUP(n);
            count++;

        }

        /// <summary>
        /// Method that maintain the property of red black tree if violated (recolor)
        /// </summary>
        /// <param name="node">the new node has been inserted</param>
        public void RBTInsertFixUP(RBTNode<TItem> node)
        {

            while (node.Parent.IsRed)
            {

                if (node.Parent == node.Parent.Parent.Left)
                {
                    var y = node.Parent.Parent.Right;
                    if (y.IsRed)
                    {
                        node.Parent.IsRed = false;
                        y.IsRed = false;
                        node.Parent.Parent.IsRed = true;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }
                        node.Parent.IsRed = false;
                        node.Parent.Parent.IsRed = true;
                        
                        RightRotate(node.Parent.Parent);
                    }

                }
                else
                {
                    var y = node.Parent.Parent.Left;
                    if (y.IsRed)
                    {
                        node.Parent.IsRed = false;
                        y.IsRed = false;
                        node.Parent.Parent.IsRed = true;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        node.Parent.IsRed = false;
                        node.Parent.Parent.IsRed = true;
                        LeftRotate(node.Parent.Parent);
                    }
                }
            }
            Root.IsRed = false;
        }
        /// <summary>
        /// Rotates at right
        /// </summary>
        /// <param name="n"></param>
        void RightRotate(RBTNode<TItem> n)
        {
            var y = n.Left;
            n.Left = y.Right;
            if (y.Right != NIL) y.Right.Parent = n;
            y.Parent = n.Parent;
            _ = n.Parent == NIL ? Root = y : n == n.Parent.Right ? n.Parent.Right = y : n.Parent.Left = y;
            y.Right = n;
            n.Parent = y;
        }
        /// <summary>
        /// Rotates at left
        /// </summary>
        /// <param name="n"></param>
        void LeftRotate(RBTNode<TItem> n)
        {
            var y = n.Right;
            n.Right = y.Left;
            if (y.Left != NIL) y.Left.Parent = n;
            y.Parent = n.Parent;
            _ = n.Parent == NIL ? Root = y : n == n.Parent.Left ? n.Parent.Left = y : n.Parent.Right = y;
            y.Left = n;
            n.Parent = y;
        }

        /// <summary>
        /// Method that returns maximum item in the set colletion
        /// </summary>
        /// <returns>maximum item</returns>
        public TItem Maximum() => Maximum(Root).Key;
        RBTNode<TItem> Maximum(RBTNode<TItem> x)
        {
            IsEmptyTree();
            while (x.Right != NIL)
                x = x.Right;
            return x;
        }

        /// <summary>
        /// Method that returns minimum item in the set collection
        /// </summary>
        /// <returns>minimum item</returns>
        public TItem Minimum() => Minimum(Root).Key;
        RBTNode<TItem> Minimum(RBTNode<TItem> x)
        {
            IsEmptyTree();
            while (x.Left != NIL)
                x = x.Left;
            return x;
        }
        /// <summary>
        /// Determines if a item is in the tree and returns true if it is or false if it is not
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true or false</returns>
        bool Contains(TItem item)
        {
            var x = Root;
            while (x != NIL)
            {
                if (comparer.Compare(item, x.Key) == 0) return true;
                else if (comparer.Compare(item, x.Key) < 0) x = x.Left;
                else x = x.Right;
            }
            return false;
        }
        bool TryFind(TKey key, out TItem item)
        {
            var x = Root;
            while (x != NIL)
            {
                if (comparer.Compare((TItem?)key, x.Key) == 0)
                {
                    item = x.Key;
                    return true;
                }
                else if (comparer.Compare((TItem?)key, x.Key) < 0) x = x.Left;
                else x = x.Right;
            }

            item = default;
            return false;
        }

        /// <summary>
        /// ckecks if the tree is empty
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        bool IsEmptyTree() => Root == NIL ? throw new Exception("Tree is Empty") : true;

        /// <summary>
        /// Method that returns the predecessor of a given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>predecessor of a given item</returns>
        /// <exception cref="NullReferenceException">Throw an exception if there isn't predecessor</exception>

        public TItem Predecessor(TItem item)
        {
            var x = Predecessor(ItemToNode(item));
            if (x == NIL) throw new NullReferenceException("No Predecessor");
            return x.Key;
        }
        /// <summary>
        /// Help method that returns the presecessor at a given node
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        RBTNode<TItem> Predecessor(RBTNode<TItem> x)
        {
            IsEmptyTree();
            if (x.Left != NIL) return Maximum(x.Left);
            var y = x.Parent;
            while (y != NIL && x == y.Left)
            {
                x = y;
                y = y.Parent;
            }
            return y;
        }

        /// <summary>
        /// Method that returns the successor of a given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>successor of a given item</returns>
        /// <exception cref="NullReferenceException">Throw an exception if there isn't Successor</exception>

        public TItem Successor(TItem item)
        {
            var x = Successor(ItemToNode(item));
            if (x == NIL) throw new NullReferenceException("No Successor");
            return x.Key;
        }
        /// <summary>
        /// Help method that returns the Successor at a given node
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        RBTNode<TItem> Successor(RBTNode<TItem> x)
        {
            IsEmptyTree();
            if (x.Right != NIL) return Minimum(x.Right);
            var y = x.Parent;
            while (y != NIL && x == y.Right)
            {
                x = y;
                y = y.Parent;
            }
            return y;
        }

        /// <summary>
        /// Method that searchs for a given key and returns its item
        /// </summary>
        /// <param name="key">the key to be searched</param>
        /// <returns>an item if found</returns>
        public TItem Search(TKey key)
        {
            TItem? item = default(TItem);
            IsEmptyTree();
            if (!TryFind(key, out item)) throw new KeyNotFoundException("Key not found");
            return item;
        }


        /// <summary>
        ///  adds the items in a tree(collection) to other collection 
        ///  the complexity is O(n)
        /// </summary>
        /// <param name="other">other collection to be unioned</param>
        /// <exception cref="ArgumentNullException">throw exception if other is null</exception>
        public void UnionWith(IEnumerable<TItem> other)
        {
            if (other == null) throw new ArgumentNullException("The object is empty");
            foreach (var item in other)
            {
                Insert(item);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// the items are enumerator in ascending order( in-order traversal)
        /// </summary>
        /// <returns>items in the collection</returns>
        public IEnumerator<TItem> GetEnumerator() => new RBTEnumerator<TKey, TItem>(Root);

        //IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();



        /// <summary>
        /// Determines if a item is in the tree 
        /// and returns node that contins the item if it is otherwise null if it is not
        /// </summary>
        /// <param name="item">item to be searched</param>
        /// <returns></returns>
        /// <exception cref="Exception">throw a exception if the item not found</exception>
        private RBTNode<TItem> ItemToNode(TItem item)
        {
            RBTNode<TItem> node;
            IsEmptyTree();
            if (!TryFindNode(item, out node)) throw new Exception("Item not found");
            return node;
        }

        /// <summary>
        /// Determines if a item is in the tree 
        /// </summary>
        /// <param name="item">the item to be found</param>
        /// <param name="node">node that contins the item</param>
        /// <returns>true if a item is in tree otherwise false</returns>
        bool TryFindNode(TItem item, out RBTNode<TItem> node)
        {
            node = null;
            var x = Root;

            while (x != NIL)
            {
                if (comparer.Compare(item, x.Key) == 0)
                {
                    node = x;
                    return true;
                }
                else if (comparer.Compare(item, x.Key) < 0) x = x.Left;
                else x = x.Right;
            }
            return false;
        }

        /// <summary>
        /// Method that calculates and returns the hight of the tree at a given node
        /// Source: StackOverFlow
        /// </summary>
        /// <param name="r">the node to be calculated</param>
        /// <returns>Hight of a tree</returns>
        public int Height(RBTNode<TItem> r) => r == NIL ? 1 : 1 + Math.Max(Height(r.Left), Height(r.Right));
        /// <summary>
        /// Method that gets the node of a given item in the tree and calls Height method to calculate the hight
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Height(TItem item) => Height(ItemToNode(item));

        /// <summary>
        /// Method that gets the node of a given item in the tree and 
        /// calls BlackHeight method to calculate the black hight
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int BlackHeight(TItem item) => BlackHeight(ItemToNode(item));

        /// <summary>
        /// Method that calculates and returns the black hight of the tree at a given node
        /// </summary>
        /// <param name="r">the node to be calculated</param>
        /// <returns>Black hight of a tree</returns>
        public int BlackHeight(RBTNode<TItem> r)
        {
            if (r == NIL) return 1;
            int left = BlackHeight(r.Left);
            int right = BlackHeight(r.Right);
            if (left == -1 || right == -1) return -1;
            if (left != right) return -1;
            return r.IsRed == false ? left + 1 : left;
        }

        /// <summary>
        /// Method that returns the lowest common ancestor of two nodes
        /// </summary>
        /// <param name="root">root</param>
        /// <param name="x">first node</param>
        /// <param name="y">second node</param>
        /// <returns>ancestor node</returns>
        RBTNode<TItem>? LowestCommonAncestor(RBTNode<TItem> root, RBTNode<TItem> x, RBTNode<TItem> y)
        {
            if (root == null) return null;
            if (x == root || y == root) return root;
            RBTNode<TItem>? left = LowestCommonAncestor(root.Left, x, y);
            RBTNode<TItem>? right = LowestCommonAncestor(root.Right, x, y);
            if (left != null && right != null) return root;
            return left != null ? left : right;
        }
        /// <summary>
        /// Method that calculate shortest path between two nodes
        /// </summary>
        /// <param name="from">source</param>
        /// <param name="to">destination</param>
        /// <returns>shortest path</returns>
        /// <exception cref="Exception">throw a exception if node is null</exception>
        public int ShortestPath(RBTNode<TItem> from, RBTNode<TItem> to)
        {
            if (from == NIL || to == NIL) throw new Exception("Node is null");
            int sp = Math.Abs(BlackHeight(from) + BlackHeight(to) - 2 * BlackHeight(LowestCommonAncestor(Root, from, to)));
            return from.Equals(to) ? 0 : ((from.IsRed || !from.IsRed) && to.IsRed) ? sp - 1 : sp;
        }
        /// <summary>
        /// Method that gets the nodes of a given items in the tree and 
        /// calls ShortestPath method to calculate the Shortest Path
        /// </summary>
        /// <param name="from">source</param>
        /// <param name="to">destination</param>
        /// <returns>shortest path</returns>
        public int ShortestPath(TItem from, TItem to) => ShortestPath(ItemToNode(from), ItemToNode(to));

        /// <summary>
        /// Method that calculate longest path between two nodes
        /// </summary>
        /// <param name="from">source</param>
        /// <param name="to">destination</param>
        /// <returns>longest path</returns>
        /// <exception cref="Exception">throw a exception if node is null</exception>
        public int LongestPath(RBTNode<TItem> from, RBTNode<TItem> to)
        {
            if (from == NIL || to == NIL) throw new Exception("Node is null");
            int lp = Math.Abs(Height(from) + Height(to) - 2 * Height(LowestCommonAncestor(Root, from, to))) - 1;
            return from.Equals(to) ? 0 : lp == 0 ? 1 : (!from.IsRed && !to.IsRed) ? lp + 1 : lp;
        }
        /// <summary>
        /// Method that gets the nodes of a given items in the tree and 
        /// calls LongestPath method to calculate the longest Path
        /// </summary>
        /// <param name="from">source</param>
        /// <param name="to">destination</param>
        /// <returns>longest path</returns>
        public int LongestPath(TItem from, TItem to) => LongestPath(ItemToNode(from), ItemToNode(to));
    }

#pragma warning restore CS8602 // Dereference of a possibly null reference.
}
