using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Enum
{
    public enum StatusCodesStatus
    {
        SuccessCode = 200,
        InternaServerError = 500,
        DataNotFound = 204,
        DataCreated = 201,
        BadRequest = 400,
        NotFound = 404,
        UnAuthorised = 401
    }
}
