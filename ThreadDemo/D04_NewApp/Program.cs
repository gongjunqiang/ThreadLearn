using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D04_NewApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 线程底层观察
            //Console.WriteLine("线程底层观察");
            //Thread thread = new Thread(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine(i);
            //        Thread.Sleep(300);
            //    }
            //});
            //thread.IsBackground = true; 
            //thread.Start();
            #endregion


            #region Join
            //Thread thread = new Thread(new ThreadStart(() =>
            //{

            //    Console.WriteLine("正在执行子线程数据");
            //    Thread.Sleep(5000);
            //}));
            //thread.Start();
            ////thread.Join();//会等待子线程执行完毕后，再执行后面的主线程
            //Console.WriteLine("子线程结束，这是主线程数据");
            #endregion

            #region 线程池

            //Method1();
            //Method2();
            //Method3();
            Method4();
            #endregion

            Console.ReadLine();

        }

        #region 线程池基本使用

        

       
        static void Method1()
        {
            ThreadPool.QueueUserWorkItem((arg) =>
            {
                //实际处理内容
                Console.WriteLine("子线程Id:" + Thread.CurrentThread.ManagedThreadId);

            });

            Console.WriteLine("主线程Id:" + Thread.CurrentThread.ManagedThreadId);
        }


        static void Method2()
        {
            ThreadPool.QueueUserWorkItem((arg) =>
            {
                //实际处理内容
                Console.WriteLine("子线程Id:" + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("arg="+ arg);
            },"参数");

            Console.WriteLine("主线程Id:" + Thread.CurrentThread.ManagedThreadId);
        }
        #endregion

        static void Method3()
        {
            for (int i = 1; i < 10; i++)
            {
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine($"子线程{i}Id:" + Thread.CurrentThread.ManagedThreadId);
                    for (int j = 1; j < 5; j++)
                    {
                        Console.WriteLine(j);
                    }
                });
                thread.Name = "线程名称：" + i;
                thread.IsBackground = true;
                thread.Start(); Thread.Sleep(300);
            }
        }

        static void Method4()
        {
            Console.WriteLine("-------------使用线程池---------");

            for (int i = 1; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem((arg) =>
                {
                    Console.WriteLine($"子线程{i}Id:" + Thread.CurrentThread.ManagedThreadId);
                    for (int j = 1; j < 5; j++)
                    {
                        Console.WriteLine(j);
                    }

                });
            }
        }
    }
}
