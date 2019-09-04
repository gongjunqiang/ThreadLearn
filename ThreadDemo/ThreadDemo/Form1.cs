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
    }
}
