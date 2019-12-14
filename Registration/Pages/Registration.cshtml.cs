using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using Registration.Entities;
using Registration.Enum;
using Registration.Helper;

namespace Registration.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        string layoutImagePath = String.Empty;
        private IHostingEnvironment _hostingEnvironment;
        private readonly PrincetonhiveContext _context;
        string schoollogo;

        public RegistrationModel(IConfiguration config, IHostingEnvironment hostingEnvironment, PrincetonhiveContext context)
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
        public string TeacherSession { get; set; }
        [BindProperty]
        public string TeacherEmail { get; set; }
        [BindProperty]
        public string TeacherUserName { get; set; }
        [BindProperty]
        public string School { get; set; }
        [BindProperty]
        public string SchoolName { get; set; }
        [BindProperty]
        public string SchoolEmail { get; set; }
        [BindProperty]
        public string SchoolAddress { get; set; }
        [BindProperty]
        public string SchoolCity { get; set; }
        [BindProperty]
        public int SchoolState { get; set; }
        [BindProperty]
        public int SchoolCountry { get; set; }
        [BindProperty]
        public string SchoolPostalCode { get; set; }
        [BindProperty]
        public string SchoolPhoneNo { get; set; }
        [BindProperty]
        public string SchoolNameExisting { get; set; }
        [BindProperty]
        public IFormFile SchoolLogo { get; set; }
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

        public IList<TblStateMaster> States { get; private set; }

        public IList<TblCountryMaster> Countrys { get; private set; }

        public IList<TblRegistration> Class { get; private set; }

        public void OnGet()
        {
            
           Schools = (from a in _context.TblRegistration
                        where a.SchoolName != null && a.SchoolIdBelouga != null
                        select new TblRegistration { SchoolName = a.SchoolName, SchoolIdBelouga = a.SchoolIdBelouga }).Distinct().ToList();

            States = (from a in _context.TblStateMaster
                        select new TblStateMaster { Stateid = a.Stateid, StateName = a.StateName }).ToList();

            Countrys = (from a in _context.TblCountryMaster
                          select new TblCountryMaster { Countryid = a.Countryid, CountryName = a.CountryName }).ToList();

            Class = (from a in _context.TblRegistration
                            select new TblRegistration { ClassIdBelouga = a.ClassIdBelouga, Class = a.Class }).ToList();

        }

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            var princetonhive = new PrincetonhiveContext();
            if (savemore == "Save")
            {
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (School == "Existing")
                {
                    var teacher = new TblRegistration()
                    {
                        Type ="Teacher",
                        FirstName = TeacherFirstName,
                        LastName = TeacherLastName,
                        DisplayName = TeacherDisplayName,
                        Gender = TeacherGender,
                        Class = TeacherClass,
                        Session = TeacherSession,
                        Email = TeacherEmail,
                        Username = TeacherUserName,
                        SchoolIdBelouga = SchoolNameExisting,
                        SchoolEmail = SchoolEmail,
                        Address = SchoolAddress,
                        City = SchoolCity,
                        State = SchoolState,
                        Country = SchoolCountry,
                        PostalCode = SchoolPostalCode,
                        SchoolPhone = SchoolPhoneNo,
                        SchoolLogo = schoollogo

                    };
                    princetonhive.TblRegistration.Add(teacher);
                    princetonhive.SaveChanges();
                    var id = teacher.RegistrationId;
                    var response = await _client.GetAsync("http://103.118.157.29:8080/Users/Registration_Teacher/" + id);
                    var keyPerosnData = response.Content.ReadAsStringAsync().Result;
                 //   var result = JsonConvert.DeserializeObject<ProductLineViewRazorModal>(keyPerosnData);
                    var content = JsonConvert.DeserializeObject<ResponseViewModel>(keyPerosnData);
                    if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                    {
                        if (content.data != null)
                        {
                          //  ViewData["ProductLine"] = result.data;
                        }
                    }

                    await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='Registration';</script>");
                    return Page();
                }
                else if (School == "New")
                {
                    if (SchoolLogo != null)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(SchoolLogo.ContentDisposition).FileName.Trim('"');
                        var saveImageName = Guid.NewGuid() + "_" + fileName;
                        string fullPath = Path.Combine(newPath, saveImageName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await SchoolLogo.CopyToAsync(stream);
                        }
                        schoollogo = saveImageName;
                    }
                    else
                    {

                    }

                    var std = new TblRegistration()
                    {
                        FirstName = TeacherFirstName,
                        LastName = TeacherLastName,
                        DisplayName = TeacherDisplayName,
                        Gender = TeacherGender,
                        Class = TeacherClass,
                        Session = TeacherSession,
                        Email = TeacherEmail,
                        Username = TeacherUserName,
                        SchoolName = SchoolName,
                        SchoolEmail = SchoolEmail,
                        Address = SchoolAddress,
                        City = SchoolCity,
                        State = SchoolState,
                        Country = SchoolCountry,
                        PostalCode = SchoolPostalCode,
                        SchoolPhone = SchoolPhoneNo,
                        SchoolLogo = schoollogo

                    };
                    princetonhive.TblRegistration.Add(std);
                    princetonhive.SaveChanges();
                    var id = std.RegistrationId;
                    var response = await _client.GetAsync("http://103.118.157.29:8080/Users/Registration_Teacher/" + id);
                    var keyPerosnData = response.Content.ReadAsStringAsync().Result;
                    //   var result = JsonConvert.DeserializeObject<ProductLineViewRazorModal>(keyPerosnData);
                    var content = JsonConvert.DeserializeObject<ResponseViewModel>(keyPerosnData);
                    if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                    {
                        if (content.data != null)
                        {
                            //  ViewData["ProductLine"] = result.data;
                        }
                    }

                    await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='Registration';</script>");
                    return Page();
                }
            }
            else if(savemore== "Save.")
            {
                var studentd = new TblStudentRegistration()
                {
                    Type = "Teacher",
                    FirstName = StudentFirstName,
                    LastName = StudentLastName,
                    DisplayName = StudentDisplayName,
                    Gender = StudentGender,
                    ClassIdBelouga = StudentClass,
                    Session = StudentSession,
                    Email = StudentEmail,
                    Username = StudentUserName
                    
                };
                princetonhive.TblStudentRegistration.Add(studentd);
                princetonhive.SaveChanges();
                var id = studentd.StudentRegistrationId;
                var response = await _client.GetAsync("http://103.118.157.29:8080//Users/Registration_Student/" + id);
                var keyPerosnData = response.Content.ReadAsStringAsync().Result;
                //   var result = JsonConvert.DeserializeObject<ProductLineViewRazorModal>(keyPerosnData);
                var content = JsonConvert.DeserializeObject<ResponseViewModel>(keyPerosnData);
                if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                {
                    if (content.data != null)
                    {
                        //  ViewData["ProductLine"] = result.data;
                    }
                }

                await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='Registration';</script>");
                return Page();
            }
            return Page();
        }
    }
}