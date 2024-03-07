﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeService.Models
{
    public class ResponseMessage
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
    }
}