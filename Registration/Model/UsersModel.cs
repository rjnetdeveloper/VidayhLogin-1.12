using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Model
{
    public class UsersModel
    {
        public string Username { set; get; }
        public string Password { set; get; }
        public string NewPassword { set; get; }
        public string Role { set; get; }
        public string Email { set; get; }
    }
}
