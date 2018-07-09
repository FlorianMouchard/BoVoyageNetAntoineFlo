using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyageNetAntoineFlo.Models
{
    public class Voyage
    {
        public int IDVoyage { get; set; }

        public DateTime DateAller { get; set; }

        public DateTime DateRetour { get; set; }

        public int PlacesDisponibles { get; set; }

        public decimal TarifToutCompris { get; set; }
    }
}