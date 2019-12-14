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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Registration.Entities;
using Registration.Enum;
using Registration.Helper;

namespace Registration
{
    public class AddStudentModel : PageModel
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        string layoutImagePath = String.Empty;
        private IHostingEnvironment _hostingEnvironment;
        private readonly PrincetonhiveContext _context;
        string schoollogo;
        private ILogger<LoginModel> _logger;

        public AddStudentModel(IConfiguration config, IHostingEnvironment hostingEnvironment, PrincetonhiveContext context, ILogger<LoginModel> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _connection = _configuration["ConnectionStrings:Princetonhive"];
            _context = context;
            _logger = logger;
        }

       [BindProperty]
        public string StudentSchool { get; set; }
        [BindProperty]
        public string StudentFirstName { get; set; }
        [BindProperty]
        public string StudentLastName { get; set; }
        [BindProperty]
        public string StudentDisplayName { get; set; }
        [BindProperty]
        public string StudentGender { get; set; }
        [BindProperty]
        public string StudentClass { get; set; }
        [BindProperty]
        public string StudentSession { get; set; }
        [BindProperty]
        public string StudentEmail { get; set; }
        [BindProperty]
        public string StudentUserName { get; set; }
        public List<TblRegistration> Schools { get; set; }

        public void OnGet()
        {
            ViewData["basePath"] = _configuration["base_Url"];
            Schools = (from a in _context.TblRegistration
                       where a.SchoolName != null
                       select new TblRegistration { SchoolName = a.SchoolName}).Distinct().ToList();

        }

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            try
            {

                var princetonhive = new PrincetonhiveContext();

                string checkusername = (from a in _context.TblStudentRegistration
                                                  where a.Username == StudentUserName
                                                  select a.Username).FirstOrDefault();

                string checkemail = (from a in _context.TblStudentRegistration
                                        where a.Email == StudentEmail
                                        select a.Email).FirstOrDefault();
                if (checkusername==null)
                {
                    if (checkemail == null)
                    {
                        var studentd = new TblStudentRegistration()
                        {
                            Type = "Student",
                            SchoolName = StudentSchool,
                            FirstName = StudentFirstName,
                            LastName = StudentLastName,
                            DisplayName = StudentDisplayName,
                            Gender = StudentGender,
                            ClassName = StudentClass,
                            Session = StudentSession,
                            Email = StudentEmail,
                            Username = StudentUserName,
                            Status = "Active"

                        };
                        princetonhive.TblStudentRegistration.Add(studentd);
                        princetonhive.SaveChanges();
                        var id = studentd.StudentRegistrationId;
                        //var response = await _client.GetAsync("http://103.118.157.29:8080//Users/Registration_Student/" + id);
                        //var keyPerosnData = response.Content.ReadAsStringAsync().Result;
                        ////   var result = JsonConvert.DeserializeObject<ProductLineViewRazorModal>(keyPerosnData);
                        //var content = JsonConvert.DeserializeObject<ResponseViewModel>(keyPerosnData);
                        //if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                        //{
                        //    if (content.data != null)
                        //    {
                        //        //  ViewData["ProductLine"] = result.data;
                        //    }
                        //}
                        _logger.LogInformation(LoggingEvents.InsertItem, LoggingEvents.InsertItem+":UserName ({0}) has inserted successfully on datetime({1})", StudentUserName, DateTime.UtcNow.AddHours(5.30));
                        await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='AddStudent';</script>");
                    }
                    else
                    {
                        _logger.LogWarning(LoggingEvents.EmailFound, LoggingEvents.EmailFound+":Email ({0}) has already Exists on datetime({1})", StudentEmail, DateTime.UtcNow.AddHours(5.30));
                        await Response.WriteAsync("<script language='javascript'>window.alert('Email Already Successfully');window.location='AddStudent';</script>");
                    }
                }

                else
                {
                    _logger.LogWarning(LoggingEvents.UserNameFound, LoggingEvents.UserNameFound+":UserName ({0}) has already Exists on datetime({1})", StudentUserName, DateTime.UtcNow.AddHours(5.30));
                    await Response.WriteAsync("<script language='javascript'>window.alert('UserName Already Exists');window.location='AddStudent';</script>");
                }
                
            }

            catch(Exception ex)
            {
                _logger.LogInformation(LoggingEvents.SomethingWentWrong, LoggingEvents.SomethingWentWrong +":"+ex.Message, StudentUserName, DateTime.UtcNow.AddHours(5.30));
                await Response.WriteAsync("<script language='javascript'>window.alert('Something Went Wrong.');window.location='Login';</script>");
                throw ex;
            }


            return Page();
        }
    }
}