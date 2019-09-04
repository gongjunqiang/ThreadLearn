using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Process;

namespace Processess
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //System.Diagnostics.Process[] pro = System.Diagnostics.Process.GetProcesses();
            //int i = 0;
            //foreach (var process in pro)
            //{
            //    if (process.ProcessName.Contains("chrome"))
            //    {
            //        Console.WriteLine(process.ProcessName);
            //        Console.WriteLine(process.Id);
            //        Console.WriteLine(process.StartInfo);
            //        Console.WriteLine("----------------------------");
            //        i++;
            //    }
            //}

            //Console.WriteLine(i);
            Console.ReadLine();

        }
    }
}
