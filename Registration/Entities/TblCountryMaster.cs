using System;
using System.Collections.Generic;

namespace Registration.Entities
{
    public partial class TblCountryMaster
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Phonecode { get; set; }
        public int Countryid { get; set; }
    }
}
