using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacialRecognition.Data.Models
{
    public class AppUser :  IdentityUser, IAppUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
