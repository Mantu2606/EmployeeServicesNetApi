using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Models
{
    public class PracticeController 
    {
            public int Id{ get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public object UserRole { get; set; }
     }
}