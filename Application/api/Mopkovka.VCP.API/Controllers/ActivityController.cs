using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Mopkovka.VCP.API.Models;

namespace Mopkovka.VCP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ActivityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select ActivityId, ActivityName, DateOfActivity from
                            dbo.Activity
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Activity act)
        {
            string query = @"
                           insert into dbo.Activity 
                           (ActivityName, DateOfActivity)
                    values (@ActivityName, @DateOfActivity)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ActivityName", act.ActivityName);
                    myCommand.Parameters.AddWithValue("@DateOfActivity", act.DateOfActivity);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Activity act)
        {
            string query = @"
                           update dbo.Activity
                           set ActivityName = @ActivityName,
                            DateOfActivity = @DateOfActivity
                            where ActivityId=@ActivityId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ActivityId", act.ActivityId);
                    myCommand.Parameters.AddWithValue("@ActivityName", act.ActivityName);
                    myCommand.Parameters.AddWithValue("@DateOfActivity", act.DateOfActivity);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Activity
                            where ActivityId=@ActivityId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ActivityId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
