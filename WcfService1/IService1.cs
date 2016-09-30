using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        bool AddStudent(Student student);

        [OperationContract]
        bool RemoveStudent(Student student);

        [OperationContract]
        List<Student> getStudentByFirstName(String firstname);


        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Student
    {
        
        private string _lastName;
        private string _firstName;
        private string _cpr;

        [DataMember]
        public string Cpr
        {
            get { return _cpr; }
            set { _cpr = value; }
        }

        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public override bool Equals(Object obj)
        {
            if (obj.GetType() != typeof(Student))
            {
                return false;
            }
            else // Nu ved jeg at det er et student obj
            {
                Student studentObj = (Student) obj;
                if (_cpr.Equals(studentObj.Cpr)) //hvis cpr er ens er objekterne ens!
                {
                    return true;
                }
                return false;
            }
        }
    }
}
