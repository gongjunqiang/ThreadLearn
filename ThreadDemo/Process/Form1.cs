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

namespace Process
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 20; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(500);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 20; i++)
            {
                Console.WriteLine($"-------------{i}--------------------");
                Thread.Sleep(500);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 1; i < 20; i++)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(500);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 1; i < 20; i++)
                {
                    Console.WriteLine($"-------------{i}--------------------");
                    Thread.Sleep(500);
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        public void Test()
        {
            for (int i = 0; i< 20; i++)
            {
                 Console.WriteLine($"----------{i}------------");
                 Thread.Sleep(500);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Test);
        
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
