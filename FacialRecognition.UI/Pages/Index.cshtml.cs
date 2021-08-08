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
        private readonly UserManager<AppUser> userManager;
        private Lecturer lecturer;
        private Student student;
        private Department department;
        public IndexModel(IFacialRecognitionService _facialRecognitionService, UserManager<AppUser> _userManager)
        {
            facialRecognitionService = _facialRecognitionService;
            userManager = _userManager;
        }

        //public string imageData { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();


        public class InputModel
        {
            public List<Course> Courses { get; set; } = new List<Course>();
            public List<Student> Students { get; set; } = new List<Student>();
            public List<SelectListItem> CourseOptions { get; set; }
        }
        
        public async Task OnGet()
        {

            var user = await userManager.GetUserAsync(User);
            if (User.IsInRole("Lecturer"))
            {
                lecturer = facialRecognitionService.GetLecturers().Select(l => l).Where(l => l.StaffID == user.Id).FirstOrDefault();
                department = lecturer.Department;
                Input.Courses= facialRecognitionService.GetCoursesByLecturer(user.Id);

                Input.CourseOptions = facialRecognitionService.GetCourses(department.DepartmentID).Select(a => new SelectListItem
                {
                    Value = a.CourseCode.ToString(),
                    Text = a.CourseTitle
                }).ToList();
            }
            if (User.IsInRole("Student"))
            {
                student = facialRecognitionService.GetStudents().Where(s => s.RegistrationNumber == user.Id).FirstOrDefault();
                department = student.Department;
                Input.Courses = facialRecognitionService.GetCoursesByStudent(user.Id);
               // var imgPath = student.StudentImage;
               // imageData = @"data:image / jpeg; base64," + Convert.ToBase64String(imgPath);
                Input.CourseOptions = facialRecognitionService.GetCourses().Select(a => new SelectListItem
                {
                    Value = a.CourseCode.ToString(),
                    Text = a.CourseTitle
                }).ToList();
            }


        }

        public async Task<IActionResult> OnPostAddCourse(List<string> CourseOptions)
        {

            List<Course> selectedCourses = new List<Course>();
            AppUser user = userManager.GetUserAsync(User).Result;
            if (User.IsInRole("Lecturer"))
            {
                foreach (var selectedCourse in CourseOptions)
                {
                    var course = facialRecognitionService.GetCourses().Where(c => c.CourseCode == selectedCourse).SingleOrDefault();
                    selectedCourses.Add(course);
                    await facialRecognitionService.AddLecturerCourse(selectedCourses, user);
                }
            }
            if (User.IsInRole("Student"))
            {
                foreach (var selectedCourse in CourseOptions)
                {
                    var course = facialRecognitionService.GetCourses().Where(c => c.CourseCode == selectedCourse).SingleOrDefault();
                    selectedCourses.Add(course);
                    await facialRecognitionService.AddStudentCourse(selectedCourses, user);
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveCourse(string courseCode)
        {
            var course = facialRecognitionService.GetCourses().Where(c => c.CourseCode == courseCode).SingleOrDefault();
            AppUser user = userManager.GetUserAsync(User).Result;
            await facialRecognitionService.RemoveLecturerCourse(course, user);
            return RedirectToPage();
        }
    }
}
