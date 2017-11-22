using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using AutoMapper;
using FitGym.WS.Dtos;
using FitGym.WS.Models;

namespace FitGym.WS.Controllers.FitGymApi
{
    public class ClientAccountsController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "Client";

        public ClientAccountsController()
        {
            _context = new FitGymEntities();
        }

        // POST /fitgymapi/clientsaccount
        [HttpPost]
        public IHttpActionResult Authenticate(AccountDto accountDto)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                var client = _context.Client.SingleOrDefault(c =>
                    c.Username.Equals(accountDto.Username) && c.Password.Equals(accountDto.Password));

                if (client == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, "#");
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                String uncoded = client.Username + ":" + client.Password;
                Response.Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(uncoded));
                Response.Client = Mapper.Map<Client, ClientDto>(client);
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
