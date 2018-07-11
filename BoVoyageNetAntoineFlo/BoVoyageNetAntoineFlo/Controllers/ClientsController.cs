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
    [RoutePrefix("api/clients")]
    public class ClientsController : ApiController
    {
        
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Clients
        /// <summary>
        /// Retourne la liste des clients
        /// </summary>
        /// <response code="200">Liste des clients retournée</response>
        /// <returns></returns>
        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        // GET: api/Clients/search
        /// <summary>
        /// Retourne la liste des clients selon un ou plusieurs critères spécifiés
        /// </summary>
        /// <param name="email"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="telephone"></param>
        /// <param name="adresse"></param>
        /// <param name="clientID"></param>
        /// <response code="200">Clients affichés selon les critères spécifiés</response>
        /// <returns></returns>

        [Route("search")]
        public IQueryable<Client> GetSearch(string email = "", string nom = "", string prenom = "", string telephone = "", string adresse ="", int? clientID = null)
        {
            var query = db.Clients.Where(x => x.ID > 0);

            if (clientID != null)
                query = query.Where(x => x.ID == clientID);
            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(x => x.Email.Contains(email));
            if (!string.IsNullOrWhiteSpace(nom))
                query = query.Where(x => x.Nom.Contains(nom));
            if (!string.IsNullOrWhiteSpace(prenom))
                query = query.Where(x => x.Prenom.Contains(prenom));
            if (!string.IsNullOrWhiteSpace(telephone))
                query = query.Where(x => x.Telephone.Contains(telephone));
            if (!string.IsNullOrWhiteSpace(adresse))
                query = query.Where(x => x.Adresse.Contains(adresse));

            return query;
        }

        // GET: api/Clients/5
        /// <summary>
        /// Retourne la liste des clients selon leur ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Client sélectionné</response>
        /// <response code="404">Client introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        /// <summary>
        /// Modifie les attributs d'un client (sélectionné par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <response code="200">Client modifié</response>
        /// <response code="400">Erreur dans la modification des attributs, ou client introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ID)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        /// <summary>
        /// Ajoute un nouveau client
        /// </summary>
        /// <param name="client"></param>
        /// <response code="200">Client créé</response>
        /// <response code="400">Erreur dans la saisie des attributs</response>
        /// <returns></returns>
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ID }, client);
        }

        // DELETE: api/Clients/5
        /// <summary>
        /// Supprime un client (sélectionné par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Client supprimé</response>
        /// <response code="404">Client introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ID == id) > 0;
        }
    }
}