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
            //Method3();
            //Method4();
            //Method5();
            //Method6();
            //Method7();
            //Method8();
            //Method9();
            //Method10();
            //Method11();
            //Method12();
            Method13();
            Console.ReadLine();

        }

        #region Task使用【1】多线程任务task开启三种方式：
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

        #region Task使用【2】Task的阻塞方式和任务延续
        /// <summary>
        /// //Task的阻塞方式
        /// </summary>
        private static void Method4()
        {
            Task task1 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("task1子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId+"时间："+DateTime.Now);
            });
            task1.Start();

            Task task2 = new Task(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("task2子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);
            });
            task2.Start();

            //方式一：挨个等待和thread的task.join一样
            //task1.Wait();
            //task2.Wait();

            //方式二：等待所有的task运行完成
            //Task.WaitAll(task1, task2);

            //方式三：任意一个线程完成开始主线程
            Task.WaitAny(task1, task2);
            Console.WriteLine("主线程开始运行，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);
        }
        /// <summary>
        /// Task的任务延续:Task.whenAll:希望前面的所有任务执行完毕后，在执行后面的新的任务
        /// 和前面的相比：既有阻塞，又有任务的延续
        /// </summary>
        private static void Method5()
        {
            Task task1 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("task1子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);
            });
            task1.Start();

            Task task2 = new Task(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("task2子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);
            });
            task2.Start();
            
            //线程的延续:特点：主线程不等待，子线程依次执行
            Task.WhenAll(task1, task2).ContinueWith(task3 =>
             {
                 Console.WriteLine("task3子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);

             });
            Console.WriteLine("主线程开始运行，线程id=:" + Task.CurrentId + "时间：" + DateTime.Now);

        }
   
        private static void Method6()
        {
            Task task1 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("task1子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);
            });
            task1.Start();

            Task task2 = new Task(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("task2子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);
            });
            task2.Start();

            //线程的延续:特点：主线程不等待，子线程任何一个执行完毕，执行后面的线程
            Task.WhenAny(task1, task2).ContinueWith(task3 =>
            {
                Console.WriteLine("task3子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now);

            });
            Console.WriteLine("主线程开始运行，线程id=:" + Task.CurrentId + "时间：" + DateTime.Now);


        }

        #endregion

        #region Task使用【3】Task常见枚举：TaskCreationOptions（父子任务运行、长时间运行的任务处理）
        private static void Method7()
        {
            Task parentTaks = new Task(() =>
            {
                Task task1 = new Task(() =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("task1子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now.ToLongTimeString());

                },TaskCreationOptions.AttachedToParent);
                Task task2 = new Task(() =>
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("task2子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now.ToLongTimeString());

                }, TaskCreationOptions.AttachedToParent);
                task1.Start();
                task2.Start();
            });
            parentTaks.Start();

            parentTaks.Wait();//等待附加的子任务全部完成，相当于Task.waitAll(task1,task2);

            Console.WriteLine("主线程开始运行，线程id=:" + Task.CurrentId + "时间：" + DateTime.Now);
        }
        private static void Method8()
        {
            Task task1 = new Task(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("task1子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now.ToLongTimeString());

            },TaskCreationOptions.LongRunning);
            //当此任务为长时间运行时，可以添加子枚举：TaskCreationOptions.LongRunning
            //Thread也可以，但是不要使用ThreadPool ,因为长时间占用不归还，系统会强制开启新线程，会影响一定的性能
            task1.Start();
            task1.Wait();
            Console.WriteLine("主线程开始运行，线程id=:" + Task.CurrentId + "时间：" + DateTime.Now);

        }
        #endregion

        #region Task使用【4】Task的取消功能：使用的是CancellationToken解决多任务的协作取消和超时取消
        /// <summary>
        /// task任务取消和判断
        /// </summary>
        private static void Method9()
        {
            //创建取消信号源对象
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = Task.Factory.StartNew(() =>
            {
                //判断任务是否被取消
                while (!cts.IsCancellationRequested)
                {
                    Thread.Sleep(200);
                    Console.WriteLine("子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now.ToLongTimeString());
                }
            }, cts.Token);
            //模拟事件产生
            Thread.Sleep(2000);
            //加一定的判断逻辑取消任务
            cts.Cancel();
        }

        /// <summary>
        /// task任务取消并做一些任务清理工作
        /// </summary>
        private static void Method10()
        {
            //创建取消信号源对象
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = Task.Factory.StartNew(() =>
            {
                //判断任务是否被取消
                while (!cts.IsCancellationRequested)
                {
                    Thread.Sleep(200);
                    Console.WriteLine("子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now.ToLongTimeString());
                }
            }, cts.Token);
            //注册一个委托，这个委托将在任务取消的时候调用
            cts.Token.Register(() =>
            {
                Console.WriteLine("任务取消，开始清理工作");
                Thread.Sleep(2000);
                Console.WriteLine("任务取消，清理工作结束");
            });
            Thread.Sleep(2000);
            cts.Cancel();
        }
        /// <summary>
        /// Task延时自动取消
        /// </summary>
        private static void Method11()
        {
            //创建取消信号源对象
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = Task.Factory.StartNew(() =>
            {
                //判断任务是否被取消
                while (!cts.IsCancellationRequested)
                {
                    Thread.Sleep(300);
                    Console.WriteLine("子线程开启，线程id=:" + Thread.CurrentThread.ManagedThreadId + "时间：" + DateTime.Now.ToLongTimeString());
                }
            }, cts.Token);
            //注册一个委托，这个委托将在任务取消的时候调用
            cts.Token.Register(() =>
            {
                Console.WriteLine("任务取消，开始清理工作");
                Thread.Sleep(2000);
                Console.WriteLine("任务取消，清理工作结束");
            });
            //自动取消
            //方式一
            cts.CancelAfter(3000);//3秒自动取消
            //方式二：在创建的时候设置
            //CancellationTokenSource cts = new CancellationTokenSource(3000);
        }
        #endregion

        #region Task使用【5】异常处理：AggregateException
        /// <summary>
        /// AggregateException:异常集合：Task可能抛出异常，需要新的类型收集对象
        /// </summary>
        private static void Method12()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                Task task1 = Task.Factory.StartNew(() =>
                {
                    //处理业务

                    //模拟异常
                    throw new Exception("task1抛出异常");
                }, TaskCreationOptions.AttachedToParent);
                Task task2 = Task.Factory.StartNew(() =>
                {
                    throw new Exception("task2抛出异常");
                }, TaskCreationOptions.AttachedToParent);
            });
            try
            {
                try
                {
                    task.Wait();//异常抛出的时机
                }
                catch (AggregateException ex)
                {
                    foreach (Exception item in ex.InnerExceptions)
                    {
                        Console.WriteLine(item.InnerException.Message + "   " + item.GetType().Name);
                    }
                    //往上层抛
                    ex.Handle(p =>
                    {
                        if (p.InnerException.Message == "task1抛出异常")
                            return true;
                        else
                            return false;//返回false表示继续往上层抛出异常
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
        #endregion

        #region Task使用【5】Lock 限制线程个数的一把锁
        /// <summary>
        /// 为什么用锁？多线程访问时，尤其是静态资源的访问，会存在竞争
        /// </summary>
        private static int nums = 0;
        private static object mylock= new object();
     

        private static void Method13()
        {
            for (int i = 0; i < 6; i++)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    Test();
                });
            }
        }

        static void Test()
        {
            for (int i = 0; i < 100; i++)
            {
                lock (mylock)
                {
                    nums++;
                    Console.WriteLine(nums);
                }
              
            }
        }
        #endregion
    }
}
