using System.Collections.Generic;

namespace FacialRecognition.Data.Models
{
    public interface ICourse
    {
        string CourseCode { get; set; }
        string CourseTitle { get; set; }
        Department Department { get; set; }
        ICollection<Lecturer> Lecturers { get; set; }
        ICollection<Student> Students { get; set; }
    }
}