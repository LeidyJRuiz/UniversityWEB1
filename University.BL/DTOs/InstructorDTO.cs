﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class InstructorDTO
    {
        [Required(ErrorMessage = "The ID is required")]
        public int ID { get; set; }

        [Required(ErrorMessage = "The LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The FirstMidName is required")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "The HireDate is required")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "The FullName is required")]
        [JsonProperty("Full Name")]
        public string FullName { get; set; }

    }
}
