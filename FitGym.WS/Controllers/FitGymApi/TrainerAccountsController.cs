using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FitGym.WS.Dtos;
using FitGym.WS.Models;

namespace FitGym.WS.Controllers.FitGymApi
{
    public class TrainerAccountsController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "Personal Trainer";

        public TrainerAccountsController()
        {
            _context = new FitGymEntities();
        }

        // POST /fitgymapi/traineraccounts
        [HttpPost]
        public IHttpActionResult Authenticate(AccountDto accountDto)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                var client = _context.PersonalTrainer.SingleOrDefault(c =>
                    c.Username.Equals(accountDto.Username) && c.Password.Equals(accountDto.Password));

                if (client == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, "#");
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Token = "8%3v9d0vsC31#%$55ferAT132PO9302casc1353";
                return Content(HttpStatusCode.OK, Response);
            }
            catch (Exception e)
            {

                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

    }
}
