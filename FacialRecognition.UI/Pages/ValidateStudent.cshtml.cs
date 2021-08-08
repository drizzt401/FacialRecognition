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
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;

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
        [BindProperty(SupportsGet =true)]
        public List<Student> Students { get; set; }
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
            Course course = facialRecognitionService.GetCourses().Where(c => c.CourseCode == courseCode).SingleOrDefault();
            var students = facialRecognitionService.GetCourseStudents(course);
            List<Datum> data = new List<Datum>();
            AjaxResponse response = new AjaxResponse();
            foreach(Student student in students)
            {
                data.Add(new Datum
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    RegNo = student.RegistrationNumber,
                    Department = student.Department.Name
                });
            }

           var str = JsonConvert.SerializeObject(data);
            var res = JsonConvert.DeserializeObject<Datum[]>(str);
            response.data = res;
            return  new JsonResult(response.data); 
        }

        public async Task OnPostUploadImage([FromForm] IFormFile picture)
        {
            if (picture != null)
            {
                if (picture.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(picture.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension =".jpeg";

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    // Combines two strings into a path.
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")).Root + $@"\{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        picture.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }
        }
    }
}
