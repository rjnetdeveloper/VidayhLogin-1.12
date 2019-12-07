using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.Helper;
using Registration.Models;


namespace Registration.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private IClassRepository _repository;
        public ClassController(IClassRepository ClassRepository)
        {
            _repository = ClassRepository;
        }
        [HttpGet]
        [Route("Class/{search}")]
        public IActionResult GetClass(string search)
        {
            try
            {
                var ClassModels = _repository.GetClass(search);
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
    }
}
