using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PuttyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SSH client = new SSH();
            client.OpenSession("10.10.200.114", "reveraprod", "reveraprod");

            var list = GetList();
            foreach (var item in list)
            {
                StreamWriter sw = new StreamWriter(@"D:\Test\test.txt", true);

                string command = "cd data; grep -l " + item + " *SBR*.DAT";
                string result = client.SendCommand(command);
                IList<string> listResult = result.Split('\n').ToList();

                if (listResult.Count > 0)
                {
                    Console.WriteLine(item);
                    sw.WriteLine(item);
                }

                foreach (var itemResult in listResult)
                {
                    if (!string.IsNullOrEmpty(itemResult))
                    {
                        Console.WriteLine("\t" + itemResult);
                        sw.WriteLine("\t" + itemResult);
                    }
                }

                Console.WriteLine("         ");
                sw.WriteLine("");
                sw.Close();
            }

            client.CloseSession();
        }

        static List<string> GetList()
        {
            List<string> line = new List<string>();
            line.Add("8292171476986");
            line.Add("8292173208322");
            return line;
        }

    }
}
