using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaSA.Cleemy.Model
{
    public class DepenseGET
    {
        [FromRoute]
        public string nom { get; set; }
        [FromRoute]
        public string prenom { get; set; }
        [FromQuery]
        public string orderBy { get; set; } = "Montant";
    }
}
