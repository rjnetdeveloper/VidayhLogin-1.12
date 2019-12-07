using Registration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models
{
    public interface IClassRepository
    {
        List<TblRegistration> GetClass(string schoolid);
    }
}
