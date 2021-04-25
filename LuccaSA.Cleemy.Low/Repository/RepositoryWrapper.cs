using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuccaSA.Cleemy.Low
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RecruitmentContext _rectruitmentContext;
        
        private IDepenseRepository _depense;
        private IUtilisateurRepository _utilisateur;

        public RepositoryWrapper(RecruitmentContext repositoryContext)
        {
            _rectruitmentContext = repositoryContext;
        }

        public IDepenseRepository Depense
        {
            get
            {
                if (_depense == null)
                {
                    _depense = new DepenseRepository(_rectruitmentContext);
                }
                return _depense;
            }
        }
        public IUtilisateurRepository Utilisateur
        {
            get
            {
                if (_utilisateur == null)
                {
                    _utilisateur = new UtilisateurRepository(_rectruitmentContext);
                }
                return _utilisateur;
            }
        }
        
        public async Task SaveAsync()
        {
            await _rectruitmentContext.SaveChangesAsync();
        }
    }
}
