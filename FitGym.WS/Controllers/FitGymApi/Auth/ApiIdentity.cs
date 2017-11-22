using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace FitGym.WS.Controllers.FitGymApi.Auth
{
    public class ApiIdentity : IIdentity
    {
        public User User
        {
            get;
            private set;
        }

        public ApiIdentity(User user)
        {
            if (user == null) throw new ArgumentException("user");
            this.User = user;
        }

        public string Name
        {
            get { return User.Username; }
        }

        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}