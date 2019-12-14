using System;
using System.Collections.Generic;

namespace Registration.Entities
{
    public partial class TblSchool
    {
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public int? SchoolCity { get; set; }
        public int? SchoolState { get; set; }
        public int? SchoolCountry { get; set; }
        public string SchoolPostalCode { get; set; }
        public string SchoolLogo { get; set; }
        public string SchoolPhone { get; set; }
        public string SchoolEmail { get; set; }
        public string SchoolWebsite { get; set; }
        public string SchoolDescription { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonMobile { get; set; }
        public string ContactEmail { get; set; }
        public string SchoolIdBelouga { get; set; }
        public int Schoolid { get; set; }
    }
}
