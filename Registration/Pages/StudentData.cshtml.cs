using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Registration.Entities;

namespace Registration
{
    public class StudentDataModel : PageModel
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        private IHostingEnvironment _hostingEnvironment;
        private readonly PrincetonhiveContext _context;

        public StudentDataModel(IConfiguration config, IHostingEnvironment hostingEnvironment, PrincetonhiveContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _connection = _configuration["ConnectionStrings:Princetonhive"];
            _context = context;
        }

        [BindProperty]
        public string LoginUserName { get; set; }
        [BindProperty]
        public string SchoolId { get; set; }
        [BindProperty]
        public string TeacherFirstName { get; set; }
        [BindProperty]
        public string TeacherLastName { get; set; }
        [BindProperty]
        public string TeacherDisplayName { get; set; }
        [BindProperty]
        public string TeacherGender { get; set; }
        [BindProperty]
        public string TeacherClass { get; set; }
        [BindProperty]
        public string TeacherUserName { get; set; }
        [BindProperty]
        public string studentsearch { get; set; }
        [BindProperty]
        public string schoolsearch { get; set; }

        public List<TblStudentRegistration> StudentData { get; set; }

        public async Task<IActionResult> OnGetAsync(int StudentRegistrationId, string Id)
        {
            var princetonhive = new PrincetonhiveContext();
            ViewData["basePath"] = _configuration["base_Url"];
            LoginUserName = HttpContext.Session.GetString("DisplayName");
            if (LoginUserName == "MasterAdmin")
            {
                StudentData = (from a in _context.TblStudentRegistration
                               select new TblStudentRegistration {StudentRegistrationId=a.StudentRegistrationId,Email=a.Email, DisplayName = a.DisplayName,SchoolName=a.SchoolName, Gender = a.Gender,ClassName=a.ClassName, Username = a.Username, Status = a.Status }).Distinct().OrderBy(x=>x.DisplayName).ToList();
                if (StudentData.Count == 0)
                {
                    ViewData["message"] = "No Data Found";
                }
                

                if (StudentRegistrationId != null && Id != null)
                {
                    var studentstatus = princetonhive.TblStudentRegistration.Where(y => y.StudentRegistrationId == StudentRegistrationId).SingleOrDefault();
                    if (studentstatus != null)
                    {
                        if (Id == "Active")
                        {
                            studentstatus.Status = "Inactive";
                            princetonhive.SaveChanges();
                            await Response.WriteAsync("<script language='javascript'>window.alert('Status Updated Successfully...!');window.location='StudentData';</script>");
                        }
                        else
                        {
                            studentstatus.Status = "Active";
                            princetonhive.SaveChanges();
                            await Response.WriteAsync("<script language='javascript'>window.alert('Status Updated Successfully...!');window.location='StudentData';</script>");
                        }

                    }
                }
            }
            else
            {
                SchoolId = HttpContext.Session.GetString("SchoolIdBelouga");
                StudentData = (from a in _context.TblStudentRegistration
                               where a.SchoolName == SchoolId
                               select new TblStudentRegistration { StudentRegistrationId = a.StudentRegistrationId,Email=a.Email, DisplayName = a.DisplayName,SchoolName=a.SchoolName, Gender = a.Gender, ClassName = a.ClassName, Username = a.Username, Status = a.Status }).Distinct().ToList();

                if (StudentData.Count == 0)
                {
                    ViewData["message"] = "No Data Found";
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            try
            {
                if (savemore == "searchstudent")
                {

                    StudentData = (from a in _context.TblStudentRegistration
                                   where a.DisplayName == studentsearch
                                   select new TblStudentRegistration { StudentRegistrationId = a.StudentRegistrationId,Email=a.Email, DisplayName = a.DisplayName, Gender = a.Gender, ClassName = a.ClassName, Username = a.Username, Status = a.Status }).OrderBy(x=>x.DisplayName).Distinct().ToList();
                    if (StudentData.Count == 0)
                    {
                        ViewData["message"] = "No Data Found";
                    }

                }


                else
                {
                    StudentData = (from a in _context.TblStudentRegistration
                                   where a.SchoolName == schoolsearch
                                   select new TblStudentRegistration { StudentRegistrationId = a.StudentRegistrationId,Email=a.Email, DisplayName = a.DisplayName, Gender = a.Gender, ClassName = a.ClassName, Username = a.Username, Status = a.Status }).OrderBy(x => x.DisplayName).Distinct().ToList();
                    if (StudentData.Count == 0)
                    {
                        ViewData["message"] = "No Data Found";
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Page();
        }
    }
}