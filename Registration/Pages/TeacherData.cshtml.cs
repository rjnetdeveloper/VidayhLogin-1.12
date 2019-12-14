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
    public class TeacherDataModel : PageModel
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        private IHostingEnvironment _hostingEnvironment;
        private readonly PrincetonhiveContext _context;

        public TeacherDataModel(IConfiguration config, IHostingEnvironment hostingEnvironment, PrincetonhiveContext context)
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
        public string teachersearch { get; set; }
        [BindProperty]
        public string schoolsearch { get; set; }

        public List<TblRegistration> TeacherData { get; set; }

        public async Task<IActionResult> OnGetAsync(int RegistrationId, string Id, int VRegistrationId, string VId)
        {
            var princetonhive = new PrincetonhiveContext();
            ViewData["basePath"] = _configuration["base_Url"];
            LoginUserName = HttpContext.Session.GetString("DisplayName");
            if (LoginUserName == "MasterAdmin")
            {
                TeacherData = (from a in _context.TblRegistration
                               where a.SchoolName!=null
                               select new TblRegistration {RegistrationId=a.RegistrationId,SchoolName=a.SchoolName,Email=a.Email, DisplayName = a.DisplayName, Gender = a.Gender, Class = a.Class, Username = a.Username, Status = a.Status, VendorStatus = a.VendorStatus }).Distinct().ToList();

                if (RegistrationId != null && Id != null)
                {
                    var teacherstatus = princetonhive.TblRegistration.Where(y => y.RegistrationId == RegistrationId).SingleOrDefault();
                    if (teacherstatus != null)
                    {
                        if(Id=="Active")
                        {
                            teacherstatus.Status = "Inactive";
                            princetonhive.SaveChanges();
                            await Response.WriteAsync("<script language='javascript'>window.alert('Status Updated Successfully...!');window.location='TeacherData';</script>");
                        }
                        else
                        {
                            teacherstatus.Status = "Active";
                            princetonhive.SaveChanges();
                            await Response.WriteAsync("<script language='javascript'>window.alert('Status Updated Successfully...!');window.location='TeacherData';</script>");
                        }
                        
                    }
                }

                if (VRegistrationId != null && VId != null)
                {
                    var teacherstatus = princetonhive.TblRegistration.Where(y => y.RegistrationId == VRegistrationId).SingleOrDefault();
                    if (teacherstatus != null)
                    {
                        if (VId == "Active")
                        {
                            teacherstatus.VendorStatus = "Inactive";
                            princetonhive.SaveChanges();
                            await Response.WriteAsync("<script language='javascript'>window.alert('Status Updated Successfully...!');window.location='TeacherData';</script>");
                        }
                        else
                        {
                            teacherstatus.VendorStatus = "Active";
                            princetonhive.SaveChanges();
                            await Response.WriteAsync("<script language='javascript'>window.alert('Status Updated Successfully...!');window.location='TeacherData';</script>");
                        }

                    }
                }
            }
            else
            {
                SchoolId = HttpContext.Session.GetString("SchoolIdBelouga");
                TeacherData = (from a in _context.TblRegistration
                               where a.SchoolIdBelouga == SchoolId
                               select new TblRegistration { RegistrationId = a.RegistrationId, DisplayName = a.DisplayName, Email = a.Email, Gender = a.Gender, Class = a.Class, Username = a.Username, Status = a.Status, VendorStatus = a.VendorStatus }).Distinct().ToList();

            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            try
            {
                if (savemore == "searchteacher")
                {

                    TeacherData = (from a in _context.TblRegistration
                                   where a.DisplayName == teachersearch
                                   select new TblRegistration { RegistrationId = a.RegistrationId, DisplayName = a.DisplayName, Email = a.Email, Gender = a.Gender, Class = a.Class, Username = a.Username, Status = a.Status, VendorStatus = a.VendorStatus }).OrderBy(x => x.DisplayName).Distinct().ToList();
                }


                else
                {
                    TeacherData = (from a in _context.TblRegistration
                                   where a.SchoolName == schoolsearch
                                   select new TblRegistration { RegistrationId = a.RegistrationId, DisplayName = a.DisplayName, Email = a.Email, Gender = a.Gender, Class = a.Class, Username = a.Username, Status = a.Status, VendorStatus = a.VendorStatus }).OrderBy(x => x.DisplayName).Distinct().ToList();
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