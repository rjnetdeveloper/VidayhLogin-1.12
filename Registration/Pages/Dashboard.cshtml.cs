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
using Registration.Entities;

namespace Registration
{
    public class DashboardModel : PageModel
    {
        private readonly PrincetonhiveContext _context;
        private readonly IConfiguration _configuration;
        HttpClient _client;

        public DashboardModel(IConfiguration config, PrincetonhiveContext context)
        {
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _context = context;
        }

        [BindProperty]
        public string DisplayName { get; set; }
        [BindProperty]
        public int Schools { get; set; }
        [BindProperty]
        public int Students { get; set; }
        [BindProperty]
        public int Teachers { get; set; }
        public void OnGet()
        {
            DisplayName = HttpContext.Session.GetString("DisplayName");

             Schools = (from a in _context.TblRegistration
                       where a.SchoolName != null
                       select a).GroupBy(x=>x.SchoolName).Count();
            
            Students = (from a in _context.TblStudentRegistration
                       select a).Distinct().Count();

            Teachers = (from a in _context.TblRegistration
                       where a.SchoolName != null 
                       select a).Count();
        }
    }
}