using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Thread thread = null;

        private int count = 0;
        //开启
        private void Button1_Click(object sender, EventArgs e)
        {
            thread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(500);
                        textBox1.Invoke(new Action(() => { textBox1.Text += count++ + ","; }));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "异常位置：" + count++, "提升信息");
                    }
                }
            });
            thread.Start(); 
        }
        //挂起
        private void Button2_Click(object sender, EventArgs e)
        {
            if (thread.ThreadState == ThreadState.Running || thread.ThreadState == ThreadState.WaitSleepJoin)
            {
                thread.Suspend();
            }
        }
        //继续
        private void Button3_Click(object sender, EventArgs e)
        {
            if (thread.ThreadState == ThreadState.Suspended)
            {
                thread.Resume();
            }
        }
        //中断后可以继续运行
        private void Button4_Click(object sender, EventArgs e)
        {
            thread.Interrupt();
        }
        //放弃丢弃:终止线程
        private void Button5_Click(object sender, EventArgs e)
        {
            thread.Abort();
        }
    }
}
