using LuccaSA.Cleemy.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuccaSA.Cleemy.Low
{
    public class RecruitmentContext : DbContext
    {
        public RecruitmentContext(DbContextOptions<RecruitmentContext> options) : base(options)
        {
        }

        public DbSet<DepenseDB> Depenses { get; set; }
        public DbSet<UtilisateurDB> Utilisateurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Table Depense
            modelBuilder.Entity<DepenseDB>().ToTable("Depense");

            //Table Utilisateur
            modelBuilder.Entity<UtilisateurDB>().ToTable("Utilisateur");
            //Clé primaire Utilisateur
            modelBuilder.Entity<UtilisateurDB>().HasKey(t => new { t.Nom, t.Prenom });
        }

    }
}
