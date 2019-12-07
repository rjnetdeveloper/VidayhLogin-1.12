using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VidayhLogin.Enum;
using VidayhLogin.Helper;
using VidayhLogin.Model;

namespace VidayhLogin.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IConfiguration _configuration;
        HttpClient _client;
        
        public System.Net.CookieCollection Cookies { get; set; }

        public DashboardModel(IConfiguration config)
        {
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Username1 { get; set; }
        [BindProperty]
        public string token1 { get; set; }
        [BindProperty]
        public int CREATED_BY1 { set; get; }
        [BindProperty]
        public string session1 { set; get; }

        string session;
        public async Task<IActionResult> OnGetAsync()
        {

            CREATED_BY1 = Convert.ToInt32(HttpContext.Session.GetString("Username1"));
            Username = HttpContext.Session.GetString("Username");
            token1 = HttpContext.Session.GetString("token");
            ViewData["Username"] = Username;
            ViewData["token"] = token1;

           return Page();


        }

        
    }
}