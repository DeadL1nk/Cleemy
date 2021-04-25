using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LuccaSA.Cleemy.Model
{
    public class DepensePOST
    {
        [Required(ErrorMessage = "NomUtilisateur is required")]
        public string NomUtilisateur { get; set; }
        [Required(ErrorMessage = "PrenomUtilisateur is required")]
        public string PrenomUtilisateur { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public DateTimeOffset Date { get; set; }
        [Required(ErrorMessage = "Nature is required")]
        public string Nature { get; set; }
        [Required(ErrorMessage = "NomUtilisateur is required")]
        public decimal Montant { get; set; }
        public string Devise { get; set; }
        [Required(ErrorMessage = "Commentaire is required")]
        public string Commentaire { get; set; }
    }
}
