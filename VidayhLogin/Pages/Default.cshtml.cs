using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VidayhLogin.Pages
{
    public class DefaultModel : PageModel
    {
        public void OnGet()
        {

        }

        [BindProperty]
        public string UserName { set; get; }
        [BindProperty]
        public string Password { set; get; }

        public async Task<IActionResult> OnPostAsync()
        {

            return Page();
        }
    }
}