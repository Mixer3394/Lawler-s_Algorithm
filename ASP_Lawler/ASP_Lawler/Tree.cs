using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASP_Lawler
{
    public class Tree
    {
        public static IList<Group> officialTree;
        public static List<Group> flatList = new List<Group>();
        public static void CreateTree(List<string> treeList)
        {
            string pattern = @"(T)(\d*)\|(T)(\d*)";
            string pattern2 = @"(T)(\d*)";
            for (int i = 0; i < treeList.Count; i++)
            {
                Match match = Regex.Match(treeList[i], pattern, RegexOptions.IgnoreCase);
                Match match2 = Regex.Match(treeList[i], pattern2, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    int id = Int32.Parse(match.Groups[4].ToString());
                    int parentId = Int32.Parse(match.Groups[2].ToString());
                    flatList.Add(new Group() { ID = id, ParentID = parentId });
                }
                else
                {
                    if (match2.Success)
                    {
                        int id = Int32.Parse(match2.Groups[2].ToString());
                        flatList.Add(new Group() { ID = id, ParentID = null });
                    }
                }
                

            }
            var lista = (from i in flatList
                        select i.ParentID).ToList();
            List<int> root = new List<int>();

        
            foreach (var i in lista)
            {
                if(i != null)
                {
                    bool alreadyExist = flatList.Any(x => x.ID == i.Value);
                    if (alreadyExist != true)
                        root.Add(i.Value);
                }
                
            }

            foreach (var i in root)
            {
                if(i != null)
                {
                    bool alreadyExist = flatList.Any(x => x.ID == i);
                    if (!alreadyExist)
                        flatList.Add(new Group() { ID = i, ParentID = null });
                }
                
            }


            var tree = flatList.BuildTree();
            officialTree = tree;
            Console.WriteLine("Zbudowałem drzewko\n-----------------------------------\n");

        }


    }
}
