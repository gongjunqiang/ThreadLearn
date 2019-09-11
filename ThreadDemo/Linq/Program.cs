using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {

            //Test();

            LinkLearn.Test01();
            Console.WriteLine("--------------");
            LinkLearn.Test02();
            Console.WriteLine("--------------");
            LinkLearn.Test03();
            Console.WriteLine("--------------");
            LinkLearn.Test04();
            Console.WriteLine("--------------");
            LinkLearn.Test05();
            Console.ReadLine();
        }

        static void Test()
        {
            int a = 10;
            Console.WriteLine("10的平方是：" + a.Square());
            Console.WriteLine("asd".StringShow());
            var person = new Person() { PersonId = 10, PersonName = "李大爷" };
            Console.WriteLine(person.Show());
            Console.WriteLine(person.Show(20, "红哇嘎小去"));
          
        }
    }
}
