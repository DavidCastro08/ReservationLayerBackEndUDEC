using Microsoft.EntityFrameworkCore;
using ReservationArquitectureLayerUDEC.Domain.Reservations;
using ReservationArquitectureLayerUDEC.Infrastructure.Contracts.IReservations;
using System;
using System.Linq;

namespace ReservationArquitectureLayerUDEC.Infrastructure.Reservations
{
    public class ReservationsInfrastructure: IReservationsInfrastructure
    {
        #region Fileds
        private readonly ReservationArquitectureLayerUDECContext Context;
        #endregion Fields

        #region Constants

        #endregion Constants

        #region Builder
        public ReservationsInfrastructure(ReservationArquitectureLayerUDECContext _Context) : base()
        {
            Context = _Context;
        }
        #endregion Builder

        public void SaveReservation(SaveReservationsDTO saveReservationsDTO)
        {
            Reserva registerToSave = MapperFromSaveReservationsDTOToReservas(saveReservationsDTO);
            Context.Reservas.Add(registerToSave);
            Context.SaveChanges();
        }

        public void UpdateReservation(SaveReservationsDTO updateReservationsDTO)
        {
            Reserva registerToSave = MapperFromSaveReservationsDTOToReservas(updateReservationsDTO);
            Context.Reservas.Update(registerToSave);
            Context.SaveChanges();
        }

        public IQueryable<Reserva> SearchReservationByUser(Guid user)
        {
            return Context.Reservas.Where(x=>x.UsuarioId==user && x.Estado==true).AsNoTracking();
        }
        public IQueryable<Reserva> TotalRoomsReservedByHotel(Guid hotel)
        {
            return Context.Reservas.Where(x=>x.HotelId==hotel).AsNoTracking();
        }
        public IQueryable<Hotele> TotalRoomsByHotel(Guid hotel)
        {
            return Context.Hoteles.Where(x => x.HotelId== hotel).AsNoTracking();
        }
        public IQueryable<Reserva> SearchReservationById(Guid id)
        {
            return Context.Reservas.Where(x => x.ReservaId == id).AsNoTracking();
        }
        public void InactivateReservation(Reserva reserve)
        {
            Context.Reservas.Update(reserve);
            Context.SaveChanges();
        }

        public IQueryable<Reserva> GetReservations(DateTime? initialDate, DateTime? finalDate, Guid? hotel, Guid? customer)
        {
            return Context.Reservas
                .Where(x => (hotel == null || x.HotelId == hotel) &&
                            (customer == null || x.UsuarioId == customer) &&
                            (initialDate == null || x.FechaInicial >= initialDate) &&
                            (finalDate == null || x.FechaFinal <= finalDate)).OrderByDescending(x=>x.Usuario.Nombre)
                .AsNoTracking();
        }

        public IQueryable<Usuario> SearchUser(string email, string password)
        {
            return Context.Usuarios
                .Where(x => x.Correo == email && x.Contraseña == password)
                .AsNoTracking();
        }
        public IQueryable<Usuario> GetAllUsers()
        {
            return Context.Usuarios
                .AsNoTracking();
        }

        public IQueryable<Hotele> GetAllHoteles()
        {
            return Context.Hoteles
                .AsNoTracking();
        }



        #region mappers
        private Reserva MapperFromSaveReservationsDTOToReservas(SaveReservationsDTO saveReservationsDTO)
        {
            Reserva reserva = new Reserva();
            reserva.ReservaId = (Guid)saveReservationsDTO.ReservaID;
            reserva.UsuarioId = saveReservationsDTO.UsuarioID;
            reserva.HotelId= saveReservationsDTO.HotelID;
            reserva.FechaReserva = DateTime.Now;
            reserva.NumeroHabitaciones = saveReservationsDTO.NumeroHabitaciones;
            reserva.NumeroPersonas= saveReservationsDTO.NumeroPersonas;
            reserva.Estado = saveReservationsDTO.Estado;
            reserva.Observaciones = saveReservationsDTO.Observaciones;
            reserva.FechaInicial = saveReservationsDTO.FechaInicial;
            reserva.FechaFinal= saveReservationsDTO.FechaFinal;
            reserva.Estado = true;
            return reserva;
        }
        #endregion mappers


    }
}
