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
    public class DestinationsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Destinations
        /// <summary>
        /// Retourne la liste des destinations
        /// </summary>
        /// <response code="200">Liste des destinations retournée</response>
        /// <returns></returns>
        public IQueryable<Destination> GetDestinations()
        {
            return db.Destinations;
        }

        // GET: api/Destinations/search
        /// <summary>
        /// Retourne la liste des destinations selon un ou plusieurs critères spécifiés
        /// </summary>
        /// <param name="continent"></param>>
        /// <param name="pays"></param>
        /// <param name="region"></param>
        /// <param name="destinationID"></param>
        /// <response code="200">Destinations affichées selon les critères spécifiés</response>
        /// <returns></returns>
        [Route("api/Destinations/search")]
        public IQueryable<Destination> GetSearch(string continent = "", string pays = "", string region = "", int? destinationID = null)
        {
            var query = db.Destinations.Where(x => x.ID > 0);

            if (destinationID != null)
                query = query.Where(x => x.ID == destinationID);
            if (!string.IsNullOrWhiteSpace(continent))
                query = query.Where(x => x.Continent.Contains(continent));
            if (!string.IsNullOrWhiteSpace(pays))
                query = query.Where(x => x.Pays.Contains(pays));
            if (!string.IsNullOrWhiteSpace(region))
                query = query.Where(x => x.Region.Contains(region));

            return query;
        }

        // GET: api/Destinations/5
        /// <summary>
        /// Retourne la liste des destinations selon leur ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Destination sélectionnée</response>
        /// <response code="404">Destination introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [ResponseType(typeof(Destination))]
        public IHttpActionResult GetDestination(int id)
        {
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return NotFound();
            }

            return Ok(destination);
        }

        // PUT: api/Destinations/5
        /// <summary>
        /// Modifie les attributs d'une destination (sélectionnée par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="destination"></param>
        /// <response code="200">Destination modifiée</response>
        /// <response code="400">Erreur dans la modification des attributs, ou destination introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDestination(int id, Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != destination.ID)
            {
                return BadRequest();
            }

            db.Entry(destination).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
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

        // POST: api/Destinations
        /// <summary>
        /// Ajoute une nouvelle destination
        /// </summary>
        /// <param name="destination"></param>
        /// <response code="200">Destination créée</response>
        /// <response code="400">Erreur dans la saisie des attributs</response>
        /// <returns></returns>
        [ResponseType(typeof(Destination))]
        public IHttpActionResult PostDestination(Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Destinations.Add(destination);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = destination.ID }, destination);
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DestinationExists(int id)
        {
            return db.Destinations.Count(e => e.ID == id) > 0;
        }
    }
}