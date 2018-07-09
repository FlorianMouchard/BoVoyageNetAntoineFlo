using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyageNetAntoineFlo.Models
{
    public class DossierReservation
    {
        public int ID { get; set; }



        public string NumeroCarteBancaire { get; set; }

        public decimal PrixTotal { get; set; }

        public EtatDossierReservation EtatDossier { get; set; }


        //utiliser une fluent API?
        //public int ParticipantID { get; set; }
        //[ForeignKey("ParticipantID")]
        //public ICollection<Participant> Participants { get; set; }

        public int VoyageID { get; set; }
        [ForeignKey("VoyageID")]
        public Voyage Voyage { get; set; }
    }
}