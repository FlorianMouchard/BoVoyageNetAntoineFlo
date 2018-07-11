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
    [RoutePrefix("api/Participants")]
    public class ParticipantsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Participants
        /// <summary>
        /// Retourne la liste des participants
        /// </summary>
        /// <response code="200">Liste des participants retournée</response>
        /// <returns></returns>
        public IQueryable<Participant> GetParticipants()
        {
            return db.Participants;
        }

        // GET: api/Participants/search
        /// <summary>
        /// Retourne la liste des participants selon un ou plusieurs critères spécifiés
        /// </summary>
        /// <param name="reduction"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="telephone"></param>
        /// <param name="adresse"></param>
        /// <param name="participantID"></param>
        /// <response code="200">Participants affichés selon les critères spécifiés</response>
        /// <returns></returns>
        [Route("search")]
        public IQueryable<Participant> GetSearch(float? reduction = null, string nom = "", string prenom = "", string telephone = "", string adresse = "", int? participantID = null)
        {
            var query = db.Participants.Where(x => x.ID > 0);

            if (participantID != null)
                query = query.Where(x => x.ID == participantID);
            if (reduction != null)
                query = query.Where(x => x.Reduction == reduction);
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

        // GET: api/Participants/5
        /// <summary>
        /// Retourne la liste des participants selon leur ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Participant sélectionné</response>
        /// <response code="404">Participant introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Participant))]
        public IHttpActionResult GetParticipant(int id)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            return Ok(participant);
        }

        // PUT: api/Participants/5
        /// <summary>
        /// Modifie les attributs d'un participant (sélectionné par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="participant"></param>
        /// <response code="200">Participant modifié</response>
        /// <response code="400">Erreur dans la modification des attributs, ou participant introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipant(int id, Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participant.ID)
            {
                return BadRequest();
            }

            db.Entry(participant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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

        // POST: api/Participants
        /// <summary>
        /// Ajoute un nouveau participant
        /// </summary>
        /// <param name="participant"></param>
        /// <response code="200">Participant créé</response>
        /// <response code="400">Erreur dans la saisie des attributs</response>
        /// <returns></returns>
        [ResponseType(typeof(Participant))]
        public IHttpActionResult PostParticipant(Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Participants.Add(participant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = participant.ID }, participant);
        }

        // DELETE: api/Participants/5
        /// <summary>
        /// Supprime un participant (sélectionné par son ID)
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Participant supprimé</response>
        /// <response code="404">Participant introuvable pour l'ID spécifié</response>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Participant))]
        public IHttpActionResult DeleteParticipant(int id)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            db.Participants.Remove(participant);
            db.SaveChanges();

            return Ok(participant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParticipantExists(int id)
        {
            return db.Participants.Count(e => e.ID == id) > 0;
        }
    }
}