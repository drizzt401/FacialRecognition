using FacialRecognition.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacialRecognition.UI.Models.ViewModels
{
    public class FacialRecognitionViewModel
    {
        public List<string> CourseOptions { get; set; }
        public SelectList CourseList { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
