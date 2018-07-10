using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageNetAntoineFlo.Models
{
    public abstract class Personne
    {
        public int ID { get; set; }

        
        [Required(ErrorMessage = "La civilité est obligatoire")]
        // Menu déroulant (M / Madame)
        public string Civilite { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'adresse est obligatoire")]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Le téléphone est obligatoire")]
        // Mettre regex
        public string Telephone { get; set; }

        [Required(ErrorMessage = "La date de naissance est obligatoire")]
        // Format date
        public DateTime DateNaissance { get; set; }

        public int Age {

            get
            {
                return DateTime.Now.Year - DateNaissance.Year -
                         (DateTime.Now.Month < DateNaissance.Month ? 1 :
                         (DateTime.Now.Month == DateNaissance.Month && DateTime.Now.Day < DateNaissance.Day) ? 1 : 0);
            }
        }
    }
    
}