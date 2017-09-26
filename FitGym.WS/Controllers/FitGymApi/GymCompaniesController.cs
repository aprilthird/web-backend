using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FitGym.WS.Dtos;
using FitGym.WS.Models;

namespace FitGym.WS.Controllers.FitGymApi
{
    public class GymCompaniesController : ApiController
    {
        private FitGymEntities _context;

        public GymCompaniesController()
        {
            _context = new FitGymEntities();
        }

        // GET /fitgymapi/gymcompanies
        public IEnumerable<GymCompanyDto> GetGymCompanies()
        {
            return _context.GymCompany.ToList().Select(Mapper.Map<GymCompany, GymCompanyDto>);
        }

        // GET /fitgymapi/gymcompanies/{id}
        public GymCompanyDto GetGymCompany(int id)
        {
            var gymCompany = _context.GymCompany.SingleOrDefault(c => c.GymCompanyId == id);

            if(gymCompany == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<GymCompany, GymCompanyDto>(gymCompany);
        }


        // POST /fitgymapi/gymcompanies
        [HttpPost]
        public GymCompanyDto CreateGymCompany(GymCompanyDto gymCompanyDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);


            var gymCompany = Mapper.Map<GymCompanyDto, GymCompany>(gymCompanyDto);
            _context.GymCompany.Add(gymCompany);
            _context.SaveChanges();

            gymCompanyDto.GymCompanyId = gymCompany.GymCompanyId;

            return gymCompanyDto;
        }

        // PUT /fitgymapi/gymcompanies/{id}
        [HttpPut]
        public void UpdateGymCompany(int id, GymCompanyDto gymCompanyDto)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var gymCompanyInDb = _context.GymCompany.SingleOrDefault(g => g.GymCompanyId == id);

            if(gymCompanyInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(gymCompanyDto, gymCompanyInDb);
            
            _context.SaveChanges();
        }

        // DELETE /fitgymapi/gymcompanies/{id}
        [HttpDelete]
        public void DeleteGymCompany(int id)
        {
            var gymCompany = _context.GymCompany.SingleOrDefault(g => g.GymCompanyId == id);

            if(gymCompany == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            gymCompany.Status = ConstantValues.Status.INACTIVE;
            _context.SaveChanges();
        }
    }
}

