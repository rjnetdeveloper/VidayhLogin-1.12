using System;
using System.Collections.Generic;

namespace VendorRegistration.Entities
{
    public partial class VendorsRegistration
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
    }
}
