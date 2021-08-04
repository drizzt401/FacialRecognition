using System.Collections.Generic;

namespace FacialRecognition.Data.Models
{
    public interface IDepartment
    {
        ICollection<Course> Courses { get; set; }
        string DepartmentID { get; set; }
        string Name { get; set; }
        ICollection<Student> Students { get; set; }
    }
}