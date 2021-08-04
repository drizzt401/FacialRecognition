using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacialRecognition.Data.Models
{
    public class Course : ICourse
    {
        [Key]
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
        public ICollection<Student> Students { get; set; }
        public Department Department { get; set; }

    }
}
