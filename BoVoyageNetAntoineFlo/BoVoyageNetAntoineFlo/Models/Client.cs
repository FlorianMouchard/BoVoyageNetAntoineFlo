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
        // Format email
        public string Email { get; set; }
    }
}