using System;

namespace BinarySearchTree
{
    public class BinaryTreeNode
    {
        public BinaryTreeNode(int value)
        {
            Value = value;
            LeftNode = null;
            RightNode = null;
            ParentNode = null;
            LeftSubtreeSize = 0;
            RightSubtreeSize = 0;
        }
        
        public BinaryTreeNode LeftNode { get; set; }
        public BinaryTreeNode RightNode { get; set; }
        
        public int LeftSubtreeSize { get; set; }
        
        public int RightSubtreeSize { get; set; }
        public BinaryTreeNode ParentNode { get; set; }
        public int Value { get; set; }

        public void CountSubtreeSizes()
        {
            LeftSubtreeSize = LeftNode == null ? 0 : LeftNode.LeftSubtreeSize + LeftNode.RightSubtreeSize + 1;
            RightSubtreeSize = RightNode == null ? 0 : RightNode.LeftSubtreeSize + RightNode.RightSubtreeSize + 1;
        }
    }
}