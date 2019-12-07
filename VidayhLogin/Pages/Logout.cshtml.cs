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
using Newtonsoft.Json;
using VidayhLogin.Enum;
using VidayhLogin.Helper;

namespace VidayhLogin.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IConfiguration _configuration;
        HttpClient _client = new HttpClient();

        public LogoutModel(IConfiguration config)
        {
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> OnGet()
        {
            string username = HttpContext.Session.GetString("Username");
            var response = await _client.GetAsync("Users/Logout?username=" + username);
            var loginResult = response.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<ResponseViewModel>(loginResult);
            if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
            {
               
            }
            HttpContext.Session.Clear();
         //   Response.Redirect("https://princetonhive.com");
            return Page();
           
        }
    }
}