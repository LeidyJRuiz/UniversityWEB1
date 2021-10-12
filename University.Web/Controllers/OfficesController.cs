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
    public class OfficesController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        // GET: Offices

        public async Task<ActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<OfficeDTO>>("http://localhost/University.API/",
                "api/Offices",
                null, ApiService.Method.Get);

            var offices = (List<OfficeDTO>)responseDTO.Data;

            ViewData["offices"] = new SelectList(offices, "InstructorID", "Location");



            return View(offices);
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

            return View(new OfficeDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(OfficeDTO officeDTO)
        {
            await LoadData();


            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<OfficeDTO>("http://localhost/University.API/",
                    "api/Offices",
                    officeDTO, ApiService.Method.Post);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(officeDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            await LoadData();


            var responseDTO = await apiService.RequestAPI<OfficeDTO>("http://localhost/University.API/",
                "api/Offices/" + id,
                null, ApiService.Method.Get);

            var office = (OfficeDTO)responseDTO.Data;

            return View(office);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OfficeDTO officeDTO)
        {
            await LoadData();

            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<OfficeDTO>("http://localhost/University.API/",
                    "api/Offices/" + officeDTO.InstructorID,
                    officeDTO, ApiService.Method.Put);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(officeDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            var responseDTO = await apiService.RequestAPI<OfficeDTO>("http://localhost/University.API/",
                "api/Offices/" + id,
                null, ApiService.Method.Delete);


            return RedirectToAction(nameof(Index));


        }
    }
}