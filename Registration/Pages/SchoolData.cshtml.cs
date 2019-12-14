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
    public class SchoolDataModel : PageModel
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        private IHostingEnvironment _hostingEnvironment;
        private readonly PrincetonhiveContext _context;

        public SchoolDataModel(IConfiguration config, IHostingEnvironment hostingEnvironment, PrincetonhiveContext context)
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

        public List<TblRegistration> SchoolData { get; set; }

        public void OnGet()
        {
            //string DisplayName = HttpContext.Session.GetString("DisplayName");
            //if (DisplayName == "MasterAdmin")
            //{
                SchoolData = (from a in _context.TblRegistration
                              where a.SchoolName!=null
                               select new TblRegistration { SchoolName = a.SchoolName, SchoolEmail = a.SchoolEmail, City = a.City, Address = a.Address }).Distinct().ToList();

            //}
            //else
            //{
            //    SchoolId = HttpContext.Session.GetString("SchoolIdBelouga");
            //    SchoolData = (from a in _context.TblStudentRegistration
            //                   where a.SchoolCode == SchoolId
            //                   select new TblStudentRegistration { DisplayName = a.DisplayName, Gender = a.Gender, ClassName = a.ClassName, Username = a.Username, Status = a.Status }).Distinct().ToList();

            //}
        }
    }
}