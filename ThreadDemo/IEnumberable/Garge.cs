using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumberable
{

    public class PhonePackage<T> : IEnumerable<T>
    {
        private List<T> dataList = null;
        public void Add(T t)
        {
            if (dataList == null)
                dataList = new List<T>();
            dataList.Add(t);

        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T t in dataList)
            {
                yield return t;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (T t in dataList)
            {
                yield return t;
            }
        }

    }

    #region 自定义IEnumerable
    public class Phone : IEnumerable

    {
        Phone[] p;
        public string Name;

        public Phone(string name)
        {
            this.Name = name;
        }


        public Phone(Phone[] t)

        {

            p = t;

        }
        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(p);
        }
    }


    public class MyEnumerator : IEnumerator
    {
        Phone[] p;
        int idx = -1;
        public MyEnumerator(Phone[] t)
        {
            p = t;
        }

        public object Current
        {
            get
            {
                if (idx == -1)
                    return new IndexOutOfRangeException();
                return p[idx];
            }
        }

        public bool MoveNext()
        {
            idx++;
            return p.Length > idx;

        }

        public void Reset()
        {
            idx = -1;
        }

    }
    #endregion
}
