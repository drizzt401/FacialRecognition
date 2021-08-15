using FacialRecognition.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacialRecognition.Data.Services
{
    public interface IFacialRecognitionService
    {
        List<Course> GetCourses(string  departmentID);
        List<Course> GetCourses();
        List<Course> GetCoursesByLecturer(string StaffID);
        List<Course> GetCoursesByStudent(string RegistrationNumber);
        List<Lecturer> GetLecturers();
        List<Student> GetStudents();
        List<Student> GetCourseStudents(Course course);
        Task<bool> AddLecturer(Lecturer lecturer);
        Task<bool> AddStudent(Student student);
        Task<bool> AddLecturerCourse(List<Course> courses, AppUser user);
        Task<bool> AddStudentCourse(List<Course> courses, AppUser user);
        Task<bool> RemoveLecturerCourse(Course course, AppUser user);
        List<Department> GetDepartments();

        byte[] GetStudentImage(string studentId);
    }
}