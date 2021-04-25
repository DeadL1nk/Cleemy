using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LuccaSA.Cleemy.Model
{
    public class UtilisateurDB
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Devise { get; set; }

        public virtual  ICollection<DepenseDB> Depenses { get; set; }
        public override string ToString()
        {
            return "Nom : " + Nom + Environment.NewLine
             + " Prenom : " + Prenom + Environment.NewLine
             + " Devise : " + Devise + Environment.NewLine;

        }
    }
}
