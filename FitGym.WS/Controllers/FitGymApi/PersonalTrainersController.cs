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
    public class PersonalTrainersController : ApiController
    {
        private FitGymEntities _context;
        private string EntityName = "Personal Trainer";

        public PersonalTrainersController()
        {
            _context = new FitGymEntities();
        }


        // GET /fitgymapi/personaltrainers
        [HttpGet]
        public IHttpActionResult GetPersonalTrainers(int? gymCompanyId = null, string query = "")
        {
            dynamic Response = new ExpandoObject();

            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;

                var personalTrainers = _context.PersonalTrainer.ToList();

                if (gymCompanyId.HasValue)
                    personalTrainers = personalTrainers.Where(p => p.GymCompanyId == gymCompanyId.Value).ToList();

                if (!query.IsNullOrWhiteSpace())
                    personalTrainers = personalTrainers.Where(p => p.FirstName.Contains(query) || p.LastName.Contains(query)).ToList();

                Response.PersonalTrainers = personalTrainers.Select(Mapper.Map<PersonalTrainer, PersonalTrainerDto>);
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
        public IHttpActionResult GetPersonalTrainer(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                PersonalTrainer personalTrainer = _context.PersonalTrainer.SingleOrDefault(p => p.PersonalTrainerId == id);

                if (personalTrainer == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.PersonalTrainer = Mapper.Map<PersonalTrainer, PersonalTrainerDto>(personalTrainer);
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }


        // POST /fitgymapi/personaltrainers
        [HttpPost]
        public IHttpActionResult CreatePersonalTrainer(PersonalTrainerDto personalTrainerDto, AccountDto accountDto)
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

                var personalTrainer = Mapper.Map<PersonalTrainerDto, PersonalTrainer>(personalTrainerDto);
                personalTrainer.Password = accountDto.Password;
                _context.PersonalTrainer.Add(personalTrainer);
                _context.SaveChanges();

                personalTrainerDto.PersonalTrainerId = personalTrainer.PersonalTrainerId;

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.PersonalTrainer = personalTrainerDto;

                return Created(new Uri(Request.RequestUri + "/" + personalTrainer.PersonalTrainerId), Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // PUT /fitgymapi/personaltrainers/{id}
        [HttpPut]
        public IHttpActionResult UpdatePersonalTrainer(int id, PersonalTrainerDto personalTrainerDto)
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

                var personalTrainerInDb = _context.PersonalTrainer.SingleOrDefault(p => p.PersonalTrainerId == id);

                if (personalTrainerInDb == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(personalTrainerDto, personalTrainerInDb);
                _context.SaveChanges();

                personalTrainerDto.PersonalTrainerId = id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.PersonalTrainer = personalTrainerDto;
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // DELETE /fitgymapi/personaltrainers/{id}
        [HttpDelete]
        public IHttpActionResult DeletePersonalTrainer(int id)
        {
            dynamic Response = new ExpandoObject();

            try
            {
                var personalTrainer = _context.PersonalTrainer.SingleOrDefault(p => p.PersonalTrainerId == id);

                if (personalTrainer == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                personalTrainer.Status = ConstantValues.EntityStatus.INACTIVE;
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
