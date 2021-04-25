using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaSA.Cleemy.Model
{
    public class DepenseDTO
    {
        public DateTime Date { get; set; }
        public string Nature { get; set; }
        public decimal Montant { get; set; }
        public string Devise { get; set; }
        public string Commentaire { get; set; }
        public string NomPrenomUtilisateur { get; set; }
        public override string ToString()
        {
            return "NomPrenomUtilisateur : " + NomPrenomUtilisateur + Environment.NewLine
            + "Date : " + Date + Environment.NewLine
            + "Nature : " + Nature + Environment.NewLine
            + "Montant : " + Montant + Environment.NewLine
            + "Devise : " + Devise + Environment.NewLine
            + "Commentaire : " + Commentaire + Environment.NewLine;
        }
    }
}
