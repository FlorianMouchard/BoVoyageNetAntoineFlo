﻿using System;
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
    [RoutePrefix("api/Voyages")]
    public class VoyagesController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Voyages
        /// <summary>
        /// Retourne la liste des voyages
        /// </summary>
        /// <response code="200">Liste des voyages retournée</response>
        /// <returns></returns>
        public IQueryable<Voyage> GetVoyages()
        {
            return db.Voyages.Include(x => x.Destination);
        }

        //GET: api/Voyages/search
        /// <summary>
        /// Retourne la liste des voyages selon un ou plusieurs critères spécifiés
        /// </summary>
        /// <param name="dateAller"></param>
        /// <param name="dateRetour"></param>
        /// <param name="destinationID"></param>
        /// <response code="200">Voyages affichés selon les critères spécifiés</response>
        /// <returns></returns>
        [Route("search")]
        public IQueryable<Voyage> GetSearch(DateTime? dateAller = null, DateTime? dateRetour = null, int? destinationID = null)
        {
            var query = db.Voyages.Where(x => x.PlacesDisponibles > 0);

            if (destinationID != null)
                query = query.Where(x => x.DestinationID == destinationID);
            if (dateAller != null)
                query = query.Where(x => x.DateAller == dateAller);
            if (dateRetour != null)
                query = query.Where(x => x.DateRetour == dateRetour);

            return query;
        }
        // GET: api/Voyages/5
        /// <summary>
        /// Retourne la liste des voyages selon leur ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Voyage sélectionné</response>
        /// <response code="404">Voyage introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult GetVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            return Ok(voyage);
        }


        // PUT: api/Voyages/5
        /// <summary>
        /// Modifie les attributs d'un voyage (sélectionné par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="voyage"></param>
        /// <response code="200">Voyage modifié</response>
        /// <response code="400">Erreur dans la modification des attributs, ou voyage introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutVoyage(int id, Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voyage.ID)
            {
                return BadRequest();
            }

            db.Entry(voyage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageExists(id))
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

        // POST: api/Voyages
        /// <summary>
        /// Ajoute un nouveau voyage
        /// </summary>
        /// <param name="voyage"></param>
        /// <response code="200">Voyage créé</response>
        /// <response code="400">Erreur dans la saisie des attributs</response>
        /// <returns></returns>
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult PostVoyage(Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Voyages.Add(voyage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = voyage.ID }, voyage);
        }

        // DELETE: api/Voyages/5
        /// <summary>
        /// Supprime un voyage (sélectionné par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Voyage supprimé</response>
        /// <response code="404">Voyage introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [ResponseType(typeof(Voyage))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            db.Voyages.Remove(voyage);
            db.SaveChanges();

            return Ok(voyage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoyageExists(int id)
        {
            return db.Voyages.Count(e => e.ID == id) > 0;
        }
    }
}