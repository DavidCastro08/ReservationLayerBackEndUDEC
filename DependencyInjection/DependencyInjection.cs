using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservationLinKtic.Infrastructure;
using ReservationLinKtic.Infrastructure.Contracts.IReservations;
using ReservationLinKtic.Infrastructure.Reservations;
using ReservationLinKtic.LogicServices;
using ReservationLinKtic.LogicServices.Contracts.IReservations;
using ReservationLinKtic.LogicServices.Reservations;
using System;

namespace ReservationLinKtic.DependencyInjection
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

            services.AddDbContext<ReservationLinkTicContext>(options =>
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(_DBSETTING)
                .Build();
                string connectionString = "Server=LAPTOP-BFQK93ER;Database=ReservationLinkTic;Trusted_Connection=True;";
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
