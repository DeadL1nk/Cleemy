using LuccaSA.Cleemy.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuccaSA.Cleemy.Low
{
    public class UtilisateurRepository : RepositoryBase<UtilisateurDB>, IUtilisateurRepository
    {
        public UtilisateurRepository(RecruitmentContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<UtilisateurDB> GetUtilisateurDBByNomPrenom(string p_nom, string p_prenom)
        {
            return await FindByCondition(e => e.Nom == p_nom && e.Prenom == p_prenom)
                .Include(e=> e.Depenses)
                .FirstOrDefaultAsync();
        }

    }
}
