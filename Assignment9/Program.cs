/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 9
 Description: This program utilizes and tests the functionality of topological sorting.
                The program populates a directed graph by creating its nodes and manually 
                assigning its adjacencies. It then utilizes a depth-first-search
                algorithm to generate a topological sort of the graph to output. 
---------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Assignment9
{
    public class graphNode
    {
        public List<graphNode> adjacency = new List<graphNode>();

        public char key;

        public bool visited = false;

        public graphNode(char key) { this.key = key; }
    }

    public class graph
    {
        public List<graphNode> asortedNodes = new List<graphNode>();

        public List<graphNode> sortedNodes = new List<graphNode>();

        public void depthFirstSearch(int j, List<graphNode> visitedNodes)
        {
            asortedNodes[j].visited = true;

            foreach (graphNode node in asortedNodes[j].adjacency)
            {
                if (node.visited == false)
                    depthFirstSearch(asortedNodes.IndexOf(node), visitedNodes);
            }
            visitedNodes.Add(asortedNodes[j]);
        }

        public void topSort()
        {
            int n = asortedNodes.Count;

            for (int j = 0; j < n; j++)
            {
                if (asortedNodes[j].visited == false)
                {
                    List<graphNode> visitedNodes = new List<graphNode>();
                    depthFirstSearch(j, visitedNodes);
                    foreach (graphNode node in visitedNodes)
                        sortedNodes.Insert(0, node);
                }
            }
        }

        public void printSortedNodes()
        {
            Console.WriteLine("~Topological Sort~");
            foreach (graphNode node in sortedNodes)
                Console.Write(node.key + " ");
            Console.WriteLine("");
        }

        public graph(graphNode[] asortedNodes) { this.asortedNodes.AddRange(asortedNodes); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            graphNode A = new graphNode('A');
            graphNode B = new graphNode('B');
            graphNode C = new graphNode('C');
            graphNode s = new graphNode('s');
            graphNode D = new graphNode('D');
            graphNode E = new graphNode('E');
            graphNode F = new graphNode('F');
            graphNode t = new graphNode('t');
            graphNode G = new graphNode('G');
            graphNode H = new graphNode('H');
            graphNode I = new graphNode('I');

            A.adjacency.AddRange(new graphNode[] { B, E });
            B.adjacency.Add(C);
            C.adjacency.Add(t);
            s.adjacency.AddRange(new graphNode[] { A, D, G });
            D.adjacency.AddRange(new graphNode[] { A, E });
            E.adjacency.AddRange(new graphNode[] { C, F, I });
            F.adjacency.AddRange(new graphNode[] { C, t });
            G.adjacency.AddRange(new graphNode[] { D, E, H });
            H.adjacency.AddRange(new graphNode[] { E, I });
            I.adjacency.AddRange(new graphNode[] { F, t });

            graph testGraph = new graph(new graphNode[] { A, B, C, s, D, E, F, t, G, H, I });
            testGraph.topSort();
            testGraph.printSortedNodes();
        }
    }
}
