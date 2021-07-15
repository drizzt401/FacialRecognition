using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacialRecognition.UI.Controllers
{
    public class FacialRecognitionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
