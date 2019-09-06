using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Method1();
            //Method2();
            Method3();
            //Method4();
            //Method5();
            //Method6();
            //Method7();
            //Method8();
            //Method9();
            Console.ReadLine();

        }

        #region task开启三种方式：
        //方法一：通过new开启
        private static void Method1()
        {
            Task task =new Task(() =>
            {
                Console.WriteLine("子线程1开启，线程id:"+Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(300);
                Console.WriteLine("完成");
            });
            task.Start();

        }
        //方法二：Task.Run
        private static void Method2()
        {
            Task task = Task.Run(() =>
            {
                Console.WriteLine("子线程2开启，线程id:" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(300);
                Console.WriteLine("完成");
            });

        }
        //方法三：Factory开启
        private static void Method3()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("子线程3开启，线程id:" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(300);
                Console.WriteLine("完成");
            });

        }
        #endregion

        #region MyRegion
        private static void Method4()
        {


        }
        private static void Method5()
        {


        }
        #endregion
        private static void Method6()
        {


        }
        private static void Method7()
        {


        }
        private static void Method8()
        {


        }
        private static void Method9()
        {


        }


    }
}
