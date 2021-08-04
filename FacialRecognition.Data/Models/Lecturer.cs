using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacialRecognition.Data.Models
{
    public class Lecturer : ILecturer
    {
        [Key]
        public string StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Course> Courses { get; set; }
        public Department Department { get; set ; }
    }
}
