using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;

        }


        [HttpGet]
        public JsonResult Get()
        {
            string a = @"select Sid,Sname from dbo.Student";
            DataTable table1 = new DataTable();
            string sqlcon1 = _configuration.GetConnectionString("ConnString");
            SqlDataReader read1;
            using (SqlConnection conn1 = new SqlConnection(sqlcon1))
            {
                conn1.Open();
                using (SqlCommand command1 = new SqlCommand(a, conn1))
                {
                    read1 = command1.ExecuteReader();
                    table1.Load(read1);
                    read1.Close();
                    conn1.Close();
                }
            }
            return new JsonResult(table1);
        }

        [HttpPost]
        public JsonResult Post(Student std)
        {
            string b = @"insert into dbo.Student values (@Sname)";
            DataTable table2 = new DataTable();
            string sqlcon2 = _configuration.GetConnectionString("ConnString");
            SqlDataReader read2;
            using (SqlConnection conn2 = new SqlConnection(sqlcon2))
            {
                conn2.Open();
                using (SqlCommand command2 = new SqlCommand(b, conn2))
                {
                    command2.Parameters.AddWithValue("@Sname", std.Sname);
                    read2 = command2.ExecuteReader();
                    table2.Load(read2);
                    read2.Close();
                    conn2.Close();
                }
            }
            return new JsonResult("Insertion Done");

        }

        [HttpPut]
        public JsonResult Put(Student std)
        {
            string c = @"update dbo.Student set  Sname = @Sname where  Sid= @Sid";
            DataTable table3 = new DataTable();
            string sqlcon3 = _configuration.GetConnectionString("ConnString");
            SqlDataReader read3;
            using (SqlConnection conn3 = new SqlConnection(sqlcon3))
            {
                conn3.Open();
                using (SqlCommand command3 = new SqlCommand(c, conn3))
                {
                    command3.Parameters.AddWithValue("@Sname", std.Sname);
                    command3.Parameters.AddWithValue("@Sid", std.Sid);
                    read3 = command3.ExecuteReader();
                    table3.Load(read3);
                    read3.Close();
                    conn3.Close();
                }
            }
            return new JsonResult("Updation Done");

        }

        [HttpDelete]
        public JsonResult Delete(Student std)
        {
            string sqlcon4 = _configuration.GetConnectionString("ConnString");
            SqlDataReader read4;


            string d = @"delete from dbo.Student where  Sid = @Sid ";
            DataTable table4 = new DataTable();


            using (SqlConnection conn4 = new SqlConnection(sqlcon4))
            {
                conn4.Open();
                using (SqlCommand command4 = new SqlCommand(d, conn4))
                {
                    command4.Parameters.AddWithValue("@Sid", std.Sid);

                    read4 = command4.ExecuteReader();
                    table4.Load(read4);
                    read4.Close();
                    conn4.Close();
                }
            }
            return new JsonResult("Deletion Done");
        }
    }
}
