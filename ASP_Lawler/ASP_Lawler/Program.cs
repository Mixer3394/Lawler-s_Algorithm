using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASP_Lawler
{
    class Program
    {
        public static List<int> GlobalList = new List<int>();
        public static List<double> fMaxList = new List<double>();
        public static List<double> valueT = new List<double>();
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Lawler's Algorithm\n----------------------------------");

            if (ReadFile.InputFileReader())
            {
                while (ReadFile.pList.Count > 0)
                {
                    Algorithm.runAlgorithm();
                }

                Console.WriteLine("Wynik działania Algorytmu Lawlera\n");

                SaveOutput.saveResult();

            }
            else
            {
                Console.WriteLine("Dane wejściowe są niepoprawne");
            }
       
            
            Console.ReadKey();
        }
    }
}
