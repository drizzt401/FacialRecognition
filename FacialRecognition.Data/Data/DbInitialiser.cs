using FacialRecognition.Data.Models;
using FacialRecognition.UI.Models.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRecognition.Data.Data
{
    public class DbInitialiser
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Courses.Any())
            {
                context.Courses.Add( new Course { CourseCode = "CSC325", CourseTitle="System Analysis and Design" });
                context.Courses.Add(new Course { CourseCode = "CSC522", CourseTitle = "Software Engineering" });
                context.Courses.Add(new Course { CourseCode = "CSC523", CourseTitle = "Computer Center Management" });
                context.Courses.Add(new Course { CourseCode = "CSC323", CourseTitle = "Principles of Compilers" });
                context.Courses.Add(new Course { CourseCode = "CSC525", CourseTitle = "Introduction To Artificial Intelligence" });
            }

            if (!context.Department.Any())
            {
                context.Department.Add(new Department { DepartmentID = "CO", Name = "Computer Science" });
                context.Department.Add(new Department { DepartmentID = "MA", Name = "Mathematics" });
                context.Department.Add(new Department { DepartmentID = "STA", Name = "Statistics" });
            }

            context.SaveChanges();
        }

        public static async Task SeedRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Lecturer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));
        }
    }

}
