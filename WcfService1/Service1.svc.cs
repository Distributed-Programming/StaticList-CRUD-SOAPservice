using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static List<Student> students = new List<Student>();

        public bool AddStudent(Student student)
        {
            //findes studerende allerede?
            foreach (var s in students)
            {
                //hvios studerende findes
                if (s.Equals(student)) return false;
            }

            //studerende findes ikke....
            students.Add(student);
            return true;
        }

        public bool RemoveStudent(Student student)
        {
            foreach (var s in students)
            {
                if (s.Equals(student))
                {
                    students.Remove(student);
                    return true;
                }
     
            }
            return false;
        }

        public List<Student> getStudentByFirstName(string firstname)
        {
            List<Student> toBeReturnedList = new List<Student>();
            foreach (var student in students)
            {
                if (student.FirstName.ToLower().Equals(firstname.ToLower()))
                {
                    toBeReturnedList.Add(student);
                }
            }
            return toBeReturnedList;
        }
    }
}
