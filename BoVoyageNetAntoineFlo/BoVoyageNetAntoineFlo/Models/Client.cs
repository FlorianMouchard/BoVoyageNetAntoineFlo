using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoVoyageNetAntoineFlo.Models
{
    public class Client:Personne
    {
        [Required(ErrorMessage = "Le champ nom est obligatoire")]
        public string Email { get; set; }
    }
}