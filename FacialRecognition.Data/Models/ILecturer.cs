using System.Collections.Generic;

namespace FacialRecognition.Data.Models
{
    public interface ILecturer : IAppUser
    {
        ICollection<Course> Courses { get; set; }
        string StaffID { get; set; }
        public Department Department { get; set; }
    }
}