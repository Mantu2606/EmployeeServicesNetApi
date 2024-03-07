using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeService.Models
{
    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public object Father_Name { get; set; }
        public object Mobile_No { get;  set; }
        public object Pin_NO { get; set; }
    }
}