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
    public class AgenceVoyagesController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/AgenceVoyages
        /// <summary>
        /// Retourne la liste des agences de voyages
        /// </summary>
        /// <response code="200">Liste des agences de voyages retournée</response>
        /// <returns></returns>
        public IQueryable<AgenceVoyage> GetAgencesVoyages()
        {
            return db.AgencesVoyages;
        }

        // GET: api/AgenceVoyages/5
        /// <summary>
        /// Retourne la liste des agences de voyages selon leur ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Agence de voyages sélectionnée</response>
        /// <response code="404">Agence de voyages introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [ResponseType(typeof(AgenceVoyage))]
        public IHttpActionResult GetAgenceVoyage(int id)
        {
            AgenceVoyage agenceVoyage = db.AgencesVoyages.Find(id);
            if (agenceVoyage == null)
            {
                return NotFound();
            }

            return Ok(agenceVoyage);
        }

        // PUT: api/AgenceVoyages/5
        /// <summary>
        /// Modifie les attributs d'une agence de voyages (sélectionnée par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenceVoyage"></param>
        /// <response code="200">Agence de voyages modifiée</response>
        /// <response code="400">Erreur dans la modification des attributs, ou agence de voyages introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAgenceVoyage(int id, AgenceVoyage agenceVoyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agenceVoyage.ID)
            {
                return BadRequest();
            }

            db.Entry(agenceVoyage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgenceVoyageExists(id))
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

        // POST: api/AgenceVoyages
        /// <summary>
        /// Ajoute une nouvelle agence de voyages
        /// </summary>
        /// <param name="agenceVoyage"></param>
        /// <response code="200">Agence de voyages créée</response>
        /// <response code="400">Erreur dans la saisie des attributs</response>
        /// <returns></returns>
        [ResponseType(typeof(AgenceVoyage))]
        public IHttpActionResult PostAgenceVoyage(AgenceVoyage agenceVoyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AgencesVoyages.Add(agenceVoyage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agenceVoyage.ID }, agenceVoyage);
        }

        // DELETE: api/AgenceVoyages/5
        /// <summary>
        /// Supprime une agence de voyages (sélectionnée par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Agence de voyages supprimée</response>
        /// <response code="400">Voyage introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [ResponseType(typeof(AgenceVoyage))]
        public IHttpActionResult DeleteAgenceVoyage(int id)
        {
            AgenceVoyage agenceVoyage = db.AgencesVoyages.Find(id);
            if (agenceVoyage == null)
            {
                return NotFound();
            }

            db.AgencesVoyages.Remove(agenceVoyage);
            db.SaveChanges();

            return Ok(agenceVoyage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgenceVoyageExists(int id)
        {
            return db.AgencesVoyages.Count(e => e.ID == id) > 0;
        }
    }
}