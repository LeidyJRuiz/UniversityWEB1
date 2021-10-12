using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        // GET: Departments

        public async Task<ActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<DepartmentDTO>>("http://localhost/University.API/",
                "api/Departments",
                null, ApiService.Method.Get);

            var departments = (List<DepartmentDTO>)responseDTO.Data;

            ViewData["departments"] = new SelectList(departments, "DepartmentID", "Name");



            return View(departments);
        }

        private async Task LoadData()
        {
            var responseDTO = await apiService.RequestAPI<List<InstructorDTO>>("http://localhost/University.API/", "api/Instructors", null, ApiService.Method.Get);
                var instructors = (List<InstructorDTO>)responseDTO.Data;
            ViewData["instructors"] = new SelectList(instructors, "ID", "FullName");


        }

        [HttpGet]
        public async Task<ActionResult> Create()

           
        {
            await LoadData();

            return View(new DepartmentDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(DepartmentDTO departmentDTO)
        {
            await LoadData();

            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<DepartmentDTO>("http://localhost/University.API/",
                    "api/Departments",
                    departmentDTO, ApiService.Method.Post);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(departmentDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            await LoadData();

            var responseDTO = await apiService.RequestAPI<DepartmentDTO>("http://localhost/University.API/",
                "api/Departments/" + id,
                null, ApiService.Method.Get);

            var department = (DepartmentDTO)responseDTO.Data;

            return View(department);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DepartmentDTO departmentDTO)
        {
            await LoadData();

            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<DepartmentDTO>("http://localhost/University.API/",
                    "api/Departments/" + departmentDTO.DepartmentID,
                    departmentDTO, ApiService.Method.Put);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(departmentDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            var responseDTO = await apiService.RequestAPI<DepartmentDTO>("http://localhost/University.API/",
                "api/Departments/" + id,
                null, ApiService.Method.Delete);


            return RedirectToAction(nameof(Index));


        }
    }
}
