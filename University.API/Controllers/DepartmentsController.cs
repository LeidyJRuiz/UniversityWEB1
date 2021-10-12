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
    [RoutePrefix("api/Departments")]
    public class DepartmentsController : ApiController

    {

        private readonly IMapper mapper;
        private readonly DepartmentRepository departmentRepository = new DepartmentRepository(new UniversityEntities());

        public DepartmentsController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        /// <summary>
        /// Bùsqueda de todos los departamentos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            var departments = await departmentRepository.GetAll();
            var departmentsDTO = departments.Select(x => mapper.Map<DepartmentDTO>(x));

            return Ok(departmentsDTO);
        }
        /// <summary>
        /// bùsqueda de departamentos por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto del departamento</returns>
        /// <response code="200">OK.Devuelve el objeto solicitado</response>

        [HttpGet]
        //[Route("GetById")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var department = await departmentRepository.GetById(id);
            var departmentDTO = mapper.Map<DepartmentDTO>(department);
            return Ok(departmentDTO);
        }

        /// <summary>
        /// Crear un objeto de departamento
        /// </summary>
        /// <param name="departmentDTO">Objeto del departamento</param>
        /// <returns>Objeto del departamento</returns>
        ///  <response code="200">OK.Devuelve el objeto solicitado</response>
        ///   <response code="400">BadRequest.No se cumple con validaciòn del modelo</response>
        ///   <response code="500">InternalServerError. Se ha presentado un error</response>
        [HttpPost]

        public async Task<IHttpActionResult> Create(DepartmentDTO departmentDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var department = mapper.Map<Department>(departmentDTO);
                department = await departmentRepository.Insert(department);



                departmentDTO.DepartmentID = department.DepartmentID;
                return Ok(departmentDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Editar un departamento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departmentDTO"></param>
        /// <returns></returns>
        ///  <response code="200">OK.Devuelve el objeto solicitado</response>
        ///   <response code="400">BadRequest.No se cumple con validaciòn del modelo</response>
        ///   <response code="500">InternalServerError. Se ha presentado un error</response>
        [HttpPut]

        public async Task<IHttpActionResult> Edit(int id, DepartmentDTO departmentDTO)
        {
            try
            {
                if (id != departmentDTO.DepartmentID)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var department = await departmentRepository.GetById(id);
                if (department == null)
                    return NotFound();

                // UPDATE FIELD
                department.Name = departmentDTO.Name;
                department.Budget = (decimal?)departmentDTO.Budget;
                department.StartDate = departmentDTO.StartDate;
                department.InstructorID = departmentDTO.InstructorID;

                //UPDATE  ALL
                //student = mapper.Map<Student>(studentDTO);

                await departmentRepository.Update(department);


                return Ok(departmentDTO);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Eliminar un departamento
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


                var department = await departmentRepository.GetById(id);
                if (department == null)
                    return NotFound();

                //if (context.Enrollment.Any(x => x.StudentID == id))
                //     throw new Exception("Dependencies");

                await departmentRepository.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


    }
}





