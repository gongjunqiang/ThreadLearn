using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace BLL
{
    public class StudentManager
    {
        private StudentService service = new StudentService();
        public List<Students> QueryStudentses(int classId)
        {
            return service.QueryStudentses(classId);

        }

    }
}
