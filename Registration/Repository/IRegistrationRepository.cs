using Registration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Repository
{
    public interface IRegistrationRepository
    {
        List<TblRegistration> GetClassBySchoolId(string schoolid);
        List<TblRegistration> GetSchoolDetailsBySchoolId(string schoolid);
        List<TblRegistration> GetTeacherBySchoolId(string schoolid);
        List<TblStudentRegistration> GetStudentAutoSearch(string StudentName, string UserName);
        List<TblRegistration> GetSchoolAutoSearch(string SchoolName, string UserName);
        List<TblRegistration> GetTeacherAutoSearch(string TeacherName, string UserName);
    }

}
