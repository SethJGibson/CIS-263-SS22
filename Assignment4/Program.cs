/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 4
 Description: This program utilizes and tests the functionality of binary search trees. 
                The tree is first tested by filling it with a tree that satisfies the 
                search tree order property and verifying it, followed by an altered tree
                that does not satisfy the property and verifying that as well. The 
                program then demonstrates that the verification algorithm operates at 
                O(n).
---------------------------------------------------------------------------------------*/

using System;

namespace Assignment4
{
    public class binaryTreeNode
    {
        public int? key = null;
        public binaryTreeNode parent { get; set; } = null;
        public binaryTreeNode left { get; set; } = null;
        public binaryTreeNode right { get; set; } = null;
    }

    public struct binaryTree
    {
        public binaryTreeNode root;

        public void addNode(int x)
        {
            binaryTreeNode insert = null;
            binaryTreeNode newNode = new binaryTreeNode() { key = x };
            binaryTreeNode cursor = root;
            while (cursor != null)
            {
                insert = cursor;
                if (x < cursor.key)
                    cursor = cursor.left;
                else
                    cursor = cursor.right;
            }

            newNode.parent = insert;

            if (insert == null)
                root = newNode;
            else if (newNode.key < insert.key)
                insert.left = newNode;
            else
                insert.right = newNode;
        }

        public bool efficientVerification(binaryTreeNode root, int? min, int? max)                          // This function is O(n)
        {
            if (root == null)
                return true;
            if (root.key < min || root.key > max)
                return false;
            return (efficientVerification(root.left, min, root.key - 1) && efficientVerification(root.right, root.key + 1, max));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var SW = new System.Diagnostics.Stopwatch();
            binaryTree firstBT = new binaryTree();

            firstBT.addNode(50);
            firstBT.addNode(25);
            firstBT.addNode(75);
            firstBT.addNode(100);
            firstBT.addNode(12);
            firstBT.addNode(37);
            firstBT.addNode(51);
            Console.Write("firstBT is ");
            SW.Start();
            bool firstBTResult = firstBT.efficientVerification(firstBT.root, 12, 100);
            SW.Stop();
            if (firstBTResult)
                Console.WriteLine("verified as a BST");
            else
                Console.WriteLine("not a BST");
            Console.WriteLine("firstBT: " + SW.Elapsed);

            binaryTree secondBT = firstBT;
            secondBT.root.right.left.key = 45;        // Change 51 to 45
            Console.Write("secondBT is ");
            SW.Start();
            bool secondBTResult = secondBT.efficientVerification(secondBT.root, 12, 100);
            SW.Stop();
            if (secondBTResult)
                Console.WriteLine("verified as a BST");
            else
                Console.WriteLine("not a BST");
            Console.WriteLine("secondBT: " + SW.Elapsed);

            binaryTree thirdBT = firstBT;
            secondBT.root.right.left.key = 51;
            thirdBT.addNode(6);
            thirdBT.addNode(18);
            thirdBT.addNode(30);
            thirdBT.addNode(42);
            thirdBT.addNode(60);
            thirdBT.addNode(80);
            thirdBT.addNode(110);
            Console.Write("thirdBT is ");
            SW.Start();
            bool thirdBTResult = thirdBT.efficientVerification(thirdBT.root, 6, 110);
            SW.Stop();
            if (thirdBTResult)
                Console.WriteLine("verified as a BST");
            else
                Console.WriteLine("not a BST");
            Console.WriteLine("thirdBT: " + SW.Elapsed);

            binaryTree fourthBT = thirdBT;
            fourthBT.addNode(9);
            fourthBT.addNode(15);
            fourthBT.addNode(20);
            fourthBT.addNode(35);
            fourthBT.addNode(48);
            fourthBT.addNode(78);
            fourthBT.addNode(105);
            Console.Write("fourthBT is ");
            SW.Start();
            bool fourthBTResult = fourthBT.efficientVerification(fourthBT.root, 6, 110);
            SW.Stop();
            if (fourthBTResult)
                Console.WriteLine("verified as a BST");
            else
                Console.WriteLine("not a BST");
            Console.WriteLine("fourthBT: " + SW.Elapsed);

            binaryTree fifthBT = fourthBT;
            fifthBT.addNode(8);
            fifthBT.addNode(16);
            fifthBT.addNode(22);
            fifthBT.addNode(46);
            fifthBT.addNode(76);
            fifthBT.addNode(79);
            fifthBT.addNode(108);
            Console.Write("fifthBT is ");
            SW.Start();
            bool fifthBTResult = fifthBT.efficientVerification(fifthBT.root, 6, 110);
            SW.Stop();
            if (fifthBTResult)
                Console.WriteLine("verified as a BST");
            else
                Console.WriteLine("not a BST");
            Console.WriteLine("fourthBT: " + SW.Elapsed);
        }
    }
}
