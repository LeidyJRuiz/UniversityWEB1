using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using University.BL.Models;

namespace University.BL.DTOs
{
    public class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration()
        {

            //MODEL (DB)-> DTO
            //DTO -> MODEL(DB)

            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDTO>();
                cfg.CreateMap<StudentDTO, Student>();

                cfg.CreateMap<Course, CourseDTO>();
                cfg.CreateMap<CourseDTO, Course>();

                cfg.CreateMap<Department, DepartmentDTO>();
                cfg.CreateMap<DepartmentDTO, Department>();

                cfg.CreateMap<Instructor, InstructorDTO>();
                cfg.CreateMap<InstructorDTO, Instructor>();

                cfg.CreateMap<OfficeAssignment, OfficeDTO>();
                cfg.CreateMap<OfficeDTO, OfficeAssignment>();
            });

        }

    }
}
