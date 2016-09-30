using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=cloudStudents;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool AddStudent(Student student)
        {
            

            //findes studerende allerede?
            foreach (var s in students)
            {
                //hvios studerende findes
                if (s.Equals(student)) return false;
            }

            //studerende findes ikke....
            //students.Add(student);
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                connection.Open();
                string sql = "INSERT INTO students(firstname, lastname, cpr) VALUES(@firstname,@lastname,@cpr)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@firstname", student.FirstName);
                cmd.Parameters.AddWithValue("@lastname", student.LastName);
                cmd.Parameters.AddWithValue("@cpr", student.Cpr);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public bool RemoveStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM students WHERE cpr = @cpr";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@cpr", student.Cpr);
                int result = cmd.ExecuteNonQuery();
                if (result >= 1) return true;
                return false;
            }
        }

        public List<Student> getStudentByFirstName(string firstname)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Student> toBeReturnedList = new List<Student>();

                connection.Open();
                string sql = "SELECT * FROM students WHERE firstname = @firstname";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@firstname", firstname);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student s = new Student();
                    s.FirstName = reader["firstname"].ToString();
                    s.LastName = reader.GetString(2);
                    s.Cpr = reader.GetString(3);
                    toBeReturnedList.Add(s);
                }
                return toBeReturnedList;

            }
            /*List<Student> toBeReturnedList = new List<Student>();
            foreach (var student in students)
            {
                if (student.FirstName.ToLower().Equals(firstname.ToLower()))
                {
                    toBeReturnedList.Add(student);
                }
            }
            return toBeReturnedList;*/
        }
    }
}
