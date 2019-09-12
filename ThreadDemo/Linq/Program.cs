using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    delegate bool moreOrlessDelgate(int item);
    class Program
    {
        #region Delgate
        //static void Main(string[] args)
        //{
        //    var arr = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
        //    var d3 = new Predicate<int>(delegate (int item)
        //    {
        //        //可以访问当前上下文中的变量
        //        Console.WriteLine(arr.Count);

        //        if (item > 3)

        //        {
        //            return true;
        //        }
        //        return false;
        //    });

        //    var d1 = new moreOrlessDelgate(More);
        //    Print(arr, d1);
        //    Console.WriteLine("OK");

        //    var d2 = new moreOrlessDelgate(Less);
        //    Print(arr, d2);
        //    Console.WriteLine("OK");
        //    Console.ReadKey();

        //}
        //static void Print(List<int> arr, moreOrlessDelgate dl)
        //{
        //    foreach (var item in arr)
        //    {
        //        if (dl(item))
        //        {
        //            Console.WriteLine(item);
        //        }
        //    }
        //}
        //static bool More(int item)
        //{
        //    if (item > 3)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //static bool Less(int item)
        //{
        //    if (item < 3)
        //    {
        //        return true;
        //    }
        //    return false;
        //}


        #endregion


        static void Main(string[] args)
        {
            var obj = new { Guid.Empty, myTitle = "匿名类型", myOtherParam = new int[] { 1, 2, 3, 4 } };
            var a = obj.GetType();
            Console.WriteLine(obj.Empty);//另一个对象的属性名字，被原封不动的拷贝到匿名对象中来了。
            Console.WriteLine(obj.myTitle);
            Console.WriteLine(obj.myOtherParam);
            Console.ReadKey();

            //Test();

            LinkLearn.Test01();
            Console.WriteLine("--------------");

            LinkLearn.Test02();
            //Console.WriteLine("--------------");
            //LinkLearn.Test03();
            //Console.WriteLine("--------------");
            //LinkLearn.Test04();
            //Console.WriteLine("--------------");
            //LinkLearn.Test05();
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
