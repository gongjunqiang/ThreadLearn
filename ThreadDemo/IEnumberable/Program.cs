using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumberable
{
    class Program
    {
        static void Main(string[] args)
        {
            show("-----------IEnumerator------------");
            Phone[] phones = new Phone[] { new Phone("iPhone 7s"), new Phone("iPhone 6s"), new Phone("iPhone 5s") };
            MyEnumerator enumerator = new MyEnumerator(phones);
            while (enumerator.MoveNext())
            {
                Phone p = enumerator.Current as Phone;
                show(p.Name);
            }

            show("-----------IEnumerable------------");
            Phone phoneList = new Phone(phones);
            foreach (Phone p in phoneList)
            {
                show(p.Name);
            }

            show("-----------IEnumerable<T>------------");
            PhonePackage<Phone> phonePackage = new PhonePackage<Phone>();

            phonePackage.Add(new Phone("iPhone 7s"));

            phonePackage.Add(new Phone("iPhone 6s"));

            phonePackage.Add(new Phone("iPhone 5s"));

            foreach (Phone p in phonePackage)
            {
                show(p.Name);
            }

            Console.ReadKey();

            #region 自定义IEnumerable
            //show("-----------IEnumerator------------");
            //Phone[] phones = new Phone[] { new Phone("iPhone 7s"), new Phone("iPhone 6s"), new Phone("iPhone 5s") };
            //MyEnumerator enumerator = new MyEnumerator(phones);
            //while (enumerator.MoveNext())
            //{
            //    Phone p = enumerator.Current as Phone;
            //    show(p.Name);
            //}
            //show("-----------IEnumerable------------");
            //Phone phoneList = new Phone(phones);
            //foreach (Phone p in phoneList)
            //{
            //    show(p.Name);
            //}
            #endregion
            Console.ReadKey();
        }
        static void show(string i)
        {
            Console.WriteLine(i);
        }
    }
}
