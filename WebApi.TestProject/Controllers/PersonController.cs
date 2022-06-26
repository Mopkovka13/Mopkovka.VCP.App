using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.TestProject.Models;

namespace WebApi.TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public Person Get([FromQuery]Person person)
        {
            var rng = new Random();
            return new Person
            {
                Id = 2,
                FirstName = "",
                LastName = "Petrov",
                Age = -22,
                Email = "VladPetrovmail.ru",
                Phone = "+79022131net"
            };
        }
    }
}
