using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FitGym.WS.Models;

namespace FitGym.WS.Controllers.FitGymApi
{
    public class TestsController : ApiController
    {
        public IHttpActionResult GetMessage(string query = null)
        {
            FitGymEntities gc = new FitGymEntities();
            int a = gc.GymCompany.Count();
            return Content(HttpStatusCode.OK, "Holaaa " + a + query);
        }
    }
}
