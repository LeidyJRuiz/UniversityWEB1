using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class DepartmentDTO
    {
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Budget is required")]
        public double Budget { get; set; }

        [Required(ErrorMessage = "The StartDate is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The InstructorID is required")]
        public int InstructorID { get; set; }

        public InstructorDTO Instructor { get; set; }




    }
}
