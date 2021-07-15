using FacialRecognition.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacialRecognition.Data.Services
{
    public interface IFacialRecognitionService
    {
        List<Course> GetCourses();
        List<Course> GetCoursesByLecturer(string StaffID);
        List<Course> GetCoursesByStudent(string RegistrationNumber);
        List<Lecturer> GetLecturers();
        List<Student> GetStudents();
        Task<bool> AddLecturer(Lecturer lecturer);
        Task<bool> AddStudent(Student student);
    }
}