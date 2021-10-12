using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using AutoMapper;


namespace University.API.Controllers
{

        [RoutePrefix("api/Courses")]
        public class CoursesController : ApiController

        {
            private readonly IMapper mapper;
        private readonly CourseRepository courseRepository = new CourseRepository(new UniversityEntities());

        public CoursesController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        /// <summary>
        /// Bùsqueda de todos los cursos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var courses = await courseRepository.GetAll();
            var coursesDTO = courses.Select(x => mapper.Map<CourseDTO>(x));

            return Ok(coursesDTO);
        }

        /// <summary>
        /// bùsqueda de curso por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto del curso</returns>
        /// <response code="200">OK.Devuelve el objeto solicitado</response>

        [HttpGet]
        //[Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var course = await courseRepository.GetById(id);
            var courseDTO = mapper.Map<CourseDTO>(course);
            return Ok(courseDTO);
        }
        /// <summary>
        /// Crear un objeto de curso
        /// </summary>
        /// <param name="courseDTO">Objeto del curso</param>
        /// <returns>Objeto del curso</returns>
        ///  <response code="200">OK.Devuelve el objeto solicitado</response>
        ///   <response code="400">BadRequest.No se cumple con validaciòn del modelo</response>
        ///   <response code="500">InternalServerError. Se ha presentado un error</response>
        [HttpPost]

        public async Task<IHttpActionResult> Create(CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var course = mapper.Map<Course>(courseDTO);
                course = await courseRepository.Insert(course);



                courseDTO.CourseID = course.CourseID;
                return Ok(courseDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Editar un curso
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseDTO"></param>
        /// <returns></returns>
        ///  <response code="200">OK.Devuelve el objeto solicitado</response>
        ///   <response code="400">BadRequest.No se cumple con validaciòn del modelo</response>
        ///   <response code="500">InternalServerError. Se ha presentado un error</response>
        [HttpPut]

        public async Task<IHttpActionResult> Edit(int id, CourseDTO courseDTO)
        {
            try
            {
                if (id != courseDTO.CourseID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var course = await courseRepository.GetById(id);
                if (course == null)
                    return NotFound();

                // UPDATE FIELD
                course.CourseID = courseDTO.CourseID;
                course.Title = courseDTO.Title;
                course.Credits = courseDTO.Credits;

                //UPDATE  ALL
                //course = mapper.Map<Course>(courseDTO);

                await courseRepository.Update(course);


                return Ok(courseDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Eliminar un curso
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///   <response code="200">OK.Devuelve el objeto solicitado</response>
        ///   <response code="400">BadRequest.No se cumple con validaciòn del modelo</response>
        ///   <response code="500">InternalServerError. Se ha presentado un error</response>
        [HttpDelete]

        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {


                var course = await courseRepository.GetById(id);
                if (course == null)
                    return NotFound();

             
                await courseRepository.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


    }
}

