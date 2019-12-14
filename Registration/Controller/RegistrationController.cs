using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.Helper;
using Registration.Repository;

namespace Registration.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IRegistrationRepository _repository;
        public RegistrationController(IRegistrationRepository ClassRepository)
        {
            _repository = ClassRepository;
        }

        [HttpGet]
        [Route("GetClassBySchoolId/{search}")]
        public IActionResult GetClassBySchoolId(string search)
        {
            try
            {
                var ClassModels = _repository.GetClassBySchoolId(search);
                if (ClassModels == null && ClassModels.Count == 0)
                    return CommonModel.GetResponse("Sorry! data not found", null, HttpStatusCode.OK);
                //if (ClassModels == null && ClassModels[0].StatusCode == Convert.ToInt32(HttpStatusCode.InternalServerError))
                //    return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
                return CommonModel.GetResponse("Data found", ClassModels, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetSchoolDetailsBySchoolId/{search}")]
        public IActionResult GetSchoolDetailsBySchoolId(string search)
        {
            try
            {
                var SchoolModels = _repository.GetSchoolDetailsBySchoolId(search);
                if (SchoolModels == null && SchoolModels.Count == 0)
                    return CommonModel.GetResponse("Sorry! data not found", null, HttpStatusCode.OK);
                //if (ClassModels == null && ClassModels[0].StatusCode == Convert.ToInt32(HttpStatusCode.InternalServerError))
                //    return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
                return CommonModel.GetResponse("Data found", SchoolModels, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetTeacherBySchoolId/{search}")]
        public IActionResult GetTeacherBySchoolId(string search)
        {
            try
            {
                var SchoolModels = _repository.GetTeacherBySchoolId(search);
                if (SchoolModels == null && SchoolModels.Count == 0)
                    return CommonModel.GetResponse("Sorry! data not found", null, HttpStatusCode.OK);
                //if (ClassModels == null && ClassModels[0].StatusCode == Convert.ToInt32(HttpStatusCode.InternalServerError))
                //    return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
                return CommonModel.GetResponse("Data found", SchoolModels, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetStudentAutoSearch/{StudentName}/{UserName}")]
        public IActionResult GetStudentAutoSearch(string StudentName,string UserName)
        {
            try
            {
                var StudentModels = _repository.GetStudentAutoSearch(StudentName,UserName);
                if (StudentModels == null && StudentModels.Count == 0)
                    return CommonModel.GetResponse("Sorry! data not found", null, HttpStatusCode.OK);
                //if (ClassModels == null && ClassModels[0].StatusCode == Convert.ToInt32(HttpStatusCode.InternalServerError))
                //    return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
                return CommonModel.GetResponse("Data found", StudentModels, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetSchoolAutoSearch/{SchoolName}/{UserName}")]
        public IActionResult GetSchoolAutoSearch(string SchoolName, string UserName)
        {
            try
            {
                var SchoolModels = _repository.GetSchoolAutoSearch(SchoolName, UserName);
                if (SchoolModels == null && SchoolModels.Count == 0)
                    return CommonModel.GetResponse("Sorry! data not found", null, HttpStatusCode.OK);
                //if (ClassModels == null && ClassModels[0].StatusCode == Convert.ToInt32(HttpStatusCode.InternalServerError))
                //    return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
                return CommonModel.GetResponse("Data found", SchoolModels, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetTeacherAutoSearch/{TeacherName}/{UserName}")]
        public IActionResult GetTeacherAutoSearch(string TeacherName, string UserName)
        {
            try
            {
                var TeacherModels = _repository.GetTeacherAutoSearch(TeacherName, UserName);
                if (TeacherModels == null && TeacherModels.Count == 0)
                    return CommonModel.GetResponse("Sorry! data not found", null, HttpStatusCode.OK);
                //if (ClassModels == null && ClassModels[0].StatusCode == Convert.ToInt32(HttpStatusCode.InternalServerError))
                //    return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
                return CommonModel.GetResponse("Data found", TeacherModels, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CommonModel.GetResponse("Sorry! Somthing went wrong", null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
