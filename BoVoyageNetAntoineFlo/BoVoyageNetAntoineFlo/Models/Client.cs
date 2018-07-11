using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageNetAntoineFlo.Models
{
    public class Client:Personne
    {
        [Required(ErrorMessage = "L'email est obligatoire")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$", ErrorMessage ="Le format de l'email est invalide")]
        public string Email { get; set; }
    }
}