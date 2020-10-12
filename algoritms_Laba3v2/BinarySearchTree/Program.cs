
using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree binaryTree = new BinaryTree();

            int[] numbersToAdd = new[] { 6, 4, 3, 2, 10, 1, 9 };

            foreach (int number in numbersToAdd)
            {
                binaryTree.Add(number);
            }

            Console.WriteLine("Дерево:");
            binaryTree.PrintTree();

            Console.WriteLine("Возрастание:");
            foreach (int number in binaryTree.AscendingSequence())
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine();

            Console.WriteLine("Убывание:");
            foreach (int number in binaryTree.DescendingSequence())
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine();

            int k = 3;
            Console.WriteLine($"k-ый минимальный элемент: {binaryTree.FindKthMinimalElement(k).Value} (k = {k})");

            BinaryTree binaryTree1 = new BinaryTree();
            int[] numbersToAdd1 = new[] { 1, 2, 3, 4, 5, 6, 7 };
            foreach (int number in numbersToAdd1)
            {
                binaryTree1.Add(number);
            }

            BinaryTree binaryTree2 = new BinaryTree();
            int[] numbersToAdd2 = new[] { 9, 8, 14, 11, 15, 20, 21 };
            foreach (int number in numbersToAdd2)
            {
                binaryTree2.Add(number);
            }

            BinaryTree binaryTree3 = new BinaryTree();
            int[] numbersToAdd3 = new[] { 6, 7, 5, 3, 1, 4 };
            foreach (int number in numbersToAdd3)
            {
                binaryTree3.Add(number);
            }

            binaryTree1.BalanceTree();
            binaryTree2.BalanceTree();
            binaryTree3.BalanceTree();

            Console.WriteLine("Сбалансированное дерево 1:");
            binaryTree1.PrintTree();

            Console.WriteLine("Сбалансированное дерево 2:");
            binaryTree2.PrintTree();

            Console.WriteLine("Сбалансированное дерево 3:");
            binaryTree3.PrintTree();
        }
    }
}