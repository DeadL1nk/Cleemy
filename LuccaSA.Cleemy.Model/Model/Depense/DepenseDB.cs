using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LuccaSA.Cleemy.Model
{
    [Table("Depense")]
    public class DepenseDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DepenseID { get; set; }
        public DateTime Date { get; set; }
        public decimal Montant { get; set; }
        public string Nature { get; set; }
        public string Devise { get; set; }
        public string Commentaire { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [ForeignKey("Nom, Prenom")]
        public virtual UtilisateurDB Utilisateur { get; set; }

        public override string ToString()
        {
            return "DepenseID : " + DepenseID + Environment.NewLine
            + " Date : " + Date + Environment.NewLine
            + " Nature : " + Nature + Environment.NewLine
            + " Montant : " + Montant + Environment.NewLine
            + " Devise : " + Devise + Environment.NewLine
            + " Commentaire : " + Commentaire + Environment.NewLine
            + " Utilisateur : " + Nom + " " + Prenom + Environment.NewLine;
        }
    }
}
