using Microsoft.Extensions.Configuration;
using Registration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models
{
    public class ClassRepository : IClassRepository
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        private readonly PrincetonhiveContext _context;
        public ClassRepository(IConfiguration configuration, PrincetonhiveContext context)
        {
            _configuration = configuration;
            _connection = _configuration["ConnectionStrings:Princetonhive"];
            _context = context;
        }

        public List<TblRegistration> GetClass(string schoolid)
        {
            List<TblRegistration> classes = new List<TblRegistration>();

            try
            {
                classes = (from a in _context.TblRegistration
                           where a.SchoolIdBelouga == schoolid
                           select new TblRegistration { ClassIdBelouga = a.ClassIdBelouga, Class = a.Class }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return classes;
        }
    }


}
