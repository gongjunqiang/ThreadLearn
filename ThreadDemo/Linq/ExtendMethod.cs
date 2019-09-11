using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    public static class ExtendMethod
    {
        public static int Square(this int num)
        {
            return num * num;
        }

        public static string StringShow(this string str)
        {
            return $"string的扩展显示：{str}";
        }

        public static string Show(this Person p)
        {
            return $"{p.PersonName}的ID是：{p.PersonId}";
        }


        public static string Show(this Person p,int age,string address)
        {
            return $"{p.PersonName}的ID是：{p.PersonId},年龄：{age}，住址：{address}";
        }
    }
}
