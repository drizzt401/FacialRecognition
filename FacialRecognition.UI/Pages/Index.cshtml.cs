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
    public class IndexModel : PageModel
    {
        private readonly IFacialRecognitionService facialRecognitionService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        public IndexModel(IFacialRecognitionService _facialRecognitionService, UserManager<AppUser> _userManager)
        {
            facialRecognitionService = _facialRecognitionService;
            userManager = _userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();
        public class InputModel
        {
            public List<Course> Courses { get; set; } = new List<Course>();
            public List<Student> Students { get; set; } = new List<Student>();


            public List<SelectListItem> CourseOptions { get; set; }
        }
        public void OnGet()
        {
            if (User.IsInRole("Lecturer"))
            {
                var user = userManager.GetUserAsync(User);
                 Input.Courses= facialRecognitionService.GetCoursesByLecturer(user.Result.Id);
            }
            if (User.IsInRole("Student"))
            {
                var user = userManager.GetUserAsync(User);
                Input.Courses = facialRecognitionService.GetCoursesByStudent(user.Result.Id);
            }

            Input.CourseOptions = facialRecognitionService.GetCourses().Select(a => new SelectListItem {
                Value = a.CourseCode.ToString(),
                Text= a.CourseTitle
            }).ToList();

        }

        public IActionResult OnPostAddCourse()
        {
            foreach(var course in Input.CourseOptions)
            {

            }
            return LocalRedirect("/Index");
        }
    }
}