using LuccaSA.Cleemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuccaSA.Cleemy.Low.Business
{
    public static class DbInitializer
    {
        public static void Initialize(RecruitmentContext p_context)
        {
            //On créer automatiquement la base de données.
            p_context.Database.EnsureCreated();

            // S'il y a déjà des utilisateurs on s'arrête là.
            if (p_context.Utilisateurs.Any())
            {
                return;  
            }

            //Sinon on les ajoute en base.
            UtilisateurDB[] v_arrayUtilisateurs = new UtilisateurDB[]
            {
                new UtilisateurDB{Nom = "Stark", Prenom="Anthony", Devise="Dollar américain"},
                new UtilisateurDB { Nom = "Romanova", Prenom = "Natasha", Devise = "Rouble russe" }
            };

            //Dépenses d'exemples
            //DepenseDB[] v_arrayDepense = new DepenseDB[]
            //{
            //    new DepenseDB{Montant = 999 ,Date = DateTime.Now.AddMonths(-2), Devise="Dollar américain", Nom = "Stark", Prenom="Anthony", Commentaire="Je suis Iron man.", Nature = EnumNature.Hotel.ToString()},
            //    new DepenseDB{Montant = 52 ,Date = DateTime.Now, Devise="Dollar américain", Nom = "Stark", Prenom="Anthony", Commentaire="Jarvis, il faut parfois savoir courir avant de savoir marcher.", Nature = EnumNature.Restaurant.ToString()},
            //};
            //foreach (DepenseDB s in v_arrayDepense)
            //{
            //    p_context.Depenses.Add(s);
            //}

            foreach (UtilisateurDB s in v_arrayUtilisateurs)
            {
                p_context.Utilisateurs.Add(s);
            }
            
            p_context.SaveChanges();
        }
    }
}
