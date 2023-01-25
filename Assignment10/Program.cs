/*---------------------------------------------------------------------------------------
 Author:      Seth J. Gibson
 Course:      CIS 263-01
 Program:     Assignment 10
 Description: This program utilizes and tests the functionality of the Ford-Fulkerson
                algorithm. This program takes in a graph of nodes, connected via 
                adjacencies with respective weights, and determines the maximum flow from
                the start node to the target node using the Ford-Fulkerson algorithm.
---------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment10
{
    public class graphNode
    {
        public List<graphNode> adjacency = new List<graphNode>();
        public List<int> weight = new List<int>();

        public char key;

        public bool visited = false;

        public graphNode(char key) { this.key = key; }
    }

    public class graph
    {
        public List<graphNode> asortedNodes = new List<graphNode>();

        public List<graphNode> sortedNodes = new List<graphNode>();

        private void resetVisited()
        {
            foreach (graphNode node in asortedNodes)
                node.visited = false;
        }

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
            {
                if (node != null)
                    Console.Write(node.key + " ");
            }
                
            Console.WriteLine("");
        }

        private graphNode[] solve(graphNode start)
        {
            Queue<graphNode> exitQueue = new Queue<graphNode>();
            exitQueue.Enqueue(start);

            start.visited = true;

            graphNode[] previous = new graphNode[asortedNodes.Count];
            while (exitQueue.Count != 0)
            {
                graphNode element = exitQueue.Dequeue();
                foreach(graphNode node in element.adjacency)
                {
                    if (node != null)
                    {
                        if (!node.visited)
                        {
                            exitQueue.Enqueue(node);
                            node.visited = true;
                            previous[asortedNodes.IndexOf(node) - 1] = element;
                        }
                    }
                }
            }

            return previous;
        }

        private List<graphNode> pathReconstruction(graphNode start, graphNode end, graphNode[] previous)
        {
            previous[previous.Length - 1] = end;
            List<graphNode> path = new List<graphNode>();

            foreach(graphNode node in previous)
            {
                if (!path.Contains(node))
                {
                    path.Add(node);
                }
            }

            if (path[0] == start)
                return path;
            return null;
        }

        public List<graphNode> breadthFirstSearch(graphNode start, graphNode end)
        {
            resetVisited();
            graphNode[] previous = solve(start);
            return pathReconstruction(start, end, previous);
        }

        public graph graphCopy(bool flow)
        {
            graphNode s = new graphNode('s');
            graphNode A = new graphNode('A');
            graphNode B = new graphNode('B');
            graphNode C = new graphNode('C');
            graphNode D = new graphNode('D');
            graphNode t = new graphNode('t');

            s.adjacency.AddRange(new graphNode[] { A, B });
            A.adjacency.AddRange(new graphNode[] { B, C, D });
            B.adjacency.Add(D);
            C.adjacency.Add(t);
            D.adjacency.Add(t);

            if (flow)
            {
                s.weight.AddRange(new int[] { 0, 0 });
                A.weight.AddRange(new int[] { 0, 0, 0 });
                B.weight.Add(0);
                C.weight.Add(0);
                D.weight.Add(0);
            }
            else
            {
                s.weight.AddRange(new int[] { 4, 2 });
                A.weight.AddRange(new int[] { 1, 2, 4 });
                B.weight.Add(2);
                C.weight.Add(3);
                D.weight.Add(3);
            }

            return new graph(new graphNode[] { s, A, B, C, D, t });
        }

        public void setWeight(char firstKey, char secondKey, int weight)
        {
            foreach(graphNode firstNode in sortedNodes)
            {
                if (firstNode.key == firstKey)
                {
                    for (int i = 0; i < firstNode.adjacency.Count; i++)
                    {
                        if (firstNode.adjacency[i].key == secondKey)
                        {
                            firstNode.weight[i] = weight;
                            return;
                        }
                    }
                }
            }
        }

        public int getWeight(char firstKey, char secondKey)
        {
            foreach (graphNode firstNode in sortedNodes)
            {
                if (firstNode.key == firstKey)
                {
                    for (int i = 0; i < firstNode.adjacency.Count; i++)
                    {
                        if (firstNode.adjacency[i].key == secondKey)
                        {
                            return firstNode.weight[i];
                        }
                    }
                }
            }
            return -1;
        }

        public int getMaxIntake(char targetKey)
        {
            int result = 0;
            foreach (graphNode node in asortedNodes)
            {
                for (int i = 0; i < node.adjacency.Count; i++)
                {
                    if (node.adjacency[i].key == targetKey)
                        result += node.weight[i];
                }
            }

            return result;
        }

        public int getMaxFlow(graphNode start, graphNode end)
        {
            int maxIntake = getMaxIntake(end.key);

            List<graphNode> shortestPath = breadthFirstSearch(start, end);
            int minFlow = int.MaxValue;
            graphNode lastNode = start;

            foreach (graphNode node in shortestPath)        // Get minimum path value of flow
            {
                if (node != start)
                {
                    int index = lastNode.adjacency.IndexOf(node);
                    if (lastNode.weight[index] < minFlow)
                        minFlow = lastNode.weight[index];
                    lastNode = node;
                }
            }

            graph flow = graphCopy(true);                   // Create first flow iteration
            flow.topSort();
            for (int i = 0; i < shortestPath.Count - 1; i++)
                flow.setWeight(shortestPath[i].key, shortestPath[i + 1].key, minFlow);

            int flowIntake = flow.getMaxIntake(end.key);

            do
            {
                graph residual = graphCopy(false);              // Create residual
                residual.topSort();

                foreach (graphNode node in flow.sortedNodes)
                {
                    for (int i = 0; i < node.weight.Count; i++)
                    {
                        if (node.weight[i] != 0)
                        {
                            residual.setWeight(node.key, node.adjacency[i].key, (residual.getWeight(node.key, node.adjacency[i].key) - node.weight[i]));
                        }
                    }
                }
                foreach (graphNode resNode in residual.sortedNodes)
                {
                    for (int i = 0; i < resNode.weight.Count; i++)
                    {
                        if (resNode.weight[i] <= 0)
                        {
                            resNode.adjacency[i] = null;
                        }
                    }
                }

                shortestPath = residual.breadthFirstSearch(residual.sortedNodes[0], residual.sortedNodes[5]);

                if (shortestPath == null)   // if there is no path, that means all paths from s to t are closed off
                {
                    return flow.getMaxIntake(end.key);
                }
                for (int i = 0; i < shortestPath.Count; i++)
                {
                    if (shortestPath[i] == null)
                        shortestPath.RemoveAt(i);
                }

                minFlow = int.MaxValue;
                lastNode = start;

                for (int i = 1; i < shortestPath.Count; i++)
                {
                    int currentFlow = flow.getWeight(shortestPath[i - 1].key, shortestPath[i].key);
                    if (currentFlow < minFlow && currentFlow != 0)
                        minFlow = flow.getWeight(shortestPath[i - 1].key, shortestPath[i].key);
                }

                for (int i = 0; i < shortestPath.Count - 1; i++)
                    flow.setWeight(shortestPath[i].key, shortestPath[i + 1].key, (flow.getWeight(shortestPath[i].key, shortestPath[i + 1].key) + minFlow));
                flowIntake = flow.getMaxIntake(end.key);

            } while (true);    
        }

        public graph(graphNode[] asortedNodes) { this.asortedNodes.AddRange(asortedNodes); }
        public graph(List<graphNode> sortedNodes) { this.sortedNodes = sortedNodes; }
        public graph(List<graphNode> asortedNodes, List<graphNode> sortedNodes) { this.asortedNodes = asortedNodes; this.sortedNodes = sortedNodes; }

        public graph(graph oldGraph) { oldGraph.asortedNodes = this.asortedNodes; oldGraph.sortedNodes = this.sortedNodes; }
    } 

    class Program
    {
        static void Main(string[] args)
        {
            graphNode s = new graphNode('s');
            graphNode A = new graphNode('A');
            graphNode B = new graphNode('B');
            graphNode C = new graphNode('C');
            graphNode D = new graphNode('D');
            graphNode t = new graphNode('t');

            s.adjacency.AddRange(new graphNode[] { A, B });
            A.adjacency.AddRange(new graphNode[] { B, C, D });
            B.adjacency.Add(D);
            C.adjacency.Add(t);
            D.adjacency.Add(t);

            s.weight.AddRange(new int[] { 4, 2 });
            A.weight.AddRange(new int[] { 1, 2, 4 });
            B.weight.Add(2);
            C.weight.Add(3);
            D.weight.Add(3);

            graph testGraph = new graph(new graphNode[] { s, A, B, C, D, t });
            testGraph.topSort();
            testGraph.printSortedNodes();

            int maxFlow = testGraph.getMaxFlow(s, t);
            Console.WriteLine("Maximum Flow of Graph: " + maxFlow);

            Console.WriteLine();
        }
    }
}
