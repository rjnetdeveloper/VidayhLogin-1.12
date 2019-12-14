using System;
using System.Collections.Generic;

namespace Registration.Entities
{
    public partial class TblClassMaster
    {
        public string ClassName { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Description { get; set; }
        public string ClassIdBelouga { get; set; }
        public string TeacherId { get; set; }
        public string SchoolId { get; set; }
        public int Classid { get; set; }
    }
}
