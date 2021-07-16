using FacialRecognition.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRecognition.Data.Services
{
    public class FacialRecognitionService : IFacialRecognitionService
    {
        private readonly AppDbContext _context;

        public FacialRecognitionService(AppDbContext context)
        {
            _context = context;
        }
        public List<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }

        public List<Student> GetStudents()
        {
            return _context.Students.Include("Courses").ToList();
        }

        public List<Lecturer> GetLecturers()
        {
            return _context.Lecturers.Include("Courses").ToList();
        }

        public List<Course> GetCoursesByLecturer(string StaffID)
        {
            return _context.Courses.Where(c => c.Lecturers.Any(l => l.StaffID == StaffID)).Include(c => c.Lecturers.Where(l => l.StaffID == StaffID)).ToList();
        }

        public List<Course> GetCoursesByStudent(string RegistrationNumber)
        {
            return _context.Courses.Where(c => c.Students.Any(s => s.RegistrationNumber == RegistrationNumber)).Include(c => c.Students.Where(s => s.RegistrationNumber == RegistrationNumber)).ToList();
        }

        public async Task<bool> AddLecturer(Lecturer lecturer)
        {
            try
            {
                await _context.Lecturers.AddAsync(lecturer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> AddLecturerCourse(List<Course> courses, AppUser user)
        {
            try
            {
                foreach (Course selectedCourse in courses)
                {
                    var course = _context.Courses.Include(l => l.Lecturers).Single(c => c.CourseCode == selectedCourse.CourseCode);
                    var Lecturer = _context.Lecturers.Single(l => l.StaffID == user.Id);
                    course.Lecturers.Add(Lecturer);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RemoveLecturerCourse(Course selectedCourse, AppUser user)
        {
            try
            {
                var course = _context.Courses.Include(p => p.Lecturers).First(c => c.CourseCode == selectedCourse.CourseCode);
                var lecturer = course.Lecturers.Single(x => x.StaffID == user.Id);
                course.Lecturers.Remove(lecturer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddStudent(Student student)
        {
            try
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
