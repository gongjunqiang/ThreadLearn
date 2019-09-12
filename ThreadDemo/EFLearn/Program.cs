using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddModel();

            //Modify();
            //Modify1();


            //Delete();
            //Delete2();

            Query();


            Console.ReadKey();
        }

        /// <summary>
        /// 新增对象
        /// </summary>
        static void AddModel()
        {
            //【1】数据验证
            //【2】对象封装
            Students student = new Students
            {
                StudentName = "龚均强",
                Gender = "男",
                Birthday = Convert.ToDateTime("1991-04-22"),
                StudentIdNo = 510722199104221978,
                CardNo = "1958141",
                Age = 28,
                PhoneNumber = "18408230331",
                StudentAddress = "西华大学",
                ClassId = 1,
            };
            //【3】创建数据库上下文对象
            StudentManageDBEntities db = new StudentManageDBEntities();
            db.Students.Add(student);//将对象添加到缓存中
            int result = db.SaveChanges();//跟新到数据库（自动生成SQL语句）
            Console.WriteLine(result);
            Console.WriteLine("学号：" + student.StudentId);
        }


        /// <summary>
        /// 编辑：修改
        /// </summary>
        static void Modify()
        {
            //【1】数据验证
            //【2】对象封装
            Students student = new Students
            {
                StudentId = 100012,
                StudentName = "龚均强",
                Gender = "男",
                Birthday = Convert.ToDateTime("1991-04-22"),
                StudentIdNo = 510722199104221978,
                CardNo = "0000000",
                Age = 28,
                PhoneNumber = "18408230331",
                StudentAddress = "西华大学",
                ClassId = 2,
            };
            //【3】创建数据库上下文对象
            StudentManageDBEntities db = new StudentManageDBEntities();
            db.Students.Attach(student);
            db.Entry(student).State = EntityState.Modified;//设置对象的状态为修改
            int result = db.SaveChanges();//跟新到数据库（自动生成SQL语句）
            Console.WriteLine(result);
            Console.WriteLine("学号：" + student.StudentId);
        }

        /// <summary>
        /// 部分字段的修改
        /// </summary>
        static void Modify1()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            //查询对象
            var stu = db.Students.FirstOrDefault(o => o.StudentId == 100012);
            //修改对象属性
            stu.StudentAddress = "红哇小区";
            //提交修改
            int result = db.SaveChanges();//跟新到数据库（自动生成SQL语句）
            Console.WriteLine("影响行数："+result);

        }


        /// <summary>
        /// 线查找对象再删除对象
        /// </summary>
        static void Delete()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            //查询对象
            var student = db.Students.FirstOrDefault(o => o.StudentId == 100012);
            //从缓存中移除对象删除
            db.Students.Remove(student);
            //提交
            int result = db.SaveChanges();//跟新到数据库（自动生成SQL语句）
            Console.WriteLine("影响行数：" + result);
        }

        static void Delete2()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            Students student = new Students
            {
                StudentId = 100013,
            };
            db.Set<Students>().Attach(student);
            db.Entry(student).State = EntityState.Deleted;

            //提交
            int result = db.SaveChanges();//跟新到数据库（自动生成SQL语句）
            Console.WriteLine("影响行数：" + result);
        }


        static void Query()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            //var list = db.Students.Where(o => o.StudentId > 100000);
            ////var list = db.Students.Select(o=>o.StudentId);
            var list = from a in db.Students where a.StudentId > 100000 select a;
            Console.WriteLine(list);
            foreach (var item in list)
            {
                Console.WriteLine(item.StudentName+"\t"+item.Birthday+"\t" + item.StudentAddress);
            }
   
        }
    }
}
