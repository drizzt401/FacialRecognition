using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacialRecognition.Data.Models
{
    public class Department : IDepartment
    {
        [Key]
        public string DepartmentID { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}
