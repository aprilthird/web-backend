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
    public class ActivityTypesController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "ActivityType";

        public ActivityTypesController()
        {
            _context = new FitGymEntities();
        }

        // GET /fitgymapi/activitytypes
        [HttpGet]
        public IHttpActionResult GetActivityTypes(int? gymCompanyId = null)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;

                var activityTypes = _context.ActivityType.ToList();

                if (gymCompanyId.HasValue)
                    activityTypes = activityTypes.Where(e => e.GymCompanyId == gymCompanyId.Value).ToList();

                Response.ActivityTypes = activityTypes.Select(Mapper.Map<ActivityType, ActivityTypeDto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET /fitgymapi/activitytypes/{id}
        [HttpGet]
        public IHttpActionResult GetActivityType(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                ActivityType activityType = _context.ActivityType.SingleOrDefault(a => a.ActivityTypeId == id);

                if (activityType == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.ActivityType = Mapper.Map<ActivityType, ActivityTypeDto>(activityType);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }


        // POST /fitgymapi/activitytypes
        [HttpPost]
        public IHttpActionResult CreateActivityType(ActivityTypeDto activityTypeDto)
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

                var activityType = Mapper.Map<ActivityTypeDto, ActivityType>(activityTypeDto);
                _context.ActivityType.Add(activityType);
                _context.SaveChanges();

                activityTypeDto.ActivityTypeId = activityType.ActivityTypeId;

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.ActivityType = activityTypeDto;

                return Created(new Uri(Request.RequestUri + "/" + activityType.ActivityTypeId), Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // PUT /fitgymapi/activitytypes/{id}
        [HttpPut]
        public IHttpActionResult UpdateActivityType(int id, ActivityTypeDto activityTypeDto)
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

                var activityType = _context.ActivityType.SingleOrDefault(a => a.ActivityTypeId == id);

                if (activityType == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(activityTypeDto, activityType);
                _context.SaveChanges();

                activityTypeDto.ActivityTypeId = activityType.ActivityTypeId;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.ActivityType = activityTypeDto;
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }


        // DELETE /fitgymapi/activitytypes/{id}
        [HttpDelete]
        public IHttpActionResult DeleteActivityType(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                var activityType = _context.ActivityType.SingleOrDefault(a => a.ActivityTypeId == id);

                if (activityType == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                activityType.Status = ConstantValues.EntityStatus.INACTIVE;
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
