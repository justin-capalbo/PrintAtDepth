using System;
using System.Collections.Generic;
using System.Linq;

namespace JCapalbo
{
    class Program
    {
        static void Main(string[] args)
        {
            Node n2a = new Node('d');
            Node n2b = new Node('e');
            Node n2c = new Node('f');
            Node n2d = new Node('g');

            Node n1a = new Node('b')
            {
                children = new List<Node> {n2a, n2b}
            };

            Node n1b = new Node('c')
            {
                children = new List<Node> {n2c, n2d}
            };
            Node root = new Node('a')
            {
                children = new List<Node> {n1a, n1b}
            };

            //Outputs 'abcdefg'
            PrintAtDepths(root);
            Console.ReadLine();
        }


        //We see each node exactly twice, and don't recurse.  We visit nodes once to add them to the queue, then we visit them again to print their values and add their children.
        //Each node is going to end up being added to the queue once, so the outer while loop will visit each node in the tree once which scales linearly with the number of nodes (O(N)).
        //For each node, we also iterate over each of its' children and add them to the queue.  For every node in the tree we should visit its' child node once and add it to the queue, which
        //shouldn't change the overall runtime, I think.  I still print and enqueue a number of nodes that scales linearly with the total count of the nodes.  However, the time it takes to perform
        //this overall traversal will scale exponentially with depth, i.e. (# of children in the worst case) ^ (depth of the tree).  Since we have an uneven tree this is hard to pin down.

        //Hope you like it!
        public static void PrintAtDepths(Node root)
        {
            var theQueue = new Queue<Node>();
            
            theQueue.Enqueue(root);

            while (theQueue.Any())
            {
                //Look for nodes to print, we only want to print the items that are in the queue right now (previous depth)
                int countToPrint = theQueue.Count;
                for (int i = 0; i < countToPrint; i++)
                {
                    //Get the next node to print
                    var currentNode = theQueue.Dequeue();
                    Console.Write(currentNode.Value);

                    //Queue all of its' children
                    foreach (var childNode in currentNode.children)
                    {
                        if (childNode != null)
                            theQueue.Enqueue(childNode);
                    }
                }
            }
        }


        public class Node
        {
            public char Value { get; set; }
            public IList<Node> children { get; set; }

            public Node(char c)
            {
                Value = c;
                children = new List<Node>();
            }
        }

    }
}
