using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Web;
using FitGym.WS.Controllers.FitGymApi.Auth;
using FitGym.WS.Models;

namespace FitGym.WS.Controllers.FitGymApi
{
    public class FitGymAuthenticationAttibute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);
                FitGymEntities _context = new FitGymEntities();
                GymCompany gymCompany =
                    _context.GymCompany.SingleOrDefault(x =>
                        x.Username.Equals(username) && x.Password.Equals(password));
                PersonalTrainer personalTrainer =
                    _context.PersonalTrainer.SingleOrDefault(x =>
                        x.Username.Equals(username) && x.Password.Equals(password));
                Client client =
                    _context.Client.SingleOrDefault(x => x.Username.Equals(username) && x.Password.Equals(password));

                if (gymCompany != null)
                {
                    User user = new User();
                    user.Username = gymCompany.Username;
                    user.Password = gymCompany.Password;
                    HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(user), new string[] { });
                    base.OnActionExecuting(actionContext);
                }
                else if (personalTrainer != null)
                {
                    User user = new User();
                    user.Username = personalTrainer.Username;
                    user.Password = personalTrainer.Password;
                    HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(user), new string[] {});
                    base.OnActionExecuting(actionContext);
                }
                else if (client != null)
                {
                    User user = new User();
                    user.Username = client.Username;
                    user.Password = client.Password;
                    HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(user), new string[] { });
                    base.OnActionExecuting(actionContext);
                }
                else
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
        }
    }

}