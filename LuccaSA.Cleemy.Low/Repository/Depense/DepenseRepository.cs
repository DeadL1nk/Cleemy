using LuccaSA.Cleemy.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaSA.Cleemy.Low
{
    public class DepenseRepository : RepositoryBase<DepenseDB>, IDepenseRepository
    {
        public DepenseRepository(RecruitmentContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<DepenseDB>> GetAllDepenseDB()
        {
            return await FindAll().Include(ac => ac.Utilisateur)
                .ToListAsync();
        }
        public async Task<DepenseDB> GetDepenseDBById(long p_id)
        {
            return await FindByCondition(e => e.DepenseID == p_id).Include(ac => ac.Utilisateur)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<DepenseDB>> GetAllDepenseDBByUtilisateur(string p_nom, string p_prenom)
        {
            return await FindByCondition(e => e.Utilisateur.Nom == p_nom && e.Utilisateur.Prenom == p_prenom)
                .Include(ac => ac.Utilisateur)
                .ToListAsync();
        }

        public void AddDepenseDB(DepenseDB p_depenseDB)
        {
            Create(p_depenseDB);
        }
    }
}
