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

        [RoutePrefix("api/Instructors")]
        public class InstructorsController : ApiController

        {

            private readonly IMapper mapper;
            private readonly InstructorRepository instructorRepository = new InstructorRepository(new UniversityEntities());


            public InstructorsController()
            {
                this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
            }
            /// <summary>
            /// Bùsqueda de todos los instructores
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            //[Route("GetAll")]
            public async Task<IHttpActionResult> GetAll()
            {
                var instructors = await instructorRepository.GetAll();
                var instructorsDTO = instructors.Select(x => mapper.Map<InstructorDTO>(x));

                return Ok(instructorsDTO);
            }

        /// <summary>
        /// bùsqueda de instructor por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto del instructor</returns>
        /// <response code="200">OK.Devuelve el objeto solicitado</response>

        [HttpGet]
            //[Route("GetById")]
            public async Task<IHttpActionResult> GetById(int id)
            {
                var instructor = await instructorRepository.GetById(id);
                var instructorDTO = mapper.Map<InstructorDTO>(instructor);
                return Ok(instructorDTO);
            }

        /// <summary>
        /// Crear un objeto de instructor
        /// </summary>
        /// <param name="instructorDTO">Objeto del instructor</param>
        /// <returns>Objeto del instructor</returns>
        ///  <response code="200">OK.Devuelve el objeto solicitado</response>
        ///   <response code="400">BadRequest.No se cumple con validaciòn del modelo</response>
        ///   <response code="500">InternalServerError. Se ha presentado un error</response>
        [HttpPost]

        public async Task<IHttpActionResult> Create(InstructorDTO instructorDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var instructor = mapper.Map<Instructor>(instructorDTO);
                instructor = await instructorRepository.Insert(instructor);



                instructorDTO.ID = instructor.ID;
                return Ok(instructorDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Editar un instructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="instructorDTO"></param>
        /// <returns></returns>
        ///  <response code="200">OK.Devuelve el objeto solicitado</response>
        ///   <response code="400">BadRequest.No se cumple con validaciòn del modelo</response>
        ///   <response code="500">InternalServerError. Se ha presentado un error</response>
        [HttpPut]

        public async Task<IHttpActionResult> Edit(int id, InstructorDTO instructorDTO)
        {
            try
            {
                if (id != instructorDTO.ID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var instructor = await instructorRepository.GetById(id);
                if (instructor == null)
                    return NotFound();

                // UPDATE FIELD
                instructor.LastName = instructorDTO.LastName;
                instructor.FirstMidName = instructorDTO.FirstMidName;
                instructor.HireDate = instructorDTO.HireDate;

                //UPDATE  ALL
                //student = mapper.Map<Student>(studentDTO);

                await instructorRepository.Update(instructor);


                return Ok(instructorDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Eliminar un instructor
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


                var instructor = await instructorRepository.GetById(id);
                if (instructor == null)
                    return NotFound();

                //if (context.Enrollment.Any(x => x.StudentID == id))
                //     throw new Exception("Dependencies");

                await instructorRepository.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


    }
}





