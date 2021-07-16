using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacialRecognition.Data.Models;
using FacialRecognition.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacialRecognition.UI.Pages
{
    public class ValidateStudentModel : PageModel
    {
        private readonly IFacialRecognitionService facialRecognitionService;
        private readonly UserManager<AppUser> userManager;

        public ValidateStudentModel(IFacialRecognitionService _facialRecognitionService, UserManager<AppUser> _userManager)
        {
            facialRecognitionService = _facialRecognitionService;
            userManager = _userManager;
        }
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<SelectListItem> CourseOptions { get; set; }
        public void OnGet()
        {
            var user = userManager.GetUserAsync(User);
            CourseOptions = facialRecognitionService.GetCoursesByLecturer(user.Result.Id).Select(a => new SelectListItem { 
                Value = a.CourseCode.ToString(),
                Text = $"{a.CourseCode}:  {a.CourseTitle}"
            }).ToList();
        }

        public async Task<IActionResult> OnGetGetCourseStudents(string courseCode)
        {
            return RedirectToPage();
        }
    }
}
