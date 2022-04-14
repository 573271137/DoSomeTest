using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//请从上到下依次打印出一颗二叉树的最左侧节点。
//例：
//              2
//           /     \
//         11        23
//       /   \      /    \
//     10    15    7     14
//                 \
//                 12
//                  /
//                13
//输出 [2,11,10,12,13]

public class Solution : MonoBehaviour
{


    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public static class TreeNodeSolution
    {
        public static List<int> FindBottomLeftValue(TreeNode root)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();

            List<int> leftNodePerFloorList = new List<int>();

            int index = 0;
            queue.Enqueue(root);
            leftNodePerFloorList.Add(root.val);

            while (queue.Count > 0)
            {
                int size = queue.Count;
                while (size > 0)
                {
                    TreeNode td = queue.Dequeue();

                    if (td.left != null)
                        queue.Enqueue(td.left);
                    if (td.right != null)
                        queue.Enqueue(td.right);

                    size--;
                }
                if (queue.Count > 0)
                    leftNodePerFloorList.Add(queue.Peek().val);
                index++;
            }

            return leftNodePerFloorList;
        }
    }
}
