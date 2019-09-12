using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    /// <summary>
    /// link学习
    /// </summary>
    public static class LinkLearn
    {
        static int[] nums = new int[] { 1,5,11, 42, 23, 340, 25,6,800 };
        static List<int> list = new List<int>();

        public static void Test01()
        {
            foreach (var item in nums)
            {
                if (item%2!=0)
                {
                    list.Add(item);
                }
            }
            list.Sort();
            list.Reverse();
            foreach (var a in list)
            {
                Console.WriteLine(a);
            }
        }

        /// <summary>
        /// select扩展方法
        /// </summary>
        public static void Test()
        {
            //定义查询
            var lst = from a in nums
                      where a % 2 != 0
                      orderby a descending
                      select a;
            //执行查询lst
            foreach (var a in lst)
            {
                Console.WriteLine(a);
            }
        }

        /// <summary>
        /// select扩展方法
        /// </summary>
        public static void Test02()
        {
            var lst = from a in nums
                      where a % 2 != 0
                      select a;
            Console.WriteLine(lst);
            //var lst = nums.Select(o => o > 200);
            //var lst1 = nums.Select(o => o);
            foreach (var a in lst)
            {
                Console.WriteLine(a);
            }
        }


        /// <summary>
        /// where扩展方法
        /// </summary>
        public static void Test03()
        {
            //var lst = from a in nums
            //    where a % 2 != 0
            //    orderby a descending
            //    select a;
            var lst = nums.Where(o => o > 20);
                         
           
            foreach (var a in lst)
            {
                Console.WriteLine(a);
            }
        }

        /// <summary>
        /// orderBy扩展方法
        /// </summary>
        public static void Test04()
        {
            //var lst = from a in nums
            //    where a % 2 != 0
            //    orderby a descending
            //    select a;
            var lst = nums.Where(o => o > 20)
                .Select(q => q / 5)
                .OrderByDescending(e => e);

            foreach (var a in lst)
            {
                Console.WriteLine(a);
            }
        }




        /// <summary>
        /// GroupBy扩展方法
        /// </summary>
        public static void Test05()
        {
            string[] nums1 = new string[] { "gong","lib1","s11b","string", "asdgong", "adhlib", "sdfsb", "ring" };
            var lst = nums1.Where(o => o.Length >= 2)
                .Select(q => q)
                
                .GroupBy(s => s.Substring(0, 1))
                .OrderByDescending(p => p.Key);

            foreach (var a in lst)//这一层循环是循环分组条件
            {
                Console.WriteLine("分组要求："+a.Key);//显示分组名称
                foreach (var b in a)//内层循环：遍历分组内的元素
                {
                    Console.WriteLine(b);
                }
            }
        }
    }
}
