using Microsoft.Extensions.Configuration;
using Registration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        private readonly PrincetonhiveContext _context;
        public RegistrationRepository(IConfiguration configuration, PrincetonhiveContext context)
        {
            _configuration = configuration;
            _connection = _configuration["ConnectionStrings:Princetonhive"];
            _context = context;
        }

        public List<TblRegistration> GetClassBySchoolId(string SchoolName)
        {
            List<TblRegistration> classes = new List<TblRegistration>();

            try
            {
                classes = (from a in _context.TblRegistration
                           where a.SchoolName == SchoolName
                           select new TblRegistration { Class = a.Class }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return classes;
        }

        public List<TblRegistration> GetSchoolDetailsBySchoolId(string SchoolName)
        {
            List<TblRegistration> schooldetails = new List<TblRegistration>();

            try
            {
                schooldetails = (from a in _context.TblRegistration
                           where a.SchoolName == SchoolName
                                 select new TblRegistration { SchoolEmail  = a.SchoolEmail, Address = a.Address,City=a.City,State=a.State,Country=a.Country,PostalCode=a.PostalCode,SchoolPhone=a.SchoolPhone }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return schooldetails;
        }

        public List<TblRegistration> GetTeacherBySchoolId(string SchoolName)
        {
            List<TblRegistration> teachers = new List<TblRegistration>();

            try
            {
                teachers = (from a in _context.TblRegistration
                           where a.SchoolName == SchoolName
                            select new TblRegistration { RegistrationId = a.RegistrationId, DisplayName = a.DisplayName }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return teachers;
        }

        public List<TblStudentRegistration> GetStudentAutoSearch(string StudentName,string UserName)
        {
            List<TblStudentRegistration> studentsData = new List<TblStudentRegistration>();
          
            try
            {
                var students = (from a in _context.TblStudentRegistration select a).ToList();

               var studentsUpper = students.Where(x => x.DisplayName.Contains(StudentName.ToUpper())).OrderBy(x=>x.DisplayName).ToList();
               var studentsLower = students.Where(x => x.DisplayName.Contains(StudentName.ToLower())).OrderBy(x => x.DisplayName).ToList();
               studentsData = studentsUpper.Concat(studentsLower).ToList();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return studentsData;
        }

        public List<TblRegistration> GetSchoolAutoSearch(string SchoolName, string UserName)
        {
            List<TblRegistration> schoolsData = new List<TblRegistration>();

            try
            {
                var schools = (from a in _context.TblRegistration where a.SchoolName != null select new TblRegistration { SchoolName = a.SchoolName }).Distinct().ToList();

                var schoolsUpper = schools.Where(x => x.SchoolName.Contains(SchoolName.ToUpper())).OrderBy(x => x.SchoolName).Distinct().ToList();
                var schoolsLower = schools.Where(x => x.SchoolName.Contains(SchoolName.ToLower())).OrderBy(x => x.SchoolName).Distinct().ToList();
                schoolsData = schoolsUpper.Concat(schoolsLower).Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return schoolsData;
        }

        public List<TblRegistration> GetTeacherAutoSearch(string TeacherName, string UserName)
        {
            List<TblRegistration> teacherData = new List<TblRegistration>();

            try
            {
                var teachers = (from a in _context.TblRegistration select a).ToList();

                var teachersUpper = teachers.Where(x => x.DisplayName.Contains(TeacherName.ToUpper())).OrderBy(x => x.DisplayName).ToList();
                var teachersLower = teachers.Where(x => x.DisplayName.Contains(TeacherName.ToLower())).OrderBy(x => x.DisplayName).ToList();
                teacherData = teachersUpper.Concat(teachersLower).Distinct().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return teacherData;
        }


    }
}
