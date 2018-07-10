using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageNetAntoineFlo.Models
{
    public class DossierReservation
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public string NumeroCarteBancaire { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public decimal PrixTotal { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public EtatDossierReservation EtatDossier { get; set; }


        //utiliser une fluent API?
        //public int ParticipantID { get; set; }
        //[ForeignKey("ParticipantID")]
        //public ICollection<Participant> Participants { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public int VoyageID { get; set; }
        [ForeignKey("VoyageID")]
        public Voyage Voyage { get; set; }
    }
}