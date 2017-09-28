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
    public class ActivitiesController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "Acitivity";

        public ActivitiesController()
        {
            _context = new FitGymEntities();
        }

        // GET /fitgymapi/activities
        [HttpGet]
        public IHttpActionResult GetActivities(int? clientId = null)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;

                var activities = _context.Activity.ToList();

                if (clientId.HasValue)
                    activities = activities.Where(a => a.ClientId == clientId.Value).ToList();

                Response.Activities = activities.Select(Mapper.Map<Activity, ActivityDto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET /fitgymapi/activities/{id}
        [HttpGet]
        public IHttpActionResult GetActivity(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Activity activity= _context.Activity.SingleOrDefault(a => a.ActivityId == id);

                if (activity == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Activity = Mapper.Map<Activity, ActivityDto>(activity);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }


        // POST /fitgymapi/activities
        [HttpPost]
        public IHttpActionResult CreateActivity(ActivityDto activityDto)
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

                var activity = Mapper.Map<ActivityDto, Activity>(activityDto);
                _context.Activity.Add(activity);
                _context.SaveChanges();

                activityDto.ActivityId = activity.ActivityId;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Activity = activityDto;

                return Created(new Uri(Request.RequestUri + "/" + activity.ActivityId), Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // PUT /fitgymapi/activities/{id}
        [HttpPut]
        public IHttpActionResult UpdateActivity(int id, ActivityDto activityDto)
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

                var activityInDb = _context.Activity.SingleOrDefault(a => a.ActivityId == id);

                if (activityInDb == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(activityDto, activityInDb);
                _context.SaveChanges();

                activityDto.ActivityId = id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Activity = activityDto;
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // DELETE /fitgymapi/activities/{id}
        [HttpDelete]
        public IHttpActionResult DeleteActivity(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                var activity = _context.Activity.SingleOrDefault(a => a.ActivityId == id);

                if (activity == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                activity.Status = ConstantValues.EntityStatus.INACTIVE;
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
