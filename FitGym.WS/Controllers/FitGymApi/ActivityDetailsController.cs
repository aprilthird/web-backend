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
    [FitGymAuthenticationAttibute]
    public class ActivityDetailsController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "Activity Detail";

        public ActivityDetailsController()
        {
            _context = new FitGymEntities();
        }

        // GET /fitgymapi/activitydetails
        [HttpGet]
        public IHttpActionResult GetActivityDetails(int? activityId = null)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                
                var activityDetails = _context.ActivityDetail.ToList();

                if (activityId.HasValue)
                    activityDetails = activityDetails.Where(a => a.ActivityId == activityId.Value).ToList();

                Response.ActivityDetails = activityDetails.Select(Mapper.Map<ActivityDetail, ActivityDetailDto>);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET /fitgymapi/activitydetails/{id}
        [HttpGet]
        public IHttpActionResult GetActivityDetail(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                ActivityDetail activityDetail = _context.ActivityDetail.SingleOrDefault(a => a.ActivityDetailId == id);

                if (activityDetail == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.ActivityDetail = Mapper.Map<ActivityDetail, ActivityDetailDto>(activityDetail);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }


        // POST /fitgymapi/activitydetails
        [HttpPost]
        public IHttpActionResult CreateActivityDetail(ActivityDetailDto activityDetailDto)
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

                var activityDetail = Mapper.Map<ActivityDetailDto, ActivityDetail>(activityDetailDto);
                _context.ActivityDetail.Add(activityDetail);
                _context.SaveChanges();

                activityDetailDto.ActivityDetailId = activityDetail.ActivityDetailId;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.ActivityDetail = activityDetailDto;

                return Created(new Uri(Request.RequestUri + "/" + activityDetail.ActivityId), Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // PUT /fitgymapi/activitydetails/{id}
        [HttpPut]
        public IHttpActionResult UpdateActivityDetail(int id, ActivityDetailDto activityDetailDto)
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

                var activityDetailInDb = _context.ActivityDetail.SingleOrDefault(a => a.ActivityDetailId == id);

                if (activityDetailInDb == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(activityDetailDto, activityDetailInDb);
                _context.SaveChanges();

                activityDetailDto.ActivityDetailId = id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.Activity = activityDetailDto;
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // DELETE /fitgymapi/activitydetails/{id}
        [HttpDelete]
        public IHttpActionResult DeleteActivity(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                var activityDetail = _context.ActivityDetail.SingleOrDefault(a => a.ActivityDetailId == id);

                if (activityDetail == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                //activityDetail.Status = ConstantValues.EntityStatus.INACTIVE;
                _context.ActivityDetail.Remove(activityDetail);
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
