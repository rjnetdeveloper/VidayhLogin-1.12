using System;
using System.Collections.Generic;

namespace Registration.Entities
{
    public partial class TblRegistration
    {
        public string Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public string PostalCode { get; set; }
        public string SchoolCode { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Session { get; set; }
        public string FatherName { get; set; }
        public string FatherMobile { get; set; }
        public string FatherEmail { get; set; }
        public string FatherEducation { get; set; }
        public string FatherOccupation { get; set; }
        public string MotherName { get; set; }
        public string MotherMobile { get; set; }
        public string MotherEmail { get; set; }
        public string MotherEducation { get; set; }
        public string MotherOccupation { get; set; }
        public string Photo { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobileno { get; set; }
        public string TeacherIdBelouga { get; set; }
        public string SchoolIdBelouga { get; set; }
        public string ClassIdBelouga { get; set; }
        public string SchoolName { get; set; }
        public string SchoolLogo { get; set; }
        public string SchoolPhone { get; set; }
        public string SchoolContactPerson { get; set; }
        public string SchoolEmail { get; set; }
        public string City { get; set; }
        public int RegistrationId { get; set; }
    }
}
