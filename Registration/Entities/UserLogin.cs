using System;
using System.Collections.Generic;

namespace Registration.Entities
{
    public partial class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Captcha { get; set; }
        public string UserRole { get; set; }
        public int UserLoginId { get; set; }
    }
}
