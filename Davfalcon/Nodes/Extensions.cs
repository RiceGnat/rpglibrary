using System;
using System.Text;

namespace Davfalcon.Nodes
{
    public static class Extensions
    {
        public static string Graph<T>(this INode<T> node)
            => node.ToStringRecursive();

        private static string ToStringRecursive<T>(this INode<T> node, int depth = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(new String('\t', depth) + node);
            foreach (INode<T> n in node)
            {
                sb.Append(n.ToStringRecursive(depth + 1));
            }
            return sb.ToString();
        }
    }
}
