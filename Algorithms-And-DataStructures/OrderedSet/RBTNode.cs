using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderedSet
{
    public class RBTNode<IKey>
    {
        /// <summary>
        /// Default constructor
        /// nullify all fields
        /// </summary>
        public RBTNode()
        {
            IsRed = false;
            Left = null;
            Right = null;
            Parent = null;
            Key = default;
        }

        /// <summary>
        /// constructor with two parameters
        /// </summary>
        /// <param name="key">item</param>
        /// <param name="isRed">color</param>
        public RBTNode(IKey key, bool isRed)
        {
            Key = key;
            IsRed = isRed;
            Left = null;
            Right = null;
            Parent = null;

        }

        /// <summary>
        /// node fields with setter and getter access
        /// </summary>
        public IKey? Key { get; set; }
        public RBTNode<IKey>? Left { get; set; }
        public RBTNode<IKey>? Right { get; set; }
        public RBTNode<IKey>? Parent { get; set; }
        public bool IsRed { get; set; }
    }
}
