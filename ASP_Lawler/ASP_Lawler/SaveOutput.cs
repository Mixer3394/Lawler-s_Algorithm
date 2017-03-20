using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASP_Lawler
{
    class SaveOutput
    {
        public static void saveResult()
        {
            try
            {
                string path = Application.StartupPath + "\\Output\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"; ;

                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
                SaveFile.WriteLine("Lawler's Algorithm Result");
                SaveFile.WriteLine("----------------------------------");
                SaveFile.WriteLine();
                SaveFile.WriteLine("Cmax jest równy " + ReadFile.cMax);
                SaveFile.WriteLine("Kolejność: ");

                string text = "Cmax jest równy " + ReadFile.cMax + "\nKolejność: ";

                for (int i = Program.GlobalList.Count; i > 0; i--)
                {
                    SaveFile.Write("T" + Program.GlobalList[i - 1] + ", ");
                    text = text + "T" + Program.GlobalList[i - 1] + ", ";
                }
                SaveFile.WriteLine();
                SaveFile.WriteLine("Koszt kolejno: ");
                text = text + "\nKoszt kolejno: ";
                for (int i = Program.valueT.Count; i > 0; i--)
                {
                    SaveFile.Write(Program.valueT[i - 1] + "| ");
                    text = text + Program.valueT[i - 1] + "| ";
                }
                SaveFile.WriteLine();
                SaveFile.WriteLine("fMax = " + Program.fMaxList.Max());
                text = text + "\nfMax = " + Program.fMaxList.Max();

                Console.WriteLine(text);
                SaveFile.Close();
                Console.WriteLine("Wynik został pomyślenie zapisany do pliku, który znajduje się: \n" + path);
            }
            
            catch(Exception ex)
            {
                Console.WriteLine("Błąd: " + ex);
            }
        }
    }
}
