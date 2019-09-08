using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace ThreadDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //执行任务1
        private void Button1_Click_1(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 1; i < 10; i++)
                {
                    //this.lbl1.Text = i.ToString();
                    if (this.lbl1.InvokeRequired)
                    {
                        this.lbl1.Invoke(new Action<string>(data => { this.lbl1.Text = data; }), i.ToString());
                        Thread.Sleep(200);
                    }
                }
              
            });
            thread.IsBackground = true;
            thread.Start();
        }
        //执行任务2
        private void Button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 1; i < 10; i++)
                {
                    //this.lbl1.Text = i.ToString();
                    if (this.lbl2.InvokeRequired)
                    {
                        this.lbl2.Invoke(new Action<string>(data => { this.lbl2.Text = data; }), i.ToString());
                        Thread.Sleep(200);
                    }
                }

            });
            thread.IsBackground = true;
            thread.Start();
        }
        //线程读取电压
        private void Button3_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(() =>
            {
                if (this.lbl3.InvokeRequired)
                {

                    this.lbl3.Invoke(new Action<string>(data => { this.lbl3.Text = data; }), this.textBox1.Text);

                    //this.textBox1.Invoke(new Action<string>(data => { this.lbl3.Text = data; }), this.textBox1.Text);//都行
                }

            });
            thread.IsBackground = true;
            thread.Start();

        }
        #region Task 跨控件访问

     
        /// <summary>
        /// 普通方法;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            Task task = new Task(() =>
            {
                this.lbl.Text = "来自task的数据跟新，多线程";
            });
            //task.Start();//这个会报错：线程间操作无效: 从不是创建控件“lbl”的线程访问它
            task.Start(TaskScheduler.FromCurrentSynchronizationContext());//解决报错的方法
        }
        /// <summary>
        /// 针对Ui耗时的情况;task.Start(TaskScheduler.FromCurrentSynchronizationContext())重载这种方式并不是很好
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            Task task = new Task(() =>
            {
                //模拟耗时(这个地方会卡住，等到完成后才能继续操作)
                Thread.Sleep(5000);
                this.lbl.Text = "来自task的数据跟新，多线程";
            });
            //task.Start();//这个会报错：线程间操作无效: 从不是创建控件“lbl”的线程访问它
            task.Start(TaskScheduler.FromCurrentSynchronizationContext());//解决报错的方法
        }
        /// <summary>
        /// 真的耗时的可以这样做
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, EventArgs e)
        {
            this.button4.Enabled = false;
            this.lbl.Text = "数据更新中。。。。。。。。。。。。。";
            Task task = Task.Factory.StartNew(() =>
            {
                //模拟耗时可以放到ThreadPool中
                Thread.Sleep(5000);
                
            });

            //在ContinueWith跟新数据
            task.ContinueWith(t =>
            {
                this.lbl.Text = "来自task的数据跟新，多线程";
                this.button4.Enabled = true;
            },TaskScheduler.FromCurrentSynchronizationContext());//跟新操作到同步的上下文中
        }
        #endregion
    }
}
