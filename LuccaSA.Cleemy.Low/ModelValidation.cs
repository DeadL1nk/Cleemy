using LuccaSA.Cleemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaSA.Cleemy.Low
{
    public class ModelValidation
    {
        private IRepositoryWrapper _repository;

        public ModelValidation(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public string DepenseDBIsValid(DepenseDB p_depense)
        {
            string v_message = null;
            try
            {
                if (p_depense != null)
                {
                    if (_repository.Utilisateur.GetUtilisateurDBByNomPrenom(p_depense.Nom, p_depense.Prenom).Result == null)
                    {
                        v_message = "Aucun utilisateur correpondant. ";
                    }
                    if (!this.IsNatureValid(p_depense.Nature))
                    {
                        v_message += "La nature de la dépense doit être une de ces valeurs : " + Enum.GetValues(typeof(EnumNature));
                    }
                    if (!this.IsDateDepenseNotInFuture(p_depense.Date))
                    {
                        v_message += "Une dépense ne peut pas avoir une date dans le futur. ";
                    }
                    else if (!this.IsDateDepenseOldEnough(p_depense.Date))
                    {
                        v_message += "Une dépense ne peut pas être datée de plus de 3 mois. ";
                    }

                    if (!this.IsCommentaireValid(p_depense.Commentaire))
                    {
                        v_message += "Le commentaire est obligatoire. ";
                    }

                    if (!this.IsDepenseUnique(p_depense))
                    {
                        v_message += "Un utilisateur ne peut pas déclarer deux fois la même dépense(même date et même montant). ";
                    }

                    if (!this.IsDeviseIdentique(p_depense))
                    {
                        v_message += "La devise de la dépense doit être identique à celle de l'utilisateur. ";
                    }
                }
                else
                {
                    v_message = "La dépense est null.";
                }
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
            return v_message;
        }
        
        public bool IsNatureValid(string p_nature)
        {
            bool v_isValid = true;
            try
            {
                if (!Enum.IsDefined(typeof(EnumNature), p_nature))
                {
                    v_isValid = false;
                }
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
            return v_isValid;
        }
        public bool IsDateDepenseNotInFuture(DateTime p_date)
        {
            bool v_isValid = true;
            try
            {
                //Une dépense ne peut pas avoir une date dans le futur
                if (p_date > DateTime.Now)
                {
                    v_isValid = false;
                }
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
            return v_isValid;
        }
        public bool IsDateDepenseOldEnough(DateTime p_date)
        {
            bool v_isValid = true;
            try
            {
                //Une dépense ne peut pas être datée de plus de 3 mois
                if (p_date < DateTime.Now.Date.AddMonths(-3))
                {
                    v_isValid = false;
                }
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
            return v_isValid;
        }
        public bool IsCommentaireValid(string p_commentaire)
        {
            bool v_isValid = true;
            try
            {
                //Le commentaire est obligatoire
                if (string.IsNullOrWhiteSpace(p_commentaire))
                {
                    v_isValid = false;
                }
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
            return v_isValid;
        }
        public bool IsDepenseUnique(DepenseDB p_depense)
        {
            bool v_isValid = true;
            try
            {
                //Un utilisateur ne peut pas déclarer deux fois la même dépense(même date et même montant),
                Task<IEnumerable<DepenseDB>> v_listDepenseDB = _repository.Depense.GetAllDepenseDBByUtilisateur(p_depense.Nom, p_depense.Prenom);
                if (v_listDepenseDB.Result.Where(e => e.Date == p_depense.Date && e.Montant == p_depense.Montant).Count() > 0)
                {
                    v_isValid = false;
                }
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
            return v_isValid;
        }
        public bool IsDeviseIdentique(DepenseDB p_depense)
        {
            bool v_isValid = true;
            try
            {
                //La devise de la dépense doit être identique à celle de l'utilisateur.
                Task<UtilisateurDB> v_utilisateurDB = _repository.Utilisateur.GetUtilisateurDBByNomPrenom(p_depense.Nom, p_depense.Prenom);
                if (v_utilisateurDB.Result == null || !string.Equals(p_depense.Devise, v_utilisateurDB.Result.Devise))
                {
                    v_isValid = false;
                }
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
            return v_isValid;
        }


    }
}
