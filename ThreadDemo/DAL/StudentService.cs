using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace DAL
{
    public class StudentService
    {
        public List<Students> QueryStudentses(int classId)
        {
            using (EFDBEntities db = new EFDBEntities())
            {
                var stulist = db.Students.Where(o => o.ClassId == classId).ToList();
                return stulist;
            }
           
        }


    }
}
