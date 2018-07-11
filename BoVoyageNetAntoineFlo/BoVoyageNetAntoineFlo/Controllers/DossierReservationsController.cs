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
    public class DossierReservationsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/DossierReservations
        /// <summary>
        /// Retourne la liste des dossiers de réservation
        /// </summary>
        /// <returns></returns>
        public IQueryable<DossierReservation> GetDossiersReservations()
        {
            return db.DossiersReservations;
        }

        // GET: api/DossierReservations/search
        /// <summary>
        /// Retourne la liste des dossiers de réservation selon un ou plusieurs critères spécifiés
        /// </summary>
        /// <param name="numeroCarte"></param>>
        /// <param name="dossierID"></param>
        /// <returns></returns>
        [Route("api/DossierReservations/search")]
        public IQueryable<DossierReservation> GetSearch(string numeroCarte = "", int? dossierID = null)
        {
            var query = db.DossiersReservations.Where(x => x.ID > 0);

            if (dossierID != null)
                query = query.Where(x => x.ID == dossierID);
            if (!string.IsNullOrWhiteSpace(numeroCarte))
                query = query.Where(x => x.NumeroCarteBancaire.Contains(numeroCarte));

            return query;
        }

        // GET: api/DossierReservations/5
        /// <summary>
        /// Retourne la liste des dossiers de réservation selon leur ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult GetDossierReservation(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return NotFound();
            }

            return Ok(dossierReservation);
        }

        // PUT: api/DossierReservations/5
        /// <summary>
        /// Modifie les attributs d'un dossier de réservation (sélectionné par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dossierReservation"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDossierReservation(int id, DossierReservation dossierReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dossierReservation.ID)
            {
                return BadRequest();
            }

            db.Entry(dossierReservation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DossierReservationExists(id))
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

        // POST: api/DossierReservations
        /// <summary>
        /// Ajoute un nouveau dossier de réservation
        /// </summary>
        /// <param name="dossierReservation"></param>
        /// <returns></returns>
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult PostDossierReservation(DossierReservation dossierReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DossiersReservations.Add(dossierReservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dossierReservation.ID }, dossierReservation);
        }

        // DELETE: api/DossierReservations/5
        /// <summary>
        /// Supprime un dossier de réservation (sélectionné par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult DeleteDossierReservation(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return NotFound();
            }

            db.DossiersReservations.Remove(dossierReservation);
            db.SaveChanges();

            return Ok(dossierReservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DossierReservationExists(int id)
        {
            return db.DossiersReservations.Count(e => e.ID == id) > 0;
        }
    }
}