using System;
using System.Collections.Generic;
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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Registration.Entities;
using Registration.Enum;
using Registration.Helper;

namespace Registration
{
    public class AddTeacherModel : PageModel
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        string layoutImagePath = String.Empty;
        private IHostingEnvironment _hostingEnvironment;
        private readonly PrincetonhiveContext _context;
        string schoollogo;
        private ILogger<LoginModel> _logger;

        public AddTeacherModel(IConfiguration config, IHostingEnvironment hostingEnvironment, PrincetonhiveContext context, ILogger<LoginModel> logger)
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
        public int TeacherId { get; set; }
        [BindProperty]
        public string ClassName { get; set; }
        [BindProperty]
        public string ClassDescription { get; set; }
        [BindProperty]
        public int Schoolstate1 { get; set; }
        [BindProperty]
        public int SchoolCountry1 { get; set; }

        public List<TblRegistration> Schools { get; set; }

        public IList<TblStateMaster> States { get; private set; }

        public IList<TblCountryMaster> Countrys { get; private set; }

        public void OnGet()
        {
            ViewData["basePath"] = _configuration["base_Url"];
            Schools = (from a in _context.TblRegistration
                       where a.SchoolName != null && a.SchoolIdBelouga != null
                       select new TblRegistration { SchoolName = a.SchoolName }).Distinct().ToList();

            States = (from a in _context.TblStateMaster
                      select new TblStateMaster { Stateid = a.Stateid, StateName = a.StateName }).ToList();

            Countrys = (from a in _context.TblCountryMaster
                        select new TblCountryMaster { Countryid = a.Countryid, CountryName = a.CountryName }).ToList();

        }

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            try
            {
                var princetonhive = new PrincetonhiveContext();
                string checkusername = (from a in _context.TblRegistration
                                        where a.Username == TeacherUserName
                                        select a.Username).FirstOrDefault();

                string checkemail = (from a in _context.TblStudentRegistration
                                     where a.Email == TeacherEmail
                                     select a.Email).FirstOrDefault();
                if (checkusername == null)
                {
                    if (checkemail == null)
                    {
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
                                    Type = "Teacher",
                                    FirstName = TeacherFirstName,
                                    LastName = TeacherLastName,
                                    DisplayName = TeacherDisplayName,
                                    Gender = TeacherGender,
                                    Class = TeacherClass,
                                    Session = TeacherSession,
                                    Email = TeacherEmail,
                                    Username = TeacherUserName,
                                    SchoolName = SchoolNameExisting,
                                    SchoolEmail = SchoolEmail,
                                    Address = SchoolAddress,
                                    City = SchoolCity,
                                    State = Schoolstate1,
                                    Country = SchoolCountry1,
                                    PostalCode = SchoolPostalCode,
                                    SchoolPhone = SchoolPhoneNo,
                                    SchoolLogo = schoollogo,
                                    Status = "Active",
                                    VendorStatus = "Inactive"

                                };
                                princetonhive.TblRegistration.Add(teacher);
                                princetonhive.SaveChanges();

                                var teacherclass = new Tblclassmap()
                                {
                                    TeacheId = Convert.ToString(teacher.RegistrationId),
                                    ClassName = TeacherClass,
                                    SchoolName = SchoolName
                                };
                                princetonhive.Tblclassmap.Add(teacherclass);
                                princetonhive.SaveChanges();

                                var id = teacher.RegistrationId;
                                //var response = await _client.GetAsync("http://103.118.157.29:8080/Users/Registration_Teacher/" + id);
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
                                _logger.LogInformation(LoggingEvents.InsertItem, LoggingEvents.InsertItem + ":UserName ({0}) has inserted successfully on datetime({1})", TeacherUserName, DateTime.UtcNow.AddHours(5.30));
                                await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='AddTeacher';</script>");
                                // return Page();
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
                                    Type = "Teacher",
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
                                    SchoolLogo = schoollogo,
                                    Status = "Active",
                                    VendorStatus = "Inactive"

                                };
                                princetonhive.TblRegistration.Add(std);
                                princetonhive.SaveChanges();

                                var teacherclass = new Tblclassmap()
                                {
                                    TeacheId = Convert.ToString(std.RegistrationId),
                                    ClassName = TeacherClass,
                                    SchoolName = SchoolName
                                };
                                princetonhive.Tblclassmap.Add(teacherclass);
                                princetonhive.SaveChanges();

                                var id = std.RegistrationId;
                                //var response = await _client.GetAsync("http://103.118.157.29:8080/Users/Registration_Teacher/" + id);
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
                                _logger.LogInformation(LoggingEvents.InsertItem, LoggingEvents.InsertItem + ":UserName ({0}) has inserted successfully on datetime({1})", TeacherUserName, DateTime.UtcNow.AddHours(5.30));
                                await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='AddTeacher';</script>");
                                //  return Page();
                            }
                        }
                        else if (savemore == "Save.")
                        {

                            var classes = new Tblclassmap()
                            {
                                TeacheId = Convert.ToString(TeacherId),
                                ClassName = ClassName,
                                SchoolName = StudentSchool

                            };
                            princetonhive.Tblclassmap.Add(classes);
                            princetonhive.SaveChanges();

                            _logger.LogInformation(LoggingEvents.InsertItem, LoggingEvents.InsertItem + ":ClassName ({0}) has inserted successfully on datetime({1})", ClassName, DateTime.UtcNow.AddHours(5.30));
                            await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='AddTeacher';</script>");
                            //var classname = (from a in _context.TblRegistration
                            //                 where a.SchoolName == StudentSchool && a.RegistrationId == TeacherId
                            //                 select new  {  a.Class }).SingleOrDefault();
                            //if(classname==null)
                            //{
                            //    var classes = new TblRegistration()
                            //    {


                            //    };
                            //    princetonhive.TblRegistration.Add(classes);
                            //    princetonhive.SaveChanges();
                            //    var id = classes.RegistrationId;
                            //    //var response = await _client.GetAsync("http://103.118.157.29:8080//Users/Registration_Student/" + id);
                            //    //var keyPerosnData = response.Content.ReadAsStringAsync().Result;
                            //    ////   var result = JsonConvert.DeserializeObject<ProductLineViewRazorModal>(keyPerosnData);
                            //    //var content = JsonConvert.DeserializeObject<ResponseViewModel>(keyPerosnData);
                            //    //if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                            //    //{
                            //    //    if (content.data != null)
                            //    //    {
                            //    //        //  ViewData["ProductLine"] = result.data;
                            //    //    }
                            //    //}

                            //    await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='Registration';</script>");
                            //    return Page();
                            //}
                            //else
                            //{
                            //    var classes = new TblRegistration()
                            //    {


                            //    };
                            //    princetonhive.TblRegistration.Add(classes);
                            //    princetonhive.SaveChanges();
                            //    var id = classes.RegistrationId;
                            //    //var response = await _client.GetAsync("http://103.118.157.29:8080//Users/Registration_Student/" + id);
                            //    //var keyPerosnData = response.Content.ReadAsStringAsync().Result;
                            //    ////   var result = JsonConvert.DeserializeObject<ProductLineViewRazorModal>(keyPerosnData);
                            //    //var content = JsonConvert.DeserializeObject<ResponseViewModel>(keyPerosnData);
                            //    //if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                            //    //{
                            //    //    if (content.data != null)
                            //    //    {
                            //    //        //  ViewData["ProductLine"] = result.data;
                            //    //    }
                            //    //}

                            //    await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='Registration';</script>");
                            //    return Page();
                            //}

                        }
                    }
                    else
                    {
                        _logger.LogWarning(LoggingEvents.EmailFound, LoggingEvents.EmailFound + ":Email ({0}) has already Exists on datetime({1})", TeacherEmail, DateTime.UtcNow.AddHours(5.30));
                        await Response.WriteAsync("<script language='javascript'>window.alert('Email Already Successfully');window.location='AddTeacher';</script>");
                    }
                }

                else
                {
                    _logger.LogWarning(LoggingEvents.UserNameFound, LoggingEvents.UserNameFound + ":UserName ({0}) has already Exists on datetime({1})", TeacherUserName, DateTime.UtcNow.AddHours(5.30));
                    await Response.WriteAsync("<script language='javascript'>window.alert('UserName Already Exists');window.location='AddTeacher';</script>");
                }
            }
            catch(Exception ex)
            {
                _logger.LogInformation(LoggingEvents.SomethingWentWrong, LoggingEvents.SomethingWentWrong +":"+ ex.Message, TeacherUserName, DateTime.UtcNow.AddHours(5.30));
                await Response.WriteAsync("<script language='javascript'>window.alert('Something Went Wrong.');window.location='Login';</script>");
                throw ex;
            }
            return Page();
        }
    }
}