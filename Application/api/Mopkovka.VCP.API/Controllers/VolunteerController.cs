using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Mopkovka.VCP.API.Models;
using Microsoft.AspNetCore.Hosting;

namespace Mopkovka.VCP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public VolunteerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select VolunteerId, VolunteerFirstName, VolunteerLastName, Activity, Institute from
                            dbo.Volunteer
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
        public JsonResult Post(Volunteer vol)
        {
            string query = @"
                           insert into dbo.Volunteer
                           (VolunteerFirstName, VolunteerLastName, Activity, Institute)
                    values (@VolunteerFirstName, @VolunteerLastName, @Activity, @Institute)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@VolunteerFirstName", vol.VolunteerFirstName);
                    myCommand.Parameters.AddWithValue("@VolunteerLastName", vol.VolunteerLastName);
                    myCommand.Parameters.AddWithValue("@Activity", vol.Activity);
                    myCommand.Parameters.AddWithValue("@Institute", vol.Institute);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Volunteer vol)
        {
            string query = @"
                           update dbo.Volunteer
                           set VolunteerFirstName = @VolunteerFirstName,
                            VolunteerLastName = @VolunteerLastName,
                            Activity = @Activity,
                            Institute = @Institute
                            where VolunteerId = @VolunteerId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@VolunteerId", vol.VolunteerId);
                    myCommand.Parameters.AddWithValue("@VolunteerFirstName", vol.VolunteerFirstName);
                    myCommand.Parameters.AddWithValue("@VolunteerLastName", vol.VolunteerLastName);
                    myCommand.Parameters.AddWithValue("@Activity", vol.Activity);
                    myCommand.Parameters.AddWithValue("@Institute", vol.Institute);
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
                           delete from dbo.Volunteer
                            where VolunteerId=@VolunteerId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("VolunteerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@VolunteerId", id);

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
