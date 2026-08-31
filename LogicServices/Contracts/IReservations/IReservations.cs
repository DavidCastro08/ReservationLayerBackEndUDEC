using ReservationArquitectureLayerUDEC.Domain.Reservations;
using ReservationArquitectureLayerUDEC.DTO.Generics;
using ReservationArquitectureLayerUDEC.DTO.Reservation;
using System;
using System.Collections.Generic;

namespace ReservationArquitectureLayerUDEC.LogicServices.Contracts.IReservations
{
    public interface IReservations
    {
        ResponseDTO SaveReservation(SaveReservationsDTO saveReservationsDTO);
        ResponseDTO UpdateReservation(SaveReservationsDTO saveReservationsDTO);
        ResponseDTO InactivateReservation(Guid reserve);
        List<ReturnReservationDTO> GetReservation(DateTime? initialDate,DateTime? finalDate, Guid? hotel, Guid? customer);
        ResponseDTO SearchUser(string email, string password);
        List<ResultSelectors> GetAllUsers();
        List<ResultSelectors> GetAllHoteles();

    }
}
