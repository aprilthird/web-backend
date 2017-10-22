using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FitGym.WS.Dtos;
using FitGym.WS.Models;
using Microsoft.Ajax.Utilities;

namespace FitGym.WS.Controllers.FitGymApi
{
    public class ClientsController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "Client";

        public ClientsController()
        {
            _context = new FitGymEntities();
        }

        // GET /fitgymapi/clients
        [HttpGet]
        public IHttpActionResult GetClients(int? personalTrainerId = null, int? gymCompanyId = null, string query = "")
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;

                var clients = _context.Client.ToList();

                if (gymCompanyId.HasValue)
                    clients = clients.Where(c => c.PersonalTrainer.GymCompanyId == gymCompanyId.Value).ToList();

                if (personalTrainerId.HasValue)
                    clients = clients.Where(c => c.PersonalTrainerId == personalTrainerId.Value).ToList();

                if (!query.IsNullOrWhiteSpace())
                    clients = clients.Where(c => c.FirstName.Contains(query) || c.LastName.Contains(query)).ToList();

                Response.Client = clients.Select(Mapper.Map<Client, ClientDto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET /fitgymapi/clients/{id}
        [HttpGet]
        public IHttpActionResult GetClient(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Client client = _context.Client.SingleOrDefault(c => c.ClientId == id);

                if (client == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.PersonalTrainer = Mapper.Map<Client, ClientDto>(client);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }


        // POST /fitgymapi/clients
        [HttpPost]
        public IHttpActionResult CreateClient (ClientDto clientDto, AccountDto accountDto)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = ConstantValues.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var client = Mapper.Map<ClientDto, Client>(clientDto);
                client.Password = accountDto.Password;
                _context.Client.Add(client);
                _context.SaveChanges();

                clientDto.ClientId = client.ClientId;

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Client = clientDto;

                return Created(new Uri(Request.RequestUri + "/" + client.ClientId), Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // PUT /fitgymapi/clients/{id}
        [HttpPut]
        public IHttpActionResult UpdateClient(int id, ClientDto clientDto)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = ConstantValues.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var clientInDb = _context.Client.SingleOrDefault(c => c.ClientId == id);

                if (clientInDb == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(clientDto, clientInDb);
                _context.SaveChanges();

                clientDto.ClientId = id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Client = clientDto;
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // DELETE /fitgymapi/clients/{id}
        [HttpDelete]
        public IHttpActionResult DeleteClient(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                var client = _context.Client.SingleOrDefault(c => c.ClientId == id);

                if (client == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                client.Status = ConstantValues.EntityStatus.INACTIVE;
                _context.SaveChanges();

                Response.Status = ConstantValues.ResponseStatus.OK;
                return Ok(Response);
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
