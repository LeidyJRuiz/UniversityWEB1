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
    public class CoursesController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        // GET: Courses

        public async Task<ActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<CourseDTO>>("http://localhost/University.API/",
                "api/Courses",
                null, ApiService.Method.Get);

            var courses = (List<CourseDTO>)responseDTO.Data;

            ViewData["courses"] = new SelectList(courses, "CourseID", "Title");



            return View(courses);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CourseDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<CourseDTO>("http://localhost/University.API/",
                    "api/Courses",
                    courseDTO, ApiService.Method.Post);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(courseDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {

            var responseDTO = await apiService.RequestAPI<CourseDTO>("http://localhost/University.API/",
                "api/Courses/" + id,
                null, ApiService.Method.Get);

            var course = (CourseDTO)responseDTO.Data;

            return View(course);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(ModelState);

                var responseDTO = await apiService.RequestAPI<CourseDTO>("http://localhost/University.API/",
                    "api/Courses/" + courseDTO.CourseID,
                    courseDTO, ApiService.Method.Put);

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(courseDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            var responseDTO = await apiService.RequestAPI<CourseDTO>("http://localhost/University.API/",
                "api/Courses/" + id,
                null, ApiService.Method.Delete);


            return RedirectToAction(nameof(Index));


        }
    }
}