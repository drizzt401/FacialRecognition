using System.Collections.Generic;

namespace FacialRecognition.Data.Models
{
    public interface IStudent : IAppUser
    {
        ICollection<Course> Courses { get; set; }
        string RegistrationNumber { get; set; }
    }
}