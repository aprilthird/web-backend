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
    public class SubscriptionTypesController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "Subscription Type";

        public SubscriptionTypesController()
        {
            _context = new FitGymEntities();
        }

        // GET /fitgymapi/subscriptiontypes
        public IHttpActionResult GetSubscriptionTypes()
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.SubscriptionTypes = _context.SubscriptionType.ToList().Select(Mapper.Map<SubscriptionType, SubscriptionTypeDto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET /gitgymapi/subscriptiontypes/{id}
        public IHttpActionResult GetSubscriptionType(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                SubscriptionType subscriptionType = _context.SubscriptionType.SingleOrDefault(s => s.SubscriptionTypeId == id);

                if (subscriptionType == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Status = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.SubscriptionType = Mapper.Map<SubscriptionType, SubscriptionTypeDto>(subscriptionType);
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
