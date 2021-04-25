using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaSA.Cleemy.Model
{
    public class UtilisateurDTO
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public virtual ICollection<DepenseDTO> Depenses { get; set; }
        public override string ToString()
        {
            return "Nom : " + Nom + Environment.NewLine
             + " Prenom : " + Prenom + Environment.NewLine;

        }
    }
}
