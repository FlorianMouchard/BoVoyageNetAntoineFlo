using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyageNetAntoineFlo.Models
{
    public abstract class Personne
    {
        public int ID { get; set; }
        
        public string Civilite { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Adresse { get; set; }

        public string Telephone { get; set; }

        public DateTime DateNaissance { get; set; }

        public int Age {

            get
            {
                return DateTime.Now.Year - DateNaissance.Year -
                         (DateTime.Now.Month < DateNaissance.Month ? 1 :
                         DateTime.Now.Day < DateNaissance.Day ? 1 : 0);
            }
        }
    }
}