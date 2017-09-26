using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<GymCompany> GetGymCompanies()
        {
            return _context.GymCompany.ToList();
        }

        // GET /fitgymapi/gymcompanies/{id}
        public GymCompany GetGymCompany(int id)
        {
            var gymCompany = _context.GymCompany.SingleOrDefault(c => c.GymCompanyId == id);

            if(gymCompany == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return gymCompany;
        }


        // POST /fitgymapi/gymcompanies
        [HttpPost]
        public GymCompany CreateGymCompany(GymCompany gymCompany)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.GymCompany.Add(gymCompany);
            _context.SaveChanges();

            return gymCompany;
        }

        // PUT /fitgymapi/gymcompanies/{id}
        [HttpPut]
        public void UpdateGymCompany(int id, GymCompany gymCompany)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var gymCompanyInDb = _context.GymCompany.SingleOrDefault(g => g.GymCompanyId == id);

            if(gymCompanyInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            gymCompanyInDb.Name = gymCompany.Name;
            gymCompanyInDb.PhoneNumber = gymCompany.PhoneNumber;
            gymCompanyInDb.Password = gymCompany.Password;

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

