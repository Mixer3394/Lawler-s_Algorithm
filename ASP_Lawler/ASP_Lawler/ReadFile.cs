using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASP_Lawler
{
    
    class ReadFile
    {
        public static Dictionary<int, double> pList = new Dictionary<int, double>();
        public static Dictionary<int, string> fList = new Dictionary<int, string>();
        public static double N = 0;
        public static double cMax = 0;
        [STAThread]
        public static bool InputFileReader()
        {
            Console.WriteLine("Proszę o wskazanie pliku źródłowego\nWciśnij Enter, aby otworzyć okno dialogowe");
            string inputFile = null;
            Console.ReadKey();
            OpenFileDialog fd = new OpenFileDialog();
            string projectPath = Application.StartupPath;
            fd.InitialDirectory = Path.Combine(Application.StartupPath, @"InputFile");
            if (fd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fInfo = new FileInfo(fd.FileName);
                inputFile = fInfo.FullName;
                Console.WriteLine("Wczytałem plik " + fInfo.Name);
            }
            string line;
            int counterLine = 0;
            List<string> readLine = new List<string>();
            StreamReader fileRead = new StreamReader(inputFile);
            while ((line = fileRead.ReadLine()) != null)
            {
                readLine.Add(line);
                counterLine++;
            }

            if (counterLine >= 3)
            {
                int pk = 1;
                for (int i = 0; i < readLine[0].Length; i++)
                {
                    
                    double n;
                    bool isNumeric = double.TryParse(readLine[0][i].ToString(), out n);
                    if (isNumeric)
                    {
                        pList.Add(pk, n);
                        pk++;
                    }
                }

                string function = null;
                int fk = 1;
                for (int i = 0; i < readLine[1].Length; i++) 
                {
                    if(readLine[1][i].ToString() != "[" && readLine[1][i].ToString() != "]" && readLine[1][i].ToString() != ";")
                    {
                        function = function + readLine[1][i].ToString();
                    }
                    else
                    {
                        if(function != null)
                        {
                            {
                                string pattern = @"(t\^)(\d*)";
                                Match match = Regex.Match(function, pattern, RegexOptions.IgnoreCase);
                                fList.Add(fk, function);
                                function = null;
                                fk++;
                            }
                        }
                    }
                }

                if(pList.Count != fList.Count)
                {
                    Console.WriteLine("Dane wejściowe są niepoprawne");
                    return false;
                }
                else
                {
                    N = pList.Values.Sum();
                    List<string> treeList = new List<string>();

                    for (int i = 2; i < readLine.Count; i++)
                    {
                        treeList.Add(readLine[i]);
                    }

                    Tree.CreateTree(treeList);

                    cMax = N;
                    return true;
                }
                
            }
            else
            {
                Console.WriteLine("Niepoprawny plik wejściowy");
                return false;
            }
            
        }
    }
}
