using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VidayhLogin.Model
{
    public class LoginViewModel : BaseModel
    {
        public string Balougaloginpage { set; get; }
    }

    public class LoginViewRazorModel
    {
        public List<LoginViewModel> data { set; get; }
        public LoginViewRazorModel()
        {
            data = new List<LoginViewModel>();
        }
    }
}
