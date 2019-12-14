using System;
using System.Collections.Generic;

namespace Registration.Entities
{
    public partial class TblEnquiry
    {
        public string SchoolName { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int EnquiryId { get; set; }
    }
}
