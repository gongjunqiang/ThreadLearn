using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 基础CURD
            //AddModel();
            //Modify();
            //Modify1();
            //Delete();
            //Delete2();
            //Query();
            #endregion

            #region EF高级查询与性能优化

            //LinkToString();
            //LinkTolist();
            //LinkToEntities();
            //DataProjection();
            //DataTolist();
            //MultQuery();
            //NestQuery();
            //Console.WriteLine("----------------");
            //SuQuery();
            //SlelectQuery();
            //Status();
            //StatusTracking();

            //SelectStudents();
            //ExecSql();

            //ExecProcedure();
            ExecSelect();
            #endregion


            Console.ReadKey();
        }

        #region EF基础CURD
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
        #endregion

        #region EF高级查询与性能优化

        #region LinkToStringAndLinkTolist
        static void LinkToString()
        {
            string str = "We Are Studying Link To String";
            //查询语句
            var query1 = from o in str
                select o;
            Console.WriteLine(query1.ToString());

            foreach (var item in query1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("------------------");
            //查询方法
            var query2 = str.Where(o => char.IsUpper(o));

            foreach (var item in query2)
            {
                Console.WriteLine(item);
            }
        }

        static void LinkTolist()
        {

            List<Students> studentses = new List<Students>
            {
                new Students(){StudentName = "龚均强",Gender = "男",Age = 26},
                new Students(){StudentName = "龚小红",Gender = "女",Age = 30},
                new Students(){StudentName = "龚均兵",Gender = "男",Age = 35},
            };
            //查询语句
            var query1 = from o in studentses
                where o.Gender == "男" && o.Age > 20
                orderby o.Age 
                select o;

            foreach (var student in query1)
            {
                Console.WriteLine("名字：{0}\t性别：{1}\t年龄：{2}", student.StudentName, student.Gender, student.Age);
            }

            Console.WriteLine("-------------------------");
            //查询方法
            var query2 = studentses.Where(o => o.Gender == "男" && o.Age > 20)
                .OrderByDescending(o => o.Age);
            foreach (var student in query2)
            {
                Console.WriteLine("名字：{0}\t性别：{1}\t年龄：{2}", student.StudentName, student.Gender, student.Age);
            }

        }
        #endregion

        #region 规范函数的使用
        static void LinkToEntities()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            //Convert不能被转变换成对应的sql语句；会报错
//            var query1 = from o in db.Students
//                         where o.StudentName.StartsWith("龚") && o.Gender=="男" && o.Birthday>Convert.ToDateTime("1900-01-11")
//                orderby o.Age descending 
//                select o;


            //解决办法：
            //可以先将不能转换成对应sql语句的方法单独拿出来转换后在使用
            DateTime dt = Convert.ToDateTime("1800-01-11");

            var query1 = from o in db.Students
                where o.StudentName.StartsWith("龚") && o.Gender == "女" && o.Birthday > dt
                orderby o.Age descending
                select o;
            Console.WriteLine(query1);
            Console.WriteLine("查询总数：{0}", query1.Count());
        }
        #endregion

        #region 数据投影与list的转换
        //数据投影与匿名对象
        static void DataProjection()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            DateTime dt = Convert.ToDateTime("1800-01-11");

            var query1 = from o in db.Students
                where o.StudentName.StartsWith("龚") && o.Gender == "女" && o.Birthday > dt
                orderby o.Age descending
                select new {o.StudentId, o.StudentName, o.Birthday, o.Gender, o.Age};

            foreach (var student in query1)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t", student.StudentId, student.StudentName, student.Gender, student.Age);
            }


            Console.WriteLine(query1);
            Console.WriteLine("查询总数：{0}", query1.Count());

        }

        //数据投影到对象集合
        static void DataTolist()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            DateTime dt = Convert.ToDateTime("1800-01-11");
            var query1 = from o in db.Students
                where o.StudentName.StartsWith("龚") && o.Gender == "女" && o.Birthday > dt
                orderby o.Age descending
                select new { o.StudentId, o.StudentName, o.Birthday, o.Gender, o.Age };
            List<Students> students = new List<Students>();
            foreach (var item in query1)
            {
                students.Add(new Students
                {
                    StudentId = item.StudentId,
                    StudentName = item.StudentName,
                    Gender = item.Gender,
                    Age = item.Age
                });
            }
            foreach (var student in students)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", student.StudentId, student.StudentName, student.Gender, student.Age);
            }
        }

        #endregion

        #region 多表查询
        //导航属性与join查询
        static void MultQuery()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            DateTime dt = Convert.ToDateTime("1800-01-11");

            var query1 = from o in db.ScoreList
                where o.Students.Gender == "女" && o.Students.StudentName.StartsWith("龚")
                select new
                {
                    o.Students.StudentName, o.Students.Gender, o.Students.StudentClass.ClassName, o.CSharp,
                    o.SQLServerDB
                };

            foreach (var student in query1)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", student.StudentName, student.Gender, student.ClassName, student.CSharp);
            }

            Console.WriteLine("-----------------------");
            var query2 = from o in db.ScoreList
                join s in db.Students on o.StudentId equals s.StudentId
                join c in db.StudentClass on s.ClassId equals c.ClassId
                where s.Gender == "女" && s.StudentName.StartsWith("龚")
                select new {s.StudentName, s.Gender, c.ClassName, o.CSharp, o.SQLServerDB};
            foreach (var student in query2)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", student.StudentName, student.Gender, student.ClassName, student.CSharp);
            }
        }


        #endregion

        #region 嵌套查询与子查询
        //嵌套查询
        static void NestQuery()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();

            var query1 = from o in db.Students
                where o.ClassId == 1 || o.ClassId == 2
                select new {o.StudentClass.ClassName, o.StudentName, o.StudentId};

            Console.WriteLine(query1);
            var query2 = from s in query1
                where s.StudentId > 100000
                select s;
            Console.WriteLine(query2);
            foreach (var student in query2)
            {
                Console.WriteLine("{0}\t{1}\t{2}", student.ClassName, student.StudentName, student.StudentId);
            }

        }

        //子查询
        static void SuQuery()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();

            var query2 = from s in (from o in db.Students
                    where o.ClassId == 1 || o.ClassId == 2
                    select new { o.StudentClass.ClassName, o.StudentName, o.StudentId })
                where s.StudentId > 100000
                select s;
            Console.WriteLine(query2);
            foreach (var student in query2)
            {
                Console.WriteLine("{0}\t{1}\t{2}", student.ClassName, student.StudentName, student.StudentId);
            }
        }
        //在select子句中使用子查询
        static void SlelectQuery()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();

            var query2 = from s in db.Students
                select new
                {
                    name = s.StudentName,
                    subject = from o in s.ScoreList select new {sql = o.CSharp, DB = o.SQLServerDB}
                };
            Console.WriteLine(query2);
            foreach (var student in query2)
            {
                Console.WriteLine(student.name);
                foreach (var s in student.subject)
                {
                    Console.WriteLine("{0}\t{1}\n", s.DB,s.sql);
                }
            }
        }

        #endregion

        #region 状态跟踪在CURD中的影响何优化
        //状态跟踪类型测试
        static void Status()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            //创建一个对象：当前对象的状态为detached
            Students student = new Students
            {
                StudentName = "龚均强",
                Gender = "男",
                Birthday = Convert.ToDateTime("1991-04-22"),
                StudentIdNo = 520722199104221978,
                CardNo = "2958141",
                Age = 28,
                PhoneNumber = "18408230331",
                StudentAddress = "西华大学",
                ClassId = 1,
            };
            Console.WriteLine(db.Entry(student).State.ToString());
            //添加到缓存对象中：状态为Added
            db.Students.Add(student);
            Console.WriteLine(db.Entry(student).State.ToString());
            //保存后：UnChanged
            db.SaveChanges();
            Console.WriteLine(db.Entry(student).State.ToString());
        }
        //状态跟踪与无状态跟踪
        static void StatusTracking()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            //状态跟踪查询：UnChanged
            var stu1 = (from o in db.Students select o).FirstOrDefault();
            Console.WriteLine(db.Entry(stu1).State.ToString());

            //无状态查询：detached  查询不被跟踪，好处：提升性能，返回实体不缓存，适合纯粹查询
            var stu2 = db.Students.AsNoTracking().Select(o=>o).FirstOrDefault();
            Console.WriteLine(db.Entry(stu2).State.ToString());
        }

        static void StatusTrackings()
        {
            StudentManageDBEntities db = new StudentManageDBEntities();
            //禁用自动跟踪
            db.Configuration.AutoDetectChangesEnabled = false;
        }



        #endregion

        #region CURD的标准优化

        #region insert
        static void AddStudents()
        {

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

            //第一种写法：
            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                db.Entry(student).State = EntityState.Added;
                db.SaveChanges();
            }

            //第二种写法：
            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                //db.Students.Attach(student);//可以省略，将student实体添加到上下文中
                db.Students.Add(student);
                db.SaveChanges();
            }
        }
        #endregion

        #region update
        static void UpdateStudents()
        {
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

            //第一种写法：构建全部属性对象修改
            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                //db.Students.Attach(student);//可以省略，将student实体添加到上下文中
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
            }

            //第二种写法：先查询再修改
            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                //查询对象
                var stu = db.Students.FirstOrDefault(o => o.StudentId == 100012);
                //修改对象属性
                stu.StudentAddress = "红哇小区";
                //提交修改
                db.SaveChanges();//跟新到数据库（自动生成SQL语句）
            }
        }
        #endregion

        #region delete
        static void DeleteStudents()
        {
            //根据主键构建一个对象
            Students student = new Students
            {
                StudentId = 100000
            };

            //第一种写法：通过设置状态
            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                db.Set<Students>().Attach(student);
                db.Entry(student).State = EntityState.Deleted;
                db.SaveChanges();
            }

            //第二种写法：通过remove方法
            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                db.Set<Students>().Attach(student);
                db.Students.Remove(student);
                db.SaveChanges();//跟新到数据库（自动生成SQL语句）
            }

            //第三种写法：先查寻，再删除
            
            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                var stu = db.Students.Where(o => o.StudentId == 100000).FirstOrDefault();
                db.Students.Remove(stu);
                db.SaveChanges();//跟新到数据库（自动生成SQL语句）
            }
        }
        #endregion

        #region select
        static void SelectStudents()
        {
            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                var stulist = from o in db.Students select o;
                foreach (var student in stulist)
                {
                    Console.WriteLine(student.StudentName+"\t"+student.StudentId);
                }
                //查询学总数
                Console.WriteLine("学生总数："+ db.Students.Count());
                //使用缓存查询总数
                Console.WriteLine("学生总数：" + db.Students.Local.Count());

            }
        }
        #endregion



        #endregion

        #region EF执行原声的sql语句与存储过程
        //EF执行原声SQL语句：增删改操作，使用DBContext对象的DataBase属性，该属性支持一系列数据库操作
        static void ExecSql()
        {
            string sql1 = "update Students set StudentName='张某某' where StudentId=100002";
            string sql2 = "update Students set StudentName=@StudentName where StudentId=@StudentId";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@StudentName","龚老师"),
                new SqlParameter("@StudentId",100000),
            };

            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                var result1=db.Database.ExecuteSqlCommand(sql1);
                var result2 = db.Database.ExecuteSqlCommand(sql2,paras);
                Console.WriteLine("执行结果："+result1+"\t"+ result2);
            }
        }

        //select
        static void ExecSelect()
        {
            var sql = "select count(1) from Students";
            var sql1 = "select * from Students where ClassId=@ClassId";

            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@ClassId",1),
            };

            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                var result1 = db.Database.SqlQuery<int>(sql).ToList()[0];
                var result2 = db.Database.SqlQuery<Students>(sql1, paras1);
                foreach (var stu in result2)
                {
                    Console.WriteLine(stu.StudentName);
                }
                Console.WriteLine("总数："+result1);
            }
        }
        //调用存储过程
        static void ExecProcedure()
        {
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@StudentName","龚老师"),
                new SqlParameter("@StudentId",100001),
            };

            SqlParameter[] paras1 = new SqlParameter[]
            {
                new SqlParameter("@ClassId",1),
            };

            using (StudentManageDBEntities db = new StudentManageDBEntities())
            {
                var result1 = db.Database.ExecuteSqlCommand("execute usp_updateStu @StudentName,@StudentId", paras);
                var result2 = db.Database.SqlQuery<Students>("execute usp_selectStu @ClassId", paras1);
                foreach (var stu in result2)
                {
                    Console.WriteLine(stu.StudentName);
                }
                Console.WriteLine("第一个受影响行数：" + result1);
            }
        }
        #endregion
        #endregion
    }
}
