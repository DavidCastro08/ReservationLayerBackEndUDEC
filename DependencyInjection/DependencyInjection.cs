using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservationArquitectureLayerUDEC.Infrastructure;
using ReservationArquitectureLayerUDEC.Infrastructure.Contracts.IReservations;
using ReservationArquitectureLayerUDEC.Infrastructure.Reservations;
using ReservationArquitectureLayerUDEC.LogicServices;
using ReservationArquitectureLayerUDEC.LogicServices.Contracts.IReservations;
using ReservationArquitectureLayerUDEC.LogicServices.Reservations;
using System;

namespace ReservationArquitectureLayerUDEC.DependencyInjection
{
    public class DependencyInjection
    {
        #region Profiles

        /// <summary>
        /// Registra el perfil de producción
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        /// <param name="configuration"></param>
        private const string _DBSETTING = "appsettings.json";

        public static object Core { get; private set; }

        public static void RegisterProfile(IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ReservationArquitectureLayerUDECContext>(options =>
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(_DBSETTING)
                .Build();
                string connectionString = "Server=LAPTOP-BFQK93ER;Database=ReservationArquitectureLayerUDEC;Trusted_Connection=True;";
                options.UseSqlServer(connectionString);
            });



            #region Application
            services.AddTransient<IReservations, Reservations>();
            #endregion

            #region Domain
            services.AddTransient<IReservationsInfrastructure, ReservationsInfrastructure>();
            #endregion Domain
        }

        #endregion Profiles

    }

}
