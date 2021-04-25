using LuccaSA.Cleemy.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuccaSA.Cleemy.Low
{
    public interface IDepenseRepository : IRepositoryBase<DepenseDB>
    {
        void AddDepenseDB(DepenseDB p_depenseDB);
        
        Task<IEnumerable<DepenseDB>> GetAllDepenseDB();
        Task<DepenseDB> GetDepenseDBById(long p_id);
        Task<IEnumerable<DepenseDB>> GetAllDepenseDBByUtilisateur(string p_nom, string p_prenom);
    }
}
