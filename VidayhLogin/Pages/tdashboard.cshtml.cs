using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace VidayhLogin
{
    public class tdashboardModel : PageModel
    {
        private readonly IConfiguration _configuration;
        HttpClient _client;

        public tdashboardModel(IConfiguration config)
        {
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<IActionResult> OnGetAsync()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            _client.DefaultRequestHeaders.Add("x-AUTH-API-Key", "6d059451f1fc640f4d1c57b2e296239d");
            _client.DefaultRequestHeaders.Add("x-AUTH-Subdomain", "princetonhive");
            var response = await _client.GetAsync("https://princetonhive.thinkific.com/collections");
            var responseData = response.Content.ReadAsStringAsync().Result;
            ViewData["responseData"] = responseData;
            return Page();
        }
    }
}