/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 6
 Description: This program utilizes and tests the functionality of B Trees. 
                The tree is implemented by filling it with test values and arranging the
                tree accordingly, then printing its contents to verify the placement of
                the nodes. Afterwards, one value is added to the tree at a time, 
                predictably altering the tree, and the tree is printed after every 
                addition to verify that the process was successful. 
---------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Assignment6
{
    public class bNode
    {
        public int n = 0;
        public List<char?> keys = new List<char?>();
        public bool leaf = false;
        public List<bNode> children = new List<bNode>();

        public bNode() { }
        public bNode(int n, List<char?> keys, bool leaf, List<bNode> children) { this.n = n; this.keys = keys; this.leaf = leaf; this.children = children; }
    }

    public struct bTree
    {
        public bNode root;
        public int t;

        public void create(int tInput)
        {
            bNode x = new bNode();    
            x.leaf = true;
            x.n = 0;
            //diskWrite(x);
            root = x;
            t = tInput;
        }

        public void splitChild(bNode parent, int i)
        {                                                               
            bNode zed = new bNode();                                    
            zed.keys.Add(null);                                         
            bNode placeHolder = parent.children[i];

            zed.leaf = placeHolder.leaf;
            zed.n = t - 1;
            for (int j = 1; j <= (t - 1); j++)
                zed.keys.Insert(j, placeHolder.keys[j + t]);

            if (!placeHolder.leaf)
            {
                if (zed.children.Count == 0)
                    zed.children.Add(null);
                for (int j = 1; j <= t; j++)
                    zed.children.Add(placeHolder.children[j + t]);
            }
            placeHolder.n = t - 1;

            parent.children.Insert(i + 1, zed);
            parent.keys.Insert(i, placeHolder.keys[t]);

            parent.n += 1;
            //diskWrite(y);
            //diskWrite(z);
            //diskWrite(x);

            for (int j = 0; j <= placeHolder.keys.Count - 1; j++)
                placeHolder.keys.RemoveAt(placeHolder.keys.Count - 1);
            for (int j = 0; j < zed.children.Count - 1; j++)
                placeHolder.children.RemoveAt(placeHolder.children.Count - 1);
        }

        public void insertNonFull(bNode node, char keyInsert)
        {
            int i = node.n;
            if (node.leaf)
            {
                while (i >= 1 && keyInsert < node.keys[i])
                    i -= 1;
                node.keys.Insert(i + 1, keyInsert);
                node.n = node.keys.Count - 1;
                //diskWrite(node);
            }
            else
            {
                while (i >= 1 && keyInsert < node.keys[i])
                    i -= 1;
                i += 1;
                //diskRead(node.children[i]);
                if (node.children[i].n == ((2 * t) - 1))
                {
                    splitChild(node, i);
                    if (keyInsert > node.keys[i])
                        i += 1;
                }
                insertNonFull(node.children[i], keyInsert);
            }
        }

        public void insert(char keyInsert)
        {
            bNode rootNode = root;

            if (rootNode.n == ((2 * t) - 1))
            {
                bNode sub = new bNode();
                root = sub;
                sub.children.Add(null);
                sub.keys.Add(null);
                sub.leaf = false;
                sub.n = 0;
                sub.children.Insert(1, rootNode);
                splitChild(sub, 1);
                insertNonFull(sub, keyInsert);
            }
            else
                insertNonFull(rootNode, keyInsert);
        }

        public void printTree(bNode rootNode, int indent)
        {
            if (rootNode != null)
            {
                for (int i = 0; i < indent; i++)
                    Console.Write("    ");
                Console.Write("-|");
                for (int i = 1; i <= rootNode.keys.Count - 1; i++)
                    Console.Write(rootNode.keys[i].Value + " ");
                Console.WriteLine("");
                if (rootNode.children != null)
                {
                    for (int i = 1; i <= rootNode.children.Count - 1; i++)
                        printTree(rootNode.children[i], indent + 1);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bTree testTree = new bTree();
            testTree.create(3);

            testTree.root.n = 4;
            testTree.root.keys.Add(null);
            testTree.root.keys.Add('G');
            testTree.root.keys.Add('M');
            testTree.root.keys.Add('P');
            testTree.root.keys.Add('X');

            char?[] one = { null, 'A', 'C', 'D', 'E' };
            char?[] two = { null, 'J', 'K' };
            char?[] three = { null, 'N', 'O' };
            char?[] four = { null, 'R', 'S', 'T', 'U', 'V' };
            char?[] five = { null, 'Y', 'Z' };

            bNode testNodeOne = new bNode(4, new List<char?>(one), true, null);
            bNode testNodeTwo = new bNode(2, new List<char?>(two), true, null);
            bNode testNodeThree = new bNode(2, new List<char?>(three), true, null);
            bNode testNodeFour = new bNode(5, new List<char?>(four), true, null);
            bNode testNodeFive = new bNode(2, new List<char?>(five), true, null);

            testTree.root.children.Add(null);
            testTree.root.children.Add(testNodeOne);
            testTree.root.children.Add(testNodeTwo);
            testTree.root.children.Add(testNodeThree);
            testTree.root.children.Add(testNodeFour);
            testTree.root.children.Add(testNodeFive);

            testTree.root.leaf = false;

            testTree.printTree(testTree.root, 0);
            Console.WriteLine("");

            testTree.insert('B');
            testTree.printTree(testTree.root, 0);
            Console.WriteLine("");

            testTree.insert('Q');
            testTree.printTree(testTree.root, 0);
            Console.WriteLine("");

            testTree.insert('L');
            testTree.printTree(testTree.root, 0);
            Console.WriteLine("");

            testTree.insert('F');
            testTree.printTree(testTree.root, 0);
            Console.WriteLine("");
        }
    }
}