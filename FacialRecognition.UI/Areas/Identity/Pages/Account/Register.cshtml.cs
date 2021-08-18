using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FacialRecognition.Data.Models;
using FacialRecognition.Data.Services;
using FacialRecognition.UI.Models.Roles;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace FacialRecognition.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IFacialRecognitionService _recognitionService;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IFacialRecognitionService recognitionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _recognitionService = recognitionService;
        }
        public List<SelectListItem> Departments { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            public IFormFile Image { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            [Required(ErrorMessage = "Please select a Role")]
            public bool isLecturer { get; set; }

            public Department Department { get; set; }
            public string ID { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Departments = _recognitionService.GetDepartments().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.DepartmentID
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string Department, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                byte[] image = null;
                var user = new AppUser { UserName = Input.ID , FirstName=Input.FirstName, LastName=Input.LastName, Id = Input.ID, Email=null };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    if (Input.isLecturer)
                    {
                        Department department = _recognitionService.GetDepartments().Where(d => d.DepartmentID == Department).SingleOrDefault();
                        await _recognitionService.AddLecturer(new Lecturer { FirstName = Input.FirstName, LastName = Input.LastName, StaffID = Input.ID, Department = department });
                        await _userManager.AddToRoleAsync(user, Roles.Lecturer.ToString());
                        await _userManager.UpdateAsync(user);
                        await _signInManager.SignInAsync(user, false);
                    }
                    else
                    {
                        Department department = _recognitionService.GetDepartments().Where(d => d.DepartmentID == Department).SingleOrDefault();
                        if(Input.Image != null)
                        {
                            if (Input.Image.Length > 0)
                            {

                                byte[] imageBytes = null;
                                using (var fs1 = Input.Image.OpenReadStream())
                                using (var ms1 = new MemoryStream())
                                {
                                    fs1.CopyTo(ms1);
                                    imageBytes = ms1.ToArray();
                                }
                                
                              image = imageBytes;

                            }
                        }
                        await _recognitionService.AddStudent(new Student { FirstName = Input.FirstName, LastName = Input.LastName, RegistrationNumber = Input.ID, Department = department, StudentImage= image});
                        await _userManager.AddToRoleAsync(user, Roles.Student.ToString());
                        await _userManager.UpdateAsync(user);
                        await _signInManager.SignInAsync(user, false);
                    }

                    returnUrl = "~/Index";
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
