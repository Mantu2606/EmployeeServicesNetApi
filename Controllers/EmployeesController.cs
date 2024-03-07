using Dapper;
using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    //for android emulator 
    //adb reverse tcp:61038 tcp:61038
    [RoutePrefix("api")]
    public class EmployeesController : ApiController
    {
        DbHealper db = new DbHealper();
        // GET: Employee

        [Route("getEmployees")]
        [HttpGet]
        public IHttpActionResult getEmployees()
        {
            var studentList = db.ExeQuery<Student>(null, "SELECT * FROM student").ToList();
            return Ok(studentList);
        }

        [Route("validateLogin")]
        [HttpPost]
        public IHttpActionResult validateLogin(Student student)
        {
            var loginQuery = "SELECT * FROM student where email=@email and password = @password";

            var param = new DynamicParameters();
            param.Add("@email", student.email);
            param.Add("@password", student.password);
            var studentList = db.ExeQuerySingle<Student>(param, loginQuery);
            if (studentList != null && studentList.email == student.email && studentList.password == student.password)
            {
                return Ok(new ResponseMessage() { StatusCode = 200, Message = "Login Successfull", Result = studentList });
            }
            else
            {
                return Ok(new ResponseMessage() { StatusCode = 401, Message = "Login failed!" });
            }
        }

        [Route("insertEmployee")]
        [HttpPost]
        public IHttpActionResult insertEmployee(Student student)
        {
            string query = "insert into student (name,address,email, password,Father_Name,Mobile_No,Pin_No) values(@name,@address,@email,@password,@Father_Name,@Mobile_No,@Pin_NO)";
            var param = new DynamicParameters();
            param.Add("@name", student.name);
            param.Add("@address", student.address);
            param.Add("@email", student.email);
            param.Add("@password", student.password);
            param.Add("@Father_Name", student.Father_Name);
            param.Add("@Mobile_No", student.Mobile_No);
            param.Add("@Pin_No", student.Pin_NO);
            var studentList = db.ExcecuteNonQuery(param, query);
            return Ok(new ResponseMessage() { StatusCode = 200, Message = "Student Inserted Successfully." });
        }

        [Route("updateEmployee/{id}")]
        [HttpPut]
        public IHttpActionResult updateEmployee([FromUri] string id, [FromBody] Student student)
        {
            string query = "update student set name=@name,address=@address, email=@email, password=@password  where id=@id";
            var param = new DynamicParameters();
            param.Add("@id", id);
            param.Add("@name", student.name);
            param.Add("@address", student.address);
            param.Add("@email", student.email);
            param.Add("@password", student.password);

            var studentList = db.ExcecuteNonQuery(param, query);
            return Ok(new ResponseMessage() { StatusCode = 200, Message = "Student Inserted Successfully." });
        }


        [Route("DeleteEmployee/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(string id)
        {
            string query = "Delete FROM student WHERE id = @id";
            var param = new DynamicParameters();
            param.Add("@id", id);
            var studentList = db.ExcecuteNonQuery(param , query);
            return Ok(new ResponseMessage() { StatusCode = 200, Message = "Student Delete Successfully." });
        }
        [Route("searchEmployee")]
        [HttpPost]
        public IHttpActionResult searchEmployee(Student student)
        {
            string query = "select *  from  student where id = @id";
            var param = new DynamicParameters();
            param.Add("@id", student.id);

            var studentList = db.ExcecuteNonQuery(param, query);
            return Ok(new ResponseMessage() { StatusCode = 200, Message = "Student Delete Successfully." });
        }

        [Route("uploadImage/{id}")]
        [HttpPost]
        public IHttpActionResult uploadImage(string id)
        {
            string query = "INSERT INTO images (ImageName,ImagePath) VALUES (@ImageName, @ImagePath)";
            var param = new DynamicParameters();
            var response = new ResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files[0];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                int MaxContentLength = 1024 * 1024 * 4; // 4 MB
                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                var extension = ext.ToLower();
                if (!AllowedFileExtensions.Contains(extension))
                {
                    var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                    response = new ResponseMessage() { StatusCode = (int)HttpStatusCode.BadRequest, Message = message };
                }
                else if (postedFile.ContentLength > MaxContentLength)
                {
                    var message = string.Format("Please Upload a file upto 4 mb.");
                    response = new ResponseMessage() { StatusCode = (int)HttpStatusCode.BadRequest, Message = message };
                }
                else
                {
                    var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    response = new ResponseMessage() { StatusCode = (int)HttpStatusCode.Created, Message = "Image uploaded successfully." };
                    
                    param.Add("@ImageName", postedFile.FileName);
                    param.Add("@ImagePath", ext);
                    db.ExcecuteNonQuery(param, query);
                }

            }

            return Ok(response);
        }

        [Route("getPractice")]
        [HttpGet]
        public IHttpActionResult getPractice()
        {
            var practice = db.ExeQuery<PracticeController>(null, "SELECT * FROM practice").ToList();
            return Ok(practice);
        }

        [Route("insertPractice")]
        [HttpPost]
        public IHttpActionResult insertPractice(PracticeController practice)
        {
            string query = "insert into practice (UserName,Email,FullName, Password,UserRole) values(@UserName,@Email,@FullName,@Password,@UserRole)";
            var param = new DynamicParameters();
            param.Add("@UserName", practice.UserName);
            param.Add("@Email", practice.Email);
            param.Add("@FullName", practice.FullName);
            param.Add("@Password", practice.Password);
            param.Add("@UserRole", practice.UserRole);
            var studentList = db.ExcecuteNonQuery(param, query);
            return Ok(new ResponseMessage() { StatusCode = 200, Message = "Student Inserted Successfully." });
        }
        [Route("updatePractice/{id}")]
        [HttpPut]
        public IHttpActionResult updatePractice([FromUri] string id, [FromBody] PracticeController practice)
        {
            string query = "update practice set UserName=@UserName,Email=@Email, FullName=@FullName, where id=@id";
            var param = new DynamicParameters();
            param.Add("@id", id);
            param.Add("@UserName", practice.UserName);
            param.Add("@Email", practice.Email);
            param.Add("@FullName", practice.FullName);

            var studentList = db.ExcecuteNonQuery(param, query);
            return Ok(new ResponseMessage() { StatusCode = 200, Message = "Student Inserted Successfully." });
        }
    }
}
