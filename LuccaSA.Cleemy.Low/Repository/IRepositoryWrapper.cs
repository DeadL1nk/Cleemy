using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuccaSA.Cleemy.Low
{
    //Permet de manipuler nos entités
    public interface IRepositoryWrapper
    {
        IDepenseRepository Depense { get; }
        IUtilisateurRepository Utilisateur { get; }
        Task SaveAsync();
    }
}
