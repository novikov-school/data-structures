﻿using BinaryTree;
using NUnit.Framework;

namespace BinaryTreeTests
{
    class RemoveTests
    {
        [Test]
        public void Remove_Head()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //        4
            //       / \
            //      2   5
            //     / \   \
            //    1   3   7
            //           / \
            //          6   8

            tree.Add(4);
            tree.Add(5);
            tree.Add(2);
            tree.Add(7);
            tree.Add(3);
            tree.Add(6);
            tree.Add(1);
            tree.Add(8);

            tree.Remove(4);

            //        5
            //       /   \
            //      2      7
            //     / \    / \
            //    1   3  6  8


            int[] expected = new[] { 1, 3, 2, 6, 8, 7, 5, };

            int index = 0;

            tree.PostOrderTraversal(item => Assert.AreEqual(expected[index++], item, "The item enumerated in the wrong order"));
        }

        [Test]
        public void Remove_Head_Line_Right()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            // 1
            //  \
            //   2
            //    \
            //     3


            tree.Add(1);
            tree.Add(2);
            tree.Add(3);

            tree.Remove(1);

            // 2
            //  \
            //   3


            int[] expected = new[] { 3, 2 };

            int index = 0;

            tree.PostOrderTraversal(item => Assert.AreEqual(expected[index++], item, "The item enumerated in the wrong order"));
        }

        [Test]
        public void Remove_Head_Line_Left()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //     3
            //    /
            //   2
            //  /
            // 1


            tree.Add(3);
            tree.Add(2);
            tree.Add(1);

            tree.Remove(3);

            //   2
            //  /
            // 1

            int[] expected = new[] { 1, 2 };

            int index = 0;

            tree.PostOrderTraversal(item => Assert.AreEqual(expected[index++], item, "The item enumerated in the wrong order"));
        }



        [Test]
        public void Remove_Head_Only_Node()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            tree.Add(4);

            Assert.IsTrue(tree.Remove(4), "Remove should return true for found node");

            foreach (int item in tree)
            {
                Assert.Fail("An empty tree should not enumerate any values");
            }
        }

        [Test]
        public void Remove_Node_No_Left_Child()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //        4
            //       / \
            //      2   5
            //     / \   \
            //    1   3   7
            //           / \
            //          6   8

            tree.Add(4);
            tree.Add(5);
            tree.Add(2);
            tree.Add(7);
            tree.Add(3);
            tree.Add(6);
            tree.Add(1);
            tree.Add(8);

            Assert.IsTrue(tree.Remove(5), "Remove should return true for found node");

            //        4
            //       /  \
            //      2    6
            //     / \    \
            //    1   3    7
            //              \
            //               8

            int[] expected = new[] { 1, 3, 2, 8, 7, 6, 4, };

            int index = 0;

            tree.PostOrderTraversal(item => Assert.AreEqual(expected[index++], item, "The item enumerated in the wrong order"));
        }

        [Test]
        public void Remove_Node_Right_Leaf()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //        4
            //       / \
            //      2   5
            //     / \   \
            //    1   3   7
            //           / \
            //          6   8

            tree.Add(4);
            tree.Add(5);
            tree.Add(2);
            tree.Add(7);
            tree.Add(3);
            tree.Add(6);
            tree.Add(1);
            tree.Add(8);

            Assert.IsTrue(tree.Remove(8), "Remove should return true for found node");

            //        4
            //       / \
            //      2   5
            //     / \   \
            //    1   3   7
            //           /
            //          6

            int[] expected = new[] { 1, 3, 2, 6, 7, 5, 4, };

            int index = 0;

            tree.PostOrderTraversal(item => Assert.AreEqual(expected[index++], item, "The item enumerated in the wrong order"));
        }

        [Test]
        public void Remove_Node_Left_Leaf()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //        4
            //       / \
            //      2   5
            //     / \   \
            //    1   3   7
            //           / \
            //          6   8

            tree.Add(4);
            tree.Add(5);
            tree.Add(2);
            tree.Add(7);
            tree.Add(3);
            tree.Add(6);
            tree.Add(1);
            tree.Add(8);

            Assert.IsTrue(tree.Remove(1), "Remove should return true for found node");

            //        4
            //       / \
            //      2   5
            //       \   \
            //        3   7
            //           / \
            //          6   8

            int[] expected = new[] { 3, 2, 6, 8, 7, 5, 4, };

            int index = 0;

            tree.PostOrderTraversal(item => Assert.AreEqual(expected[index++], item, "The item enumerated in the wrong order"));
        }


        [Test]
        public void Remove_Current_Right_Has_No_Left()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //         4
            //       /   \
            //      2     6
            //     / \    /\
            //    1   3  5  7
            //               \
            //                8

            tree.Add(4);
            tree.Add(6);
            tree.Add(5);
            tree.Add(2);
            tree.Add(7);
            tree.Add(3);
            tree.Add(1);
            tree.Add(8);

            Assert.IsTrue(tree.Remove(6), "Remove should return true for found node");

            //          4
            //       /    \
            //      2      7
            //     / \    / \
            //    1   3  5   8

            int[] expected = new[] { 1, 3, 2, 5, 8, 7, 4, };

            int index = 0;

            tree.PostOrderTraversal(item => Assert.AreEqual(expected[index++], item, "The item enumerated in the wrong order"));
        }

        [Test]
        public void Remove_Current_Has_No_Right()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //         4
            //       /   \
            //      2     8 
            //     / \    /
            //    1   3  6
            //          / \
            //         5   7   

            tree.Add(4);
            tree.Add(2);
            tree.Add(1);
            tree.Add(3);
            tree.Add(8);
            tree.Add(6);
            tree.Add(7);
            tree.Add(5);

            Assert.IsTrue(tree.Remove(8), "Remove should return true for found node");

            //         4
            //       /   \
            //      2      6 
            //     / \    / \
            //    1   3  5   7

            int[] expected = new[] { 1, 3, 2, 5, 7, 6, 4, };

            int index = 0;

            tree.PostOrderTraversal(item => Assert.AreEqual(expected[index++], item, "The item enumerated in the wrong order"));
        }

        [Test]
        public void Remove_Current_Right_Has_Left()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //         4
            //       /    \
            //      2      6 
            //     / \    / \
            //    1   3  5   8
            //              /
            //             7

            tree.Add(4);
            tree.Add(2);
            tree.Add(1);
            tree.Add(3);
            tree.Add(6);
            tree.Add(5);
            tree.Add(8);
            tree.Add(7);

            Assert.IsTrue(tree.Remove(6), "Remove should return true for found node");

            //         4
            //       /    \
            //      2      7 
            //     / \    / \
            //    1   3  5   8

            int[] expected = new[] { 1, 3, 2, 5, 8, 7, 4, };

            int index = 0;

            tree.PostOrderTraversal(item => Assert.AreEqual(expected[index++], item, "The item enumerated in the wrong order"));
        }

        [Test]
        public void Remove_From_Empty()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.IsFalse(tree.Remove(10));
        }

        [Test]
        public void Remove_Missing_From_Tree()
        {
            BinaryTree<int> tree = new BinaryTree<int>();

            //         4
            //       /   \
            //      2     8 
            //     / \    /
            //    1   3  6
            //          / \
            //         5   7   

            int[] values = new[]{4, 2, 1, 3, 8, 6, 7, 5};

            foreach(int i in values)
            {
                Assert.IsFalse(tree.Contains(10), "Tree should not contain 10");
                tree.Add(i);
            }
        }
    }
}
