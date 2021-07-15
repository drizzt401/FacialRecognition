using Microsoft.AspNetCore.Identity;

namespace FacialRecognition.Data.Models
{
    public interface IAppUser 
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}