using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageNetAntoineFlo.Models
{
    public class Voyage
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public DateTime DateAller { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public DateTime DateRetour { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public int PlacesDisponibles { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public decimal TarifToutCompris { get; set; }

        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public int DestinationID { get; set; }
        [ForeignKey("DestinationID")]
        public Destination Destination { get; set; }
    }
}