using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FitGym.WS.Controllers.FitGymApi.Auth
{
    [FitGymAuthenticationAttibute]
    public class AuthorizedController : ApiController
    {
        public User AuthorizedUser
        {
            get
            {
                return ((ApiIdentity)HttpContext.Current.User.Identity).User;
            }
        }
    }
}