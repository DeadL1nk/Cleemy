using LuccaSA.Cleemy.Low;
using LuccaSA.Cleemy.Low.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuccaSA.Cleemy.RecruitmentTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost p_host)
        {
            using (IServiceScope v_scope = p_host.Services.CreateScope())
            {
                IServiceProvider services = v_scope.ServiceProvider;
                try
                {
                    //On r�cup�re une instance du contexte de base de donn�es avec l'injection de d�pendance. 
                    RecruitmentContext v_context = services.GetRequiredService<RecruitmentContext>();

                    //La premi�re fois que l�application est ex�cut�e, la base de donn�es est cr��e et charg�e avec les donn�es de test.
                    DbInitializer.Initialize(v_context);
                }
                catch (Exception v_ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(v_ex, "Une erreur s'est produite en cr�ant la base de donn�e.");
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
