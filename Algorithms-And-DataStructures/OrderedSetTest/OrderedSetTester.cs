using OrderedSet;
namespace OrderedSetTest
{
    [TestClass]
    public class OrderedSetTester
    {
        /// <summary>
        /// Tests the OrderedSet class.
        /// </summary>
        [TestMethod]
        public void Test()
        {

            var t = new OrderedSet<string, string>();
            t.Insert("F");
            t.Insert("D");
            t.Insert("M");
            Assert.IsTrue(t.Tree.Root.IsRed == false && t.Tree.Root.Left.IsRed == true && t.Tree.Root.Right.IsRed == true);
            t.Insert("B"); // case 1 both parent and uncle are red
            Assert.IsTrue(t.Tree.Root.IsRed == false && t.Tree.Root.Left.IsRed == false && t.Tree.Root.Right.IsRed == false && t.Tree.Root.Left.Left.IsRed == true);
            t.Insert("C"); //case 2 and 3, case 2: parent is red, uncle is black, and path from D to B to C meskes zig zag.
            // efter Left Rotation reduces the case 3. uncale is NIL and parent is red and path from D to C to B is straight line 
            Assert.IsTrue(!t.Tree.Root.IsRed && !t.Tree.Root.Right.IsRed && !t.Tree.Root.Left.IsRed && t.Tree.Root.Left.Right.IsRed && t.Tree.Root.Left.Left.IsRed);

            t.Insert("K");
            t.Insert("P");
            Assert.IsTrue(!t.Tree.Root.IsRed && !t.Tree.Root.Left.IsRed && !t.Tree.Root.Right.IsRed && t.Tree.Root.Right.Left.IsRed && t.Tree.Root.Right.Right.IsRed);
            t.Insert("R"); // case 1. parent P is red and uncle K is red. 
            Assert.IsTrue(!t.Tree.Root.IsRed && t.Tree.Root.Right.IsRed && !t.Tree.Root.Right.Left.IsRed && !t.Tree.Root.Right.Right.IsRed && t.Tree.Root.Right.Right.Right.IsRed);
            t.Insert("Q"); // case 2 and 3. case 2 : parent i red R and uncle is black nil and the path from p to R to Q makes zig zag.
                           // efter Right Rotation reduces the case 3. uncale is NIL and parent is red and path from P to Q(red) to R(red) is straight line
            Assert.IsTrue(!t.Tree.Root.IsRed && t.Tree.Root.Right.IsRed && !t.Tree.Root.Right.Left.IsRed && !t.Tree.Root.Right.Right.IsRed && t.Tree.Root.Right.Right.Left.IsRed && t.Tree.Root.Right.Right.Right.IsRed);
            try
            {
                Assert.ThrowsException<ArgumentNullException>(() => t.Insert(null), "Inserting null should throw an exception");
                Assert.ThrowsException<Exception>(() => t.Search("L"), "Searching for a non-existing element should throw an exception");
                Assert.ThrowsException<Exception>(() => t.Predecessor(t.Minimum()), "Predecessor of a minimum element should throw an exception");
                Assert.ThrowsException<Exception>(() => t.Successor(t.Maximum()), "Successor of a maximum element should throw an exception");
            }
            catch { }
            Assert.IsTrue(t.Count() == 9, "Count should be 9");
            Assert.IsTrue(t.Minimum() == "B", "Minimum should be B");
            Assert.IsTrue(t.Maximum() == "R", "Maximum should be R");
            Assert.IsTrue(t.Successor("B") == "C", "Successor of B should be C");
            Assert.IsFalse(t.Successor("C") == "B", "Successor of C should be D");
            Assert.IsTrue(t.Predecessor("R") == "Q", "Predecessor of R should be Q");
            Assert.AreNotEqual(t.Search(t.Minimum()), null, "Searching for minimum should return a node");
            Assert.IsTrue(t.Height(t.Tree.Root) == 5, "Height from Root should be 5 incuding the root");
            Assert.AreEqual(t.Height(t.Minimum()), 2, "Height of minimum should be 2");
            Assert.AreEqual(t.Height(t.Maximum()), 2, "Height of maximum should be 2");
            Assert.AreEqual(t.BlachHeight(t.Tree.Root), 3, "Black height from root to nil should be 3");
            Assert.AreEqual(t.BlachHeight(t.Minimum()), 1, "Black height of minimum should be 1");
            Assert.AreEqual(t.BlachHeight(t.Maximum()), 1, "Black height of maximum should be 1");
            Assert.AreEqual(t.ShortestPath(t.Tree.Root.Key, t.Maximum()), 1, "Shortest path from root to maximum should be 1");
            Assert.AreEqual(t.ShortestPath(t.Minimum(), t.Maximum()), 3, "Shortest path from minimum to maximum should be 3");
            Assert.AreEqual(t.LongestPath(t.Minimum(), t.Maximum()), 5, "Longest path from minimum to maximum should be 5");
            Assert.AreEqual(t.LongestPath(t.Tree.Root.Key, t.Maximum()), 2, " Longest path from root to maximum should be 2");

          
            var c = new OrderedSet<Person, Person>();
            try
            {
                Assert.ThrowsException<NullReferenceException>(() => c.Search(new Person("A", "A")), "Searching for a element in empty tree should throw an exception");
            }
            catch { }
            c.Insert(new Person("F", "F"));
            c.Insert(new Person("G", "G"));
            c.Insert(new Person("A", "A"));
            c.Insert(new Person("B", "B"));

            var c2 = new OrderedSet<Person, Person>();
            try
            {
                Assert.ThrowsException<ArgumentNullException>(() => c.UnionWith(c2), "Union with null should throw an exception");
            }
            catch { }
            c2.Insert(new Person("N", "N"));
            c2.Insert(new Person("M", "M"));
            c2.Insert(new Person("C", "C"));
            c2.Insert(new Person("D", "D"));
            c.UnionWith(c2);
            Assert.IsTrue(c.Count() == 8, "Count should be 8");
            Assert.IsTrue(c.Minimum().CompareTo(new Person("A", "A")) == 0, "Minimum should be A");

            // The answer to the question (How do we know that the tree is correct after union/inserting) is:
            // The path between the root and all leaves must have the same number of black nodes.
            // The tree is correct if the following conditions are met: 

            Assert.IsTrue(c.ShortestPath(new Person("F", "F"), new Person("A", "A")) == 1, "Shortest path from F to A should be 1");
            Assert.IsTrue(c.ShortestPath(new Person("F", "F"), new Person("D", "D")) == 1, "Shortest path from F to D should be 1");
            Assert.IsTrue(c.ShortestPath(new Person("F", "F"), new Person("N", "N")) == 1, "Shortest path from F to N should be 2");
            Assert.IsTrue(c.ShortestPath(new Person("F", "F"), new Person("G", "G")) == 1, "Shortest path from F to G should be 2");

            // The Analys of part 3:
            //How do the heights and the length of the paths correlate and are they within
            //the expected bounds of a correctly implemented red-black tree?
            //The height of the tree is the number of edges on the longest path from the root to a leaf.(longest path)
            //The length of the path from the root to a node is the number of edges on the path from the root to the node.
            //The shortest path in red black tree is the black height of the tree. the longest is at most twice the shortest.
            //
            //the hight of the tree should less or equal to 2log2(number of nodes + 1) or the number of nodes greater or equal than 2^height -1.
            // height <= 2log2(n+ 1) or n >= 2^height -1
            Assert.IsTrue(c.Count() == 8, "Count should be 8");
            Assert.IsTrue(c.Height(c.Minimum()) == 2, "height should be 2");
            // we already have eight nodes, so the height should be 2, if we test the ekvation above so it should be true.
            //  2 <= 2log2(8+1) => 2 <= 6.34 or 8 >= 2^2 -1


            c.Insert(new Person("L", "L"));
            c.Insert(new Person("K", "K"));
            c.Insert(new Person("J", "J"));
            c.Insert(new Person("I", "I"));
            c.Insert(new Person("H", "H"));
            c.Insert(new Person("E", "E"));
            c.Insert(new Person("C", "C"));
            c.Insert(new Person("B", "B"));
            c.Insert(new Person("A", "A"));
            c.Insert(new Person("N", "N"));
            c.Insert(new Person("M", "M"));
            c.Insert(new Person("O", "O"));
            c.Insert(new Person("P", "P"));
            c.Insert(new Person("Q", "Q"));
            c.Insert(new Person("R", "R"));
            c.Insert(new Person("S", "S"));
            Assert.IsTrue(c.Count() == 24, "Count should be 24");
            Assert.IsTrue(c.Height(c.Minimum()) == 3, "height should be 3");
            // then we insert further 16 items (nodes) so the total items is 24 and the height should be 3.
            // (3 <= 3log3(24+1)) => (3 <= 9.3) or (24 >= 2^3 -1)


        }
        /// <summary>
        /// person class to test the set
        /// </summary>
        class Person : IComparable<Person>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public Person(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }
            public int CompareTo(Person? other)
              => other == null ? 1
                : FirstName == other.FirstName ? LastName.CompareTo(other.LastName)
                  : FirstName.CompareTo(other.FirstName);
            public override string ToString() => $"({FirstName} {LastName})";
        }
    }
}