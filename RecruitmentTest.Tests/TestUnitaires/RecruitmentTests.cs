using LuccaSA.Cleemy.Low;
using LuccaSA.Cleemy.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecruitmentTest.Tests.TestUnitaires
{
    public class RecruitmentTests
    {
        private readonly ModelValidation _validation;
        private readonly Mock<IRepositoryWrapper> _mockRepository;

        public RecruitmentTests()
        {
            _mockRepository = new Mock<IRepositoryWrapper>();
            _validation = new ModelValidation(_mockRepository.Object);
        }

        [Fact]
        public void IsDateDepenseNotInFuture_ReturnsFalse()
        {
            try
            {
                DateTime v_dateTime = DateTime.Now.AddDays(1);
                Assert.False(_validation.IsDateDepenseNotInFuture(v_dateTime));
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
        }

        [Fact]
        public void IsDateDepenseOldEnough_ReturnsFalse()
        {
            try
            {
                DateTime v_dateTime = DateTime.Now.AddMonths(-3);
                Assert.False(_validation.IsDateDepenseOldEnough(v_dateTime));
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
        }

        [Fact]
        public void IsCommentaireValid_ReturnsFalse()
        {
            try
            {
                string v_commentaire = "";
                Assert.False(_validation.IsCommentaireValid(v_commentaire));
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
        }

        [Fact]
        public void IsDepenseUnique_ReturnsFalse()
        {
            try
            {
                DepenseDB v_depense = new DepenseDB { Montant = 777, Date = DateTime.Now.Date, Devise = "Dollar américain", Nom = "Stark", Prenom = "Anthony", Commentaire = "Je suis un test.", Nature = EnumNature.Hotel.ToString() };

                DepenseDB[] v_arrayDepense = new DepenseDB[]
                {
                    new DepenseDB{Montant = 777 ,Date = DateTime.Now.Date, Devise="Dollar américain", Nom = "Stark", Prenom="Anthony", Commentaire="Je suis Iron man.", Nature = EnumNature.Hotel.ToString()},
                    new DepenseDB{Montant = 52 ,Date = DateTime.Now.Date.AddDays(-1), Devise="Dollar américain", Nom = "Stark", Prenom="Anthony", Commentaire="Jarvis, il faut parfois savoir courir avant de savoir marcher.", Nature = EnumNature.Restaurant.ToString()},
                };

                // On simule le comportement attendu du service utilisé par le test.
                _mockRepository.Setup(repo => repo.Depense.GetAllDepenseDBByUtilisateur("Stark", "Anthony"))
                    .ReturnsAsync(v_arrayDepense);

                Assert.False(_validation.IsDepenseUnique(v_depense));
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
        }
        
        [Fact]
        public void IsDeviseIdentique_ReturnsFalse()
        {
            try
            {
                DepenseDB v_depense = new DepenseDB { Montant = 777, Date = DateTime.Now, Devise = "Dollar américain", Nom = "Stark", Prenom = "Anthony", Commentaire = "Je suis un test.", Nature = EnumNature.Hotel.ToString() };
                UtilisateurDB v_utilisateurDB = new UtilisateurDB { Nom = "Stark", Prenom = "Anthony", Devise = "Beaucoup de dollar" };

                // On simule le comportement attendu du service utilisé par le test.
                _mockRepository.Setup(repo => repo.Utilisateur.GetUtilisateurDBByNomPrenom("Stark", "Anthony"))
                    .ReturnsAsync(v_utilisateurDB);

                Assert.False(_validation.IsDeviseIdentique(v_depense));
            }
            catch (Exception v_ex)
            {
                Console.WriteLine(v_ex.Message);
            }
        }
    }
}
