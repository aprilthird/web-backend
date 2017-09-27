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
    public class SubscriptionsController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "Subscription";

        public SubscriptionsController()
        {
            _context = new FitGymEntities();
        }

        // GET /fitgymapi/subscriptions
        public IHttpActionResult GetSubscriptions()
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Subscriptions =
                    _context.Subscription.ToList().Select(Mapper.Map<Subscription, SubscriptionDto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET /fitgymapi/subscriptions/{id}
        public IHttpActionResult GetSubscription(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Subscription subscription= _context.Subscription.SingleOrDefault(s => s.SubscriptionId == id);

                if (subscription == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Subscription = Mapper.Map<Subscription, SubscriptionDto>(subscription);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // POST /fitgymapi/subscriptions
        [HttpPost]
        public IHttpActionResult CreateSubscription(SubscriptionDto subscriptionDto)
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

                var subscription = Mapper.Map<SubscriptionDto, Subscription>(subscriptionDto);
                _context.Subscription.Add(subscription);
                _context.SaveChanges();

                subscriptionDto.SubscriptionId = subscription.SubscriptionId;

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Subscription = subscriptionDto;

                return Created(new Uri(Request.RequestUri + "/" + subscription.SubscriptionId), Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // PUT /fitgymapi/subscriptions/{id}
        [HttpPut]
        public IHttpActionResult UpdateSubscription(int id, SubscriptionDto subscriptionDto)
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

                var subscriptionInDb = _context.Subscription.SingleOrDefault(s => s.SubscriptionId == id);

                if (subscriptionInDb == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(subscriptionDto, subscriptionInDb);
                _context.SaveChanges();

                subscriptionDto.GymCompanyId = id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Subscription = subscriptionDto;
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // DELETE /fitgymapi/subscriptions/{id}
        public IHttpActionResult DeleteSubscription(int id)
        {

            dynamic Response = new ExpandoObject();

            try
            {
                var subscription = _context.Subscription.SingleOrDefault(s => s.SubscriptionId == id);

                if (subscription == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                subscription.Status = ConstantValues.EntityStatus.INACTIVE;
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
