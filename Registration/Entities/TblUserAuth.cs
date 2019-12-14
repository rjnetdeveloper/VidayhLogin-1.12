using System;
using System.Collections.Generic;

namespace Registration.Entities
{
    public partial class TblUserAuth
    {
        public string UserRole { get; set; }
        public string UserAuthorities { get; set; }
        public string UserStatus { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public int UserAuthId { get; set; }
    }
}
