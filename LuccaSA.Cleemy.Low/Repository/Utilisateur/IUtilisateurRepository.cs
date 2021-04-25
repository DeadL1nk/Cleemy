using LuccaSA.Cleemy.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuccaSA.Cleemy.Low
{
    public interface IUtilisateurRepository : IRepositoryBase<UtilisateurDB>
    {
        Task<UtilisateurDB> GetUtilisateurDBByNomPrenom(string p_nom, string p_prenom);
    }
}
