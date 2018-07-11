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
    [RoutePrefix("api/AssuranceAnnulations")]
    public class AssuranceAnnulationsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/AssuranceAnnulations
        /// <summary>
        /// Retourne la liste des assurances Annulation
        /// </summary>
        /// <response code="200">Liste des assurances Annulation retournée</response>
        /// <returns></returns>
        public IQueryable<AssuranceAnnulation> GetAssurancesAnnulations()
        {
            return db.AssurancesAnnulations;
        }

        // GET: api/AssuranceAnnulations/5
        /// <summary>
        /// Retourne la liste des assurances Annulation selon leur ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Assurance Annulation sélectionnée</response>
        /// <response code="404">Assurance Annulation introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
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
        /// <summary>
        /// Modifie les attributs d'une assurance Annulation (sélectionnée par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="assuranceAnnulation"></param>
        /// <response code="200">Assurance Annulation modifiée</response>
        /// <response code="400">Erreur dans la modification des attributs, ou assurance Annulation introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
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
        /// <summary>
        /// Ajoute une nouvelle assurance Annulation
        /// </summary>
        /// <param name="assuranceAnnulation"></param>
        /// <response code="200">Assurance annulation créée</response>
        /// <response code="400">Erreur dans la saisie des attributs</response>
        /// <returns></returns>
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
        /// <summary>
        /// Supprime une assurance Annulation (sélectionnée par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Assurance Annulation supprimée</response>
        /// <response code="404">Assurance Annulation introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
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