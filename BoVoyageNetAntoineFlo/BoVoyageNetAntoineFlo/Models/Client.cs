using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyageNetAntoineFlo.Models
{
    public class Client
    {
        public int IDClient { get; set; }

        public string Civilite { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Adresse { get; set; }

        public string Telephone { get; set; }

        public DateTime DateNaissance { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }
}