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
    [RoutePrefix("api/OfficeAssignments")]
    public class OfficesController : ApiController

    {

        private readonly IMapper mapper;
        private readonly OfficeRepository officeRepository = new OfficeRepository(new UniversityEntities());


        public OfficesController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        /// <summary>
        /// Bùsqueda de todos las oficinas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var offices = await officeRepository.GetAll();
            var officesDTO = offices.Select(x => mapper.Map<OfficeDTO>(x));

            return Ok(officesDTO);
        }

        /// <summary>
        /// bùsqueda de oficina por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto de la oficina</returns>
        /// <response code="200">OK.Devuelve el objeto solicitado</response>

        [HttpGet]
        //[Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var office = await officeRepository.GetById(id);
            var officeDTO = mapper.Map<OfficeDTO>(office);
            return Ok(officeDTO);
        }

        /// <summary>
        /// Crear un objeto de oficina
        /// </summary>
        /// <param name="officeDTO">Objeto de la oficina</param>
        /// <returns>Objeto de la oficina</returns>
        ///  <response code="200">OK.Devuelve el objeto solicitado</response>
        ///   <response code="400">BadRequest.No se cumple con validaciòn del modelo</response>
        ///   <response code="500">InternalServerError. Se ha presentado un error</response>
        [HttpPost]

        public async Task<IHttpActionResult> Create(OfficeDTO officeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var office = mapper.Map<OfficeAssignment>(officeDTO);
                office = await officeRepository.Insert(office);



                officeDTO.InstructorID = office.InstructorID;
                return Ok(officeDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Editar una oficina
        /// </summary>
        /// <param name="id"></param>
        /// <param name="officeDTO"></param>
        /// <returns></returns>
        ///  <response code="200">OK.Devuelve el objeto solicitado</response>
        ///   <response code="400">BadRequest.No se cumple con validaciòn del modelo</response>
        ///   <response code="500">InternalServerError. Se ha presentado un error</response>
        [HttpPut]

        public async Task<IHttpActionResult> Edit(int id, OfficeDTO officeDTO)
        {
            try
            {
                if (id != officeDTO.InstructorID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var office = await officeRepository.GetById(id);
                if (office == null)
                    return NotFound();

                // UPDATE FIELD
                office.InstructorID = officeDTO.InstructorID;
                office.Location = officeDTO.Location;
               

                //UPDATE  ALL
                //student = mapper.Map<Student>(studentDTO);

                await officeRepository.Update(office);


                return Ok(officeDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Eliminar una oficina
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


                var office = await officeRepository.GetById(id);
                if (office == null)
                    return NotFound();

                //if (context.Enrollment.Any(x => x.StudentID == id))
                //     throw new Exception("Dependencies");

                await officeRepository.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


    }
}





