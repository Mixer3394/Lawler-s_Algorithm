using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Lawler
{
    class Algorithm
    {
        private static List<int> L = new List<int>();
        public static bool alreadyExist(int number, List<int> lList)
        {
            bool alreadyExist = lList.Any(x => x == number);
            return alreadyExist;
        }
        public static bool alreadyExistDouble(double number, List<double> lList)
        {
            bool alreadyExist = lList.Any(x => x == number);
            return alreadyExist;
        }
        private static void haveChildren(IList<Group> lista)
        {
            foreach (var i in lista)
            {
                if (i.Children.Count == 0)
                {
                    if (alreadyExist(i.ID, L) != true)
                    {
                        L.Add(i.ID);
                    }

                }
                else
                {
                    haveChildren(i.Children);
                }
            }
        }

        public static void runAlgorithm()
        {
            L.Clear();
            
            foreach (var i in Tree.officialTree)
            {
                if(i.Children.Count == 0)
                {
                    if (alreadyExist(i.ID, L) != true)
                    {
                        L.Add(i.ID);
                    }
                }
                else
                {
                    haveChildren(i.Children);
                }
                
            }


            Dictionary<int, string> functionDict = new Dictionary<int, string>();
            foreach (var i in L)
            {
                functionDict.Add(i, ReadFile.fList[i]);
            }

            Dictionary<int, double> algorithmDict = new Dictionary<int, double>();
            foreach (var i in functionDict)
            {
                string result = i.Value.Replace("t", ReadFile.N.ToString());

                string formula = result;
                StringToFormula stf = new StringToFormula();
                double result1 = stf.Eval(formula);

                algorithmDict.Add(i.Key, result1);
            }


            var min = algorithmDict.Values.Min();
            if (!alreadyExistDouble(min, Program.fMaxList))
            {
                Program.fMaxList.Add(min);
            }
            var item = (from i in algorithmDict
                        where i.Value == min
                        select i.Key).FirstOrDefault();




            var itemValue = (from k in ReadFile.pList
                             where k.Key == Convert.ToInt32(item)
                             select k.Value).FirstOrDefault();

            Program.valueT.Add(itemValue);

            ReadFile.N = ReadFile.N - itemValue;


            ReadFile.pList.Remove(item);
            ReadFile.fList.Remove(item);

      

            L.Clear();
            functionDict.Clear();


            GroupEnumerable.RemoveChild(Tree.officialTree, item);


        }

        
    }
}
