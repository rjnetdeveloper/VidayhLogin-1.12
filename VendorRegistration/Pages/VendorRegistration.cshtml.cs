using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using VendorRegistration.Entities;

namespace VendorRegistration.Pages
{
    public class VendorRegistrationModel : PageModel
    {

        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        private readonly VendorsContext _context;

        public VendorRegistrationModel(IConfiguration config, VendorsContext context)
        {
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _connection = _configuration["ConnectionStrings:Princetonhive"];
            _context = context;
        }

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string City { get; set; }
        [BindProperty]
        public string State { get; set; }
        [BindProperty]
        public string Country { get; set; }
        [BindProperty]
        public string Pincode { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string ContactPerson { get; set; }
        [BindProperty]
        public string PhoneNo { get; set; }
        [BindProperty]
        public string MobileNo { get; set; }
        [BindProperty]
        public int Vendor { get; set; }
        [BindProperty]
        public string Type { get; set; }
        [BindProperty]
        public string VendorAPI { get; set; }
        public IList<VendorsRegistration> Vendors { get; private set; }
        List<VendorsRegistration> Vendors1 = new List<VendorsRegistration>();

        public void OnGet()
        {
            var vendors = (from a in _context.VendorsRegistration
                           select new VendorsRegistration { VendorId = a.VendorId, Name = a.Name }).ToList();

            Vendors = vendors.ToList();
        }

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            var vendor = new VendorsContext();
            if (savemore == "Save")
            {
                
                  var teacher = new VendorsRegistration()
                    {
                      Name = Name,
                      Address = Address, 
                      City = City,
                      State = State,
                      Country = Country,
                      Pincode = Pincode,
                      Email = Email,
                      ContactPerson=ContactPerson,
                      PhoneNo=PhoneNo,
                      MobileNo=MobileNo
                       
                    };
                vendor.VendorsRegistration.Add(teacher);
                vendor.SaveChanges();


                await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='VendorRegistration';</script>");
                return Page();

            }
            else if (savemore == "Save.")
            {
                var studentd = new VendorApi()
                {
                    VendorId = Vendor,
                    Type = Type,
                    VendorApi1 = VendorAPI
                   
                };
                vendor.VendorApi.Add(studentd);
                vendor.SaveChanges();
                await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='VendorRegistration';</script>");
            }
            return Page();
        }
    }
}