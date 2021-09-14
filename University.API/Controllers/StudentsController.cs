using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;

namespace University.API.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly UniversityEntities context = new UniversityEntities();

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var students = context.Student.ToList();
            var studentDTO = students.Select(x => new StudentDTO
            {
                ID = x.ID,
                FirstMidName = x.FirstMidName,
                LastName = x.LastName,
                EnrollmentDate = x.EnrollmentDate.Value
            }).ToList();
            return Ok(studentDTO);
        }
    }
}
