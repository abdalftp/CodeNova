using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WMS.Models.VM;

namespace WebAPIJWT.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage ValidLogin(string userName, string userPassword)
        {
            if (userName == "admin" && userPassword == "admin")
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, value: TokenManager.GenerateToken(userName));
            }

            else
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadGateway, message: "User name and password is invalid");
            }

        }
        [HttpGet]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetEmployee()
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, value:"Successfully Valid");
        }
    }
}