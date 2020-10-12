using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySearchTree
{
    class TreePrinter
    {
        public static void PrintNode(BinaryTreeNode root)
        {
            int maxLevel = MaxLevel(root);
            List<BinaryTreeNode> nodeList = new List<BinaryTreeNode>
            {
                root
            };
            PrintNodeInternal(nodeList, 1, maxLevel);
        }

        private static void PrintNodeInternal(List<BinaryTreeNode> nodeList, int level, int maxLevel)
        {
            if (nodeList.Count == 0 || IsAllElementsNull(nodeList)) 
            {
                return;
            }

            int floor = maxLevel - level;
            int endgeLines = (int)Math.Pow(2, (Math.Max(floor - 1, 0)));
            int firstSpaces = (int)Math.Pow(2, (floor)) - 1;
            int betweenSpaces = (int)Math.Pow(2, (floor + 1)) - 1;

            PrintWhitespaces(firstSpaces);

            List<BinaryTreeNode> newNodes = new List<BinaryTreeNode>();
            foreach (BinaryTreeNode node in nodeList)
            {
                if (node != null)
                {
                    Console.Write(node.Value);
                    newNodes.Add(node.LeftNode);
                    newNodes.Add(node.RightNode);
                }
                else
                {
                    newNodes.Add(null);
                    newNodes.Add(null);
                    Console.Write(" ");
                }

                PrintWhitespaces(betweenSpaces);
            }
            Console.WriteLine();

            for (int i = 1; i <= endgeLines; i++)
            {
                for (int j = 0; j < nodeList.Count; j++)
                {
                    PrintWhitespaces(firstSpaces - i);
                    if (nodeList[j] == null)
                    {
                        PrintWhitespaces(endgeLines + endgeLines + i + 1);
                        continue;
                    }

                    if (nodeList[j].LeftNode != null)
                    {
                        Console.Write("/");
                    }
                    else 
                    {
                        PrintWhitespaces(1);
                    }

                    PrintWhitespaces(i + i - 1);
                    if (nodeList[j].RightNode != null) 
                    {
                        Console.Write("\\");
                    }
                    else
                    {
                        PrintWhitespaces(1);
                    }

                    PrintWhitespaces(endgeLines + endgeLines - i);
                }

                Console.WriteLine();
            }

            PrintNodeInternal(newNodes, level + 1, maxLevel);
        }

        private static void PrintWhitespaces(int count)
        {
            for (int i = 0; i < count; i++) {
                Console.Write(" ");
            }
        }

        private static int MaxLevel(BinaryTreeNode node)
        {
            if (node == null)
            {
                return 0;
            }

            return Math.Max(MaxLevel(node.LeftNode), MaxLevel(node.RightNode)) + 1;
        }

        private static bool IsAllElementsNull(List<BinaryTreeNode> nodeList)
        {
            foreach(BinaryTreeNode node in nodeList)
            {
                if (node != null)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
