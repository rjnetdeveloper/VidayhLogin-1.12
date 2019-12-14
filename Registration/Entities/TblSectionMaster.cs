using System;
using System.Collections.Generic;

namespace Registration.Entities
{
    public partial class TblSectionMaster
    {
        public string SectionName { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Sectionid { get; set; }
    }
}
