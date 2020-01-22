using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VidayhLogin
{
    public class vendorsModel : PageModel
    {
        [BindProperty]
        public string vendor1 { get; set; }
        [BindProperty]
        public string vendor2 { get; set; }
        public void OnGet()
        {
            var vendorname = HttpContext.Session.GetString("vendorname");
            var vendornameresult = vendorname.Split(",");
             string ven1 = vendornameresult[0].ToString();
            if (ven1 == "Belouga")
            {
                vendor1 = vendornameresult[0].ToString();
                vendor2 = vendornameresult[1].ToString();
            }
            if (ven1 == "Thinkfic")
            {
                vendor1 = vendornameresult[1].ToString();
                vendor2 = vendornameresult[0].ToString();
            }



        }
    }
}