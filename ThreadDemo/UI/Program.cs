using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();
            var stulist = manager.QueryStudentses(1);
            foreach (var student in stulist)
            {
                Console.WriteLine(student.StudentId+"\t"+student.StudentName);
            }

            Console.ReadLine();

        }
    }
}
