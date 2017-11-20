using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using FitGym.WS.Dtos;
using FitGym.WS.Models;

namespace FitGym.WS.Controllers.FitGymApi
{
    public class GymCompaniesController : ApiController
    {
        private string EntityName = "Gym Company";
        private FitGymEntities _context;

        public GymCompaniesController()
        {
            _context = new FitGymEntities();
        }

        // GET /fitgymapi/gymcompanies
        [HttpGet]
        public IHttpActionResult GetGymCompanies()
        {
            dynamic Response = new ExpandoObject();
            
            try
            {
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.GymCompanies = _context.GymCompany.ToList().Select(Mapper.Map<GymCompany, GymCompanyDto>); 
                return Ok(Response);
            }
            catch (Exception e)
            {
                Response.Status = ConstantValues.ResponseStatus.ERROR;
                Response.Message = ConstantValues.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        // GET /fitgymapi/gymcompanies/{id}
        public IHttpActionResult GetGymCompany(int id)
        {
            dynamic Response = new ExpandoObject();
            try
            {
                
                GymCompany gymCompany = _context.GymCompany.SingleOrDefault(c => c.GymCompanyId == id);

                if (gymCompany == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.GymCompany = Mapper.Map<GymCompany, GymCompanyDto>(gymCompany);
                
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
        public IHttpActionResult CreateGymCompany(GymCompanyDto gymCompanyDto)
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

                var gymCompany = Mapper.Map<GymCompanyDto, GymCompany>(gymCompanyDto);
                gymCompany.CreatedAt = DateTime.Now;
                gymCompany.UpdatedAt = DateTime.Now;
                gymCompany.Status = ConstantValues.EntityStatus.ACTIVE;
                _context.GymCompany.Add(gymCompany);
                _context.SaveChanges();

                gymCompanyDto.GymCompanyId = gymCompany.GymCompanyId;

                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.GymCompany = gymCompanyDto;

                return Created(new Uri(Request.RequestUri + "/" + gymCompany.GymCompanyId), Response);
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
        public IHttpActionResult UpdateGymCompany(int id, GymCompanyDto gymCompanyDto)
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

                var gymCompanyInDb = _context.GymCompany.SingleOrDefault(g => g.GymCompanyId == id);

                if (gymCompanyInDb == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                Mapper.Map(gymCompanyDto, gymCompanyInDb);
                gymCompanyInDb.UpdatedAt = DateTime.Now;
                _context.SaveChanges();

                gymCompanyDto.GymCompanyId = id;
                Response.Status = ConstantValues.ResponseStatus.OK;
                Response.GymCompany = gymCompanyDto;
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
        public IHttpActionResult DeleteGymCompany(int id)
        {
            dynamic Response = new ExpandoObject();
            
            try
            {
                var gymCompany = _context.GymCompany.SingleOrDefault(g => g.GymCompanyId == id);

                if (gymCompany == null)
                {
                    Response.Status = ConstantValues.ResponseStatus.ERROR;
                    Response.Message = string.Format(ConstantValues.ErrorMessage.NOT_FOUND, EntityName, id);
                    return Content(HttpStatusCode.NotFound, Response);
                }

                gymCompany.Status = ConstantValues.EntityStatus.INACTIVE;
                gymCompany.UpdatedAt = DateTime.Now;
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

