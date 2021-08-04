using System.Collections.Generic;

namespace FacialRecognition.Data.Models
{
    public interface IStudent : IAppUser
    {
        public byte[] StudentImage { get; set; }
        ICollection<Course> Courses { get; set; }
        string RegistrationNumber { get; set; }
        Department Department { get; set; }
    }
}