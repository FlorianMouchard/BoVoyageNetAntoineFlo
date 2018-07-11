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
        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        // GET: api/Clients/search
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