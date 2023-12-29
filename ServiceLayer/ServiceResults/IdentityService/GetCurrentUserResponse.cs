using EntityLayer.Entities;
using ServiceLayer.Base.ServiceResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceResults.IdentityService
{
    public class GetCurrentUserResponse:AbstractResponseWithErrorsAndValue<ApplicationUser>
    {
    }
}
