using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Lawler
{
    public static class GroupEnumerable
    {
        public static IList<Group> BuildTree(this IEnumerable<Group> source)
        {
            var groups = source.GroupBy(i => i.ParentID);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }

        private static void AddChildren(Group node, IDictionary<int, List<Group>> source)
        {
            if (source.ContainsKey(node.ID))
            {
                node.Children = source[node.ID];
                for (int i = 0; i < node.Children.Count; i++)
                {
                    AddChildren(node.Children[i], source);
                }

            }
            else
            {
                node.Children = new List<Group>();
            }
        }
        public static void RemoveChild(IList<Group> node, int childID)
        {
            foreach(var i in node)
            {
                if(i.ID == childID)
                {
                    node.Remove(i);
                    
                    if (!Algorithm.alreadyExist(i.ID, Program.GlobalList))
                    {
                        Program.GlobalList.Add(i.ID);
                    }
                    
                    break;
                }
                else
                {
                    if (i.Children.Count > 0)
                    {
                        RemoveChild(i.Children, childID);
                    }
                }
            }
        }
    }
}
