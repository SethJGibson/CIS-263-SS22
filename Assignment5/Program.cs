/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 5
 Description: This program utilizes and tests the functionality of Red-Black Trees. 
                The tree is implemented by filling it with test values and arranging the
                tree accordingly, then printing its contents to verify the placement of
                the nodes. Afterwards, one node is removed from the tree using the delete
                method and the tree is printed once more to verify that the process was
                successful. 
---------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Assignment5
{
    public class redBlackNode
    {
        public int? key = null;
        public redBlackNode parent { get; set; } = null;
        public redBlackNode left { get; set; } = null;
        public redBlackNode right { get; set; } = null;
        public bool color { get; set; } = false;    // false = Black, true = Red

        public redBlackNode(int key) { this.key = key; }
    }

    public struct redBlackTree
    {
        public redBlackNode root;
        public redBlackNode nil;

        public void leftRotate(redBlackNode node)
        {
            redBlackNode placeHolder = node.right;
            node.right = placeHolder.left;
            if (placeHolder.left != nil)
                placeHolder.left.parent = node;
            placeHolder.parent = node.parent;
            if (node.parent == nil)
                root = placeHolder;
            else if (node == node.parent.left)
                node.parent.left = placeHolder;
            else
                node.parent.right = placeHolder;
            placeHolder.left = node;
            node.parent = placeHolder;
        }

        public void rightRotate(redBlackNode node)
        {
            redBlackNode placeHolder = node.left;
            node.left = placeHolder.right;
            if (placeHolder.right != nil)
                placeHolder.right.parent = node;
            placeHolder.parent = node.parent;
            if (node.parent == nil)
                root = placeHolder;
            else if (node == node.parent.right)
                node.parent.right = placeHolder;
            else
                node.parent.left = placeHolder;
            placeHolder.right = node;
            node.parent = placeHolder;
        }

        public void insertFixup(redBlackNode newNode)
        {
            redBlackNode placeHolder = null;

            while (newNode.parent != null && newNode.parent.color == true) 
            {
                if (newNode.parent == newNode.parent.parent.left)
                {
                    placeHolder = newNode.parent.parent.right;
                    if (placeHolder.color == true)
                    {
                        newNode.parent.color = false;
                        placeHolder.color = false;
                        newNode.parent.parent.color = true;
                        newNode = newNode.parent.parent;
                    }
                    else 
                    {
                        if (newNode == newNode.parent.right)
                        {
                            newNode = newNode.parent;
                            leftRotate(newNode);
                        }
                        newNode.parent.color = false;
                        newNode.parent.parent.color = true;
                        rightRotate(newNode.parent.parent);
                    }
                }
                else
                {
                    placeHolder = newNode.parent.parent.left;
                    if (placeHolder.color == true)
                    {
                        newNode.parent.color = false;
                        placeHolder.color = false;
                        newNode.parent.parent.color = true;
                        newNode = newNode.parent.parent;
                    }
                    else
                    {
                        if (newNode == newNode.parent.left)
                        {
                            newNode = newNode.parent;
                            rightRotate(newNode);
                        }
                        newNode.color = false;
                        newNode.parent.parent.color = true;
                        leftRotate(newNode.parent.parent);
                    }
                }
            }
            root.color = false;
        }

        public void insert(redBlackNode newNode)
        {
            redBlackNode placeHolder = nil;
            redBlackNode cursor = root;

            while (cursor != nil)
            {
                placeHolder = cursor;
                if (newNode.key < cursor.key)
                    cursor = cursor.left;
                else
                    cursor = cursor.right;
            }
            newNode.parent = placeHolder;

            if (placeHolder == nil)
                root = newNode;
            else if (newNode.key < placeHolder.key)
                placeHolder.left = newNode;
            else
                placeHolder.right = newNode;

            newNode.left = nil;
            newNode.right = nil;
            newNode.color = true;

            insertFixup(newNode);
        }

        public void printTree(redBlackNode rootNode, int indent)
        {
            if (rootNode != nil)
            {
                printTree(rootNode.right, indent + 1);
                for (int i = 0; i < indent; i++)
                    Console.Write("    ");
                if (rootNode.color)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-|" + rootNode.key);
                Console.ResetColor();
                printTree(rootNode.left, indent + 1);
            }
        }

        public void transplant(redBlackNode first, redBlackNode second)
        {
            if (first.parent == nil)
                root = second;
            else if (first == first.parent.left)
                first.parent.left = second;
            else
                first.parent.right = second;
            second.parent = first.parent;
        }

        public void deleteFixup(redBlackNode node)
        {
            while (node != null && node != root && node.color == false)
            {
                if (node == node.parent.left)
                {
                    redBlackNode placeHolder = node.parent.right;
                    if (placeHolder.color == true)
                    {
                        placeHolder.color = false;
                        node.parent.color = true;
                        leftRotate(node.parent);
                        placeHolder = node.parent.right;
                    }
                    if (placeHolder.left.color == false && placeHolder.right.color == false)
                    {
                        placeHolder.color = true;
                        node = node.parent;
                    }
                    else
                    {
                        if (placeHolder.right.color == false)
                        {
                            placeHolder.left.color = false;
                            placeHolder.color = true;
                            rightRotate(placeHolder);
                            placeHolder = node.parent.right;
                        }
                        placeHolder.color = node.parent.color;
                        node.parent.color = false;
                        placeHolder.right.color = false;
                        leftRotate(node.parent);
                        node = root;
                    }
                }
                else
                {
                    redBlackNode placeHolder = node.parent.left;
                    if (placeHolder.color == true)
                    {
                        placeHolder.color = false;
                        node.parent.color = true;
                        rightRotate(node.parent);
                        placeHolder = node.parent.left;
                    }
                    if (placeHolder.right.color == false && placeHolder.left.color == false)
                    {
                        placeHolder.color = true;
                        node = node.parent;
                    }
                    else
                    {
                        if (placeHolder.left.color == false)
                        {
                            placeHolder.right.color = false;
                            placeHolder.color = true;
                            leftRotate(placeHolder);
                            placeHolder = node.parent.left;
                        }
                        placeHolder.color = node.parent.color;
                        node.parent.color = false;
                        placeHolder.left.color = false;
                        rightRotate(node.parent);
                        node = root;
                    }
                }
            }
        }

        public void delete(redBlackNode delNode)
        {
            redBlackNode placeHolder = delNode;
            redBlackNode cursor = null;
            bool originalColor = placeHolder.color;

            if (delNode.left == nil)
            {
                cursor = delNode.right;
                transplant(delNode, delNode.right);
            }
            else if (delNode.right == nil)
            {
                cursor = delNode.left;
                transplant(delNode, delNode.left);
            }
            else
            {
                originalColor = placeHolder.color;
                delNode = placeHolder.right;
                if (placeHolder.parent == delNode)
                    delNode.parent = placeHolder;
                else
                {
                    transplant(placeHolder, placeHolder.right);
                    placeHolder.right = delNode.right;
                    placeHolder.right.parent = placeHolder;
                }
                transplant(delNode, placeHolder);
                placeHolder.left = delNode.left;
                placeHolder.left.parent = placeHolder;
                placeHolder.color = delNode.color;
            }
            if (originalColor == false)
                deleteFixup(cursor);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            redBlackTree testTree = new redBlackTree();
            testTree.nil = new redBlackNode(0);
            testTree.root = testTree.nil;

            Console.WriteLine("1st");
            testTree.insert(new redBlackNode(55));
            testTree.insert(new redBlackNode(40));
            testTree.insert(new redBlackNode(65));
            testTree.insert(new redBlackNode(60));
            testTree.insert(new redBlackNode(75));
            testTree.insert(new redBlackNode(57));
            testTree.printTree(testTree.root, 0);

            Console.WriteLine("________________________\n2nd");
            testTree.delete(testTree.root.right.right);
            testTree.printTree(testTree.root, 0);
        }
    }
}
