using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BoVoyageNetAntoineFlo.Data;
using BoVoyageNetAntoineFlo.Models;

namespace BoVoyageNetAntoineFlo.Controllers
{
    public class AssuranceAnnulationsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/AssuranceAnnulations
        public IQueryable<AssuranceAnnulation> GetAssurancesAnnulations()
        {
            return db.AssurancesAnnulations;
        }

        // GET: api/AssuranceAnnulations/5
        [ResponseType(typeof(AssuranceAnnulation))]
        public IHttpActionResult GetAssuranceAnnulation(int id)
        {
            AssuranceAnnulation assuranceAnnulation = db.AssurancesAnnulations.Find(id);
            if (assuranceAnnulation == null)
            {
                return NotFound();
            }

            return Ok(assuranceAnnulation);
        }

        // PUT: api/AssuranceAnnulations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssuranceAnnulation(int id, AssuranceAnnulation assuranceAnnulation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assuranceAnnulation.ID)
            {
                return BadRequest();
            }

            db.Entry(assuranceAnnulation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssuranceAnnulationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AssuranceAnnulations
        [ResponseType(typeof(AssuranceAnnulation))]
        public IHttpActionResult PostAssuranceAnnulation(AssuranceAnnulation assuranceAnnulation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AssurancesAnnulations.Add(assuranceAnnulation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assuranceAnnulation.ID }, assuranceAnnulation);
        }

        // DELETE: api/AssuranceAnnulations/5
        [ResponseType(typeof(AssuranceAnnulation))]
        public IHttpActionResult DeleteAssuranceAnnulation(int id)
        {
            AssuranceAnnulation assuranceAnnulation = db.AssurancesAnnulations.Find(id);
            if (assuranceAnnulation == null)
            {
                return NotFound();
            }

            db.AssurancesAnnulations.Remove(assuranceAnnulation);
            db.SaveChanges();

            return Ok(assuranceAnnulation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssuranceAnnulationExists(int id)
        {
            return db.AssurancesAnnulations.Count(e => e.ID == id) > 0;
        }
    }
}