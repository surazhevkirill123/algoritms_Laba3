using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    public class BinaryTree
    {
        private BinaryTreeNode _head;
        public void Add(int value)
        {
            if (_head == null)
            {
                _head = new BinaryTreeNode(value);
            }
            else
            {
                AddTo(_head, new BinaryTreeNode(value));
            }
        }

        private static void AddTo(BinaryTreeNode parentNode, BinaryTreeNode insertNode)
        {
            if (insertNode.Value < parentNode.Value)
            {
                if (parentNode.LeftNode == null)
                {
                    parentNode.LeftNode = insertNode;
                    parentNode.LeftSubtreeSize++;
                    insertNode.ParentNode = parentNode;
                }
                else
                {
                    parentNode.LeftSubtreeSize++;
                    AddTo(parentNode.LeftNode, insertNode);
                }
            }
            else if (insertNode.Value > parentNode.Value)
            {
                if (parentNode.RightNode == null)
                {
                    parentNode.RightNode = insertNode;
                    parentNode.RightSubtreeSize++;
                    insertNode.ParentNode = parentNode;
                }
                else
                {
                    parentNode.RightSubtreeSize++;
                    AddTo(parentNode.RightNode, insertNode);
                }
            }
        }

        public bool Contains(int value) => FindByValue(value) != null;

        private BinaryTreeNode FindByValue(int value, BinaryTreeNode headOfSubtree = null)
        {
            BinaryTreeNode currentNode = headOfSubtree ?? _head;
            
            while (currentNode != null)
            {
                if (currentNode.Value > value)
                {
                    currentNode = currentNode.LeftNode;
                }
                else if (currentNode.Value < value)
                {
                    currentNode = currentNode.RightNode;
                }
                else
                    break;
            }

            return currentNode;
        }
        
        public void PrintTree()
        {
            TreePrinter.PrintNode(_head);
        }

        public List<int> AscendingSequence()
        {
            List<int> ascendingList = new List<int>();
            InAscendingOrderWalk(_head, ascendingList);
            return ascendingList;
        }

        public List<int> DescendingSequence()
        {
            List<int> descendingList = new List<int>();
            InDescendingOrderWalk(_head, descendingList);
            return descendingList;
        }

        private void InAscendingOrderWalk(BinaryTreeNode node, List<int> ascendingList)
        {
            if (node != null)
            {
                InAscendingOrderWalk(node.LeftNode, ascendingList);
                ascendingList.Add(node.Value);
                InAscendingOrderWalk(node.RightNode, ascendingList);
            }
        }

        private void InDescendingOrderWalk(BinaryTreeNode node, List<int> descendingList)
        {
            if (node != null)
            {
                InDescendingOrderWalk(node.RightNode, descendingList);
                descendingList.Add(node.Value);
                InDescendingOrderWalk(node.LeftNode, descendingList);
            }
        }

        public BinaryTreeNode FindKthMinimalElement(int k, BinaryTreeNode headOfSubtree = null)
        {
            headOfSubtree ??= _head;

            while (headOfSubtree.LeftSubtreeSize + 1 != k)
            {
                if (headOfSubtree.LeftSubtreeSize + 1 < k)
                {
                    k -= headOfSubtree.LeftSubtreeSize + 1;
                    headOfSubtree = headOfSubtree.RightNode;
                }
                else
                {
                    headOfSubtree = headOfSubtree.LeftNode;
                }
            }

            return headOfSubtree;
        }
        public void BalanceTree()
        {
            GetBalancedSubTree(_head);
        }

        private void GetBalancedSubTree(BinaryTreeNode headOfSubtree)
        {
            if (headOfSubtree.LeftSubtreeSize + headOfSubtree.RightSubtreeSize <= 1)
            {
                return;
            }
            
            int middleElement = (headOfSubtree.LeftSubtreeSize + headOfSubtree.RightSubtreeSize + 1) / 2;
            
            BinaryTreeNode nodeToHead = FindKthMinimalElement(middleElement + 1, headOfSubtree);

            while (nodeToHead.Value != headOfSubtree.Value && nodeToHead.ParentNode != null)
            {
                BinaryTreeNode parentNode = nodeToHead.ParentNode;
                if (parentNode.LeftNode != null && parentNode.LeftNode.Value == nodeToHead.Value)
                {
                    RotateRight(ref parentNode);
                }
                else if (parentNode.RightNode != null && parentNode.RightNode.Value == nodeToHead.Value)
                {
                    RotateLeft(ref parentNode);
                }
                nodeToHead = parentNode;
            }
            
            GetBalancedSubTree(nodeToHead.LeftNode);
            GetBalancedSubTree(nodeToHead.RightNode);
        }

        private void RotateRight(ref BinaryTreeNode parentNode)
        {
            int z = parentNode.Value;
            parentNode.Value = parentNode.LeftNode.Value;
            parentNode.LeftNode.Value = z;
            
            (BinaryTreeNode parentLeft, BinaryTreeNode parentRight) = (parentNode.LeftNode, parentNode.RightNode);
            (BinaryTreeNode nodeLeft, BinaryTreeNode nodeRight) = (parentNode.LeftNode.LeftNode, parentNode.LeftNode.RightNode);
            
            parentNode.RightNode = parentLeft;
            parentNode.LeftNode = nodeLeft;

            parentNode.RightNode.RightNode = parentRight;
            parentNode.RightNode.LeftNode = nodeRight;
            
            SetParentNodes(ref parentNode);

            parentNode.LeftNode?.CountSubtreeSizes();
            parentNode.RightNode?.CountSubtreeSizes();
            parentNode.CountSubtreeSizes();
        }

        private void RotateLeft(ref BinaryTreeNode parentNode)
        {
            int z = parentNode.Value;
            parentNode.Value = parentNode.RightNode.Value;
            parentNode.RightNode.Value = z;
            
            (BinaryTreeNode parentLeft, BinaryTreeNode parentRight) = (parentNode.LeftNode, parentNode.RightNode);
            (BinaryTreeNode nodeLeft, BinaryTreeNode nodeRight) = (parentNode.RightNode.LeftNode, parentNode.RightNode.RightNode);

            parentNode.RightNode = nodeRight;
            parentNode.LeftNode = parentRight;

            parentNode.LeftNode.LeftNode = parentLeft;
            parentNode.LeftNode.RightNode = nodeLeft;
            
            SetParentNodes(ref parentNode);
            
            parentNode.LeftNode?.CountSubtreeSizes();
            parentNode.RightNode?.CountSubtreeSizes();
            parentNode.CountSubtreeSizes();
        }

        private void SetParentNodes(ref BinaryTreeNode parentNode)
        {
            if (parentNode.LeftNode != null)
            {
                parentNode.LeftNode.ParentNode = parentNode;
                if (parentNode.LeftNode.LeftNode != null)
                {
                    parentNode.LeftNode.LeftNode.ParentNode = parentNode.LeftNode;
                }

                if (parentNode.LeftNode.RightNode != null)
                {
                    parentNode.LeftNode.RightNode.ParentNode = parentNode.LeftNode;
                }
            }

            if (parentNode.RightNode != null)
            {
                parentNode.RightNode.ParentNode =  parentNode;
                if (parentNode.RightNode.LeftNode != null)
                {
                    parentNode.RightNode.LeftNode.ParentNode = parentNode.RightNode;
                }

                if (parentNode.RightNode.RightNode != null)
                {
                    parentNode.RightNode.RightNode.ParentNode = parentNode.RightNode;
                }
            }
        }
    }
}