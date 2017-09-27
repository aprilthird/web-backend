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

namespace FitGym.WS.Controllers.FitGymApi
{
    public class EstablishmentsController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "Establishment";

        public EstablishmentsController()
        {
            _context = new FitGymEntities();
        }

        // GET /fitgymapi/personaltrainers
        [HttpGet]
        public IHttpActionResult GetEstablishments(int? gymCompanyId = null)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;

                var establishments = _context.Establishment.ToList();

                if (gymCompanyId.HasValue)
                    establishments = establishments.Where(e => e.GymCompanyId == gymCompanyId.Value).ToList();

                Response.Establishments = establishments.Select(Mapper.Map<Establishment, EstablishmentDto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET /fitgymapi/personaltrainers/{id}
        [HttpGet]
        public IHttpActionResult GetEstablishment(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Establishment establishment = _context.Establishment.SingleOrDefault(e => e.EstablishmentId == id);

                if (establishment == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Establishment = Mapper.Map<Establishment, EstablishmentDto>(establishment);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }


        // POST /fitgymapi/gymcompanies
        [HttpPost]
        public IHttpActionResult CreateEstablishment(EstablishmentDto establishmentDto)
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

                var establishment = Mapper.Map<EstablishmentDto, Establishment>(establishmentDto);
                _context.Establishment.Add(establishment);
                _context.SaveChanges();

                establishmentDto.EstablishmentId = establishment.EstablishmentId;

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Establishment = establishmentDto;

                return Created(new Uri(Request.RequestUri + "/" + establishment.EstablishmentId), Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // PUT /fitgymapi/gymcompanies/{id}
        [HttpPut]
        public IHttpActionResult UpdateEstablishment(int id, EstablishmentDto establishmentDto)
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

                var establishment = _context.Establishment.SingleOrDefault(e => e.EstablishmentId == id);

                if (establishment == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(establishmentDto, establishment);
                _context.SaveChanges();

                establishmentDto.EstablishmentId = establishment.EstablishmentId;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Establishment = establishmentDto;
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // DELETE /fitgymapi/gymcompanies/{id}
        [HttpDelete]
        public IHttpActionResult DeleteEstablishment(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                var establishment = _context.Establishment.SingleOrDefault(e => e.EstablishmentId == id);

                if (establishment == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                establishment.Status = ConstantValues.EntityStatus.INACTIVE;
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
