using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    static class ExtendMethod
    {
        public static int Square(this int num)
        {
            return num * num;
        }
    }
}
