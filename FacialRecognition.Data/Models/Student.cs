using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacialRecognition.Data.Models
{
    public class Student : IStudent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Key]
        public string RegistrationNumber { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
