using ReservationLinKtic.Domain.Reservations;
using System;
using System.Linq;

namespace ReservationLinKtic.Infrastructure.Contracts.IReservations
{
    public interface IReservationsInfrastructure
    {
        void SaveReservation(SaveReservationsDTO saveReservationsDTO);
        IQueryable<Reserva> SearchReservationByUser(Guid user);
        IQueryable<Reserva> TotalRoomsReservedByHotel(Guid hotel);
        IQueryable<Hotele> TotalRoomsByHotel(Guid hotel);
        void UpdateReservation(SaveReservationsDTO saveReservationsDTO);
        IQueryable<Reserva> SearchReservationById(Guid id);
        void InactivateReservation(Reserva reserve);
        IQueryable<Reserva> GetReservations(DateTime? initialDate, DateTime? finalDate, Guid? hotel, Guid? customer);
        IQueryable<Usuario> SearchUser(string email, string password);
        IQueryable<Usuario> GetAllUsers();
        IQueryable<Hotele> GetAllHoteles();
    }
}
