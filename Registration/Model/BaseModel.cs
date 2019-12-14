using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace VidayhLogin.Model
{
    public class BaseModel
    {
        [IgnoreDataMember]
        public long TotalCount { set; get; }

        [IgnoreDataMember]
        public int StatusCode { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
        public int Createdby { set; get; }
        public int Updatedby { set; get; }
    }
}
