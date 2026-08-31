using ReservationArquitectureLayerUDEC.Domain.Reservations;
using ReservationArquitectureLayerUDEC.DTO.Generics;
using ReservationArquitectureLayerUDEC.DTO.Reservation;
using ReservationArquitectureLayerUDEC.Infrastructure;
using ReservationArquitectureLayerUDEC.Infrastructure.Contracts.IReservations;
using ReservationArquitectureLayerUDEC.LogicServices.Contracts.IReservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ReservationArquitectureLayerUDEC.LogicServices.Reservations
{
    public class Reservations : IReservations
    {
        #region Fileds
        private readonly IReservationsInfrastructure _reservations;
        #endregion Fields

        #region constants
        private const string FORMAT = "yyyy/MM/dd  HH:mm:ss";
        #endregion constants

        #region Builder
        public Reservations(IReservationsInfrastructure reservations)
        {
            _reservations = reservations;

        }
        #endregion Builder


        #region Methods
        public ResponseDTO SaveReservation(SaveReservationsDTO saveReservationsDTO)
        {
            try
            {
                ResponseDTO responseDTO = new ResponseDTO();
                int totalRoomsReservedByHotel = 0;
                int totalRoomsAvailableByHotel = 0;
                //Validar si el usuario ya tiene reserva
                Guid? registerExisted = _reservations.SearchReservationByUser(saveReservationsDTO.UsuarioID).Select(x=>x.ReservaId).FirstOrDefault();
                if (registerExisted!=null && registerExisted !=Guid.Empty)
                {
                    responseDTO.isValid = false;
                    responseDTO.error = "El usuario ingresado ya cuenta con una reserva activa";
                    return responseDTO;
                }
                // validar si hay habitaciones disponibles
                List<Guid> roomsReserved = _reservations.TotalRoomsReservedByHotel(saveReservationsDTO.HotelID).Select(x=>x.ReservaId).ToList();
                int roomsTotal = _reservations.TotalRoomsByHotel(saveReservationsDTO.HotelID).Select(x=>x.NumeroHabitaciones).FirstOrDefault();
                if (roomsReserved!=null)
                {
                    totalRoomsReservedByHotel = roomsReserved.Count();
                }
                totalRoomsAvailableByHotel = roomsTotal - totalRoomsReservedByHotel;
                if (totalRoomsAvailableByHotel >= saveReservationsDTO.NumeroHabitaciones)
                {
                    saveReservationsDTO.ReservaID = Guid.NewGuid();
                    _reservations.SaveReservation(saveReservationsDTO);
                    responseDTO.isValid = true;
                    responseDTO.message = "Registro procesado correctamente";
                    return responseDTO;
                }
                else
                {
                    responseDTO.isValid = false;
                    responseDTO.error = $"El numero de habitaciones disponibles es de {totalRoomsAvailableByHotel}";
                    return responseDTO;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Cominiquese con el administrado", ex);
            }
        }

        public ResponseDTO UpdateReservation(SaveReservationsDTO updateReservationsDTO)
        {
            try
            {
                ResponseDTO responseDTO = new ResponseDTO();
                int totalRoomsReservedByHotel = 0;
                int totalRoomsAvailableByHotel = 0;
                //Validar si el usuario ya tiene reserva
                Reserva registerExisted = _reservations.SearchReservationByUser(updateReservationsDTO.UsuarioID).FirstOrDefault();
                if (registerExisted == null)
                {
                    responseDTO.isValid = false;
                    responseDTO.error = "No se encuentra una reserva activa con el usuario ingresado";
                    return responseDTO;
                }
                // validar si hay habitaciones disponibles
                List<Guid> roomsReserved = _reservations.TotalRoomsReservedByHotel(registerExisted.HotelId).Select(x => x.ReservaId).ToList();
                int roomsTotal = _reservations.TotalRoomsByHotel(registerExisted.HotelId).Select(x => x.NumeroHabitaciones).FirstOrDefault();
                if (roomsReserved != null)
                {
                    totalRoomsReservedByHotel = roomsReserved.Count();
                }
                totalRoomsAvailableByHotel = roomsTotal - totalRoomsReservedByHotel;
                if (totalRoomsAvailableByHotel >= updateReservationsDTO.NumeroHabitaciones)
                {
                    updateReservationsDTO.ReservaID = registerExisted.ReservaId;
                    updateReservationsDTO.HotelID = registerExisted.HotelId;
                    updateReservationsDTO.Estado = true;
                    _reservations.UpdateReservation(updateReservationsDTO);
                    responseDTO.isValid = true;
                    responseDTO.message = "Registro procesado correctamente";
                    return responseDTO;
                }
                else
                {
                    responseDTO.isValid = false;
                    responseDTO.error = $"El numero de habitaciones disponibles es de {totalRoomsAvailableByHotel}";
                    return responseDTO;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Cominiquese con el administrado", ex);
            }
        }

        public ResponseDTO InactivateReservation(Guid user)
        {
            try
            {
                ResponseDTO responseDTO = new ResponseDTO();
                Reserva registerExisted = _reservations.SearchReservationByUser(user).FirstOrDefault();
                if(registerExisted==null)
                {
                    responseDTO.isValid = false;
                    responseDTO.error = $"No se encuentra reserva";
                    return responseDTO;
                }
                else
                {
                    registerExisted.Estado = false;
                    _reservations.InactivateReservation(registerExisted);
                    responseDTO.isValid = true;
                    responseDTO.message = "Registro procesado correctamente";
                    return responseDTO;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Cominiquese con el administrado", ex);
            }
        }

        public List<ReturnReservationDTO> GetReservation(DateTime? initialDate, DateTime? finalDate, Guid? hotel, Guid? customer)
        {
            try
            {
                return _reservations.GetReservations(initialDate, finalDate, hotel, customer).Select(MapperFromReservaToReturnReservationDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Cominiquese con el administrado", ex);
            }
        }

        public ResponseDTO SearchUser(string email, string password)
        {
            try
            {
                ResponseDTO responseDTO = new ResponseDTO();
                string idUser = _reservations.SearchUser(email,password).Select(x=>x.Nombre).FirstOrDefault();
                if (idUser != null)
                {
                    responseDTO.isValid = true;
                    responseDTO.message = idUser.ToString();
                    return responseDTO;
                }
                else
                {
                    responseDTO.isValid = false;
                    responseDTO.error = "Los datos ingresados no coinciden";
                    return responseDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Cominiquese con el administrado", ex);
            }
        }

        public List<ResultSelectors> GetAllUsers()
        {
            try
            {
                return _reservations.GetAllUsers().Select(MapperFromUsuarioToResultSelectors).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Cominiquese con el administrado", ex);
            }
        }

        public List<ResultSelectors> GetAllHoteles()
        {
            try
            {
                return _reservations.GetAllHoteles().Select(MapperFromHotelesToResultSelectors).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Cominiquese con el administrado", ex);
            }
        }



        #endregion Methods

        #region intermedialMapper
        private static Expression<Func<Reserva, ReturnReservationDTO>> MapperFromReservaToReturnReservationDTO = register => new ReturnReservationDTO()
        {
            reservaID = register.ReservaId.ToString(),
            usuarioID = register.Usuario.Nombre,
            hotelID = register.Hotel.Nombre,
            fechaInicial = register.FechaInicial.ToString(FORMAT),
            fechaFinal = register.FechaFinal.ToString(FORMAT),
            numeroHabitaciones = register.NumeroHabitaciones.ToString(),
            numeroPersonas = register.NumeroPersonas.ToString(),
            observaciones = register.Observaciones,
            estado = (register.Estado == true) ? "Activo": "Inactivo"
        };

        private static Expression<Func<Usuario, ResultSelectors>> MapperFromUsuarioToResultSelectors = register => new ResultSelectors()
        {
            valueExpr = register.UsuarioId,
            displayExpr = register.Nombre
        };

        private static Expression<Func<Hotele, ResultSelectors>> MapperFromHotelesToResultSelectors = register => new ResultSelectors()
        {
            valueExpr = register.HotelId,
            displayExpr = register.Nombre
        };
        #endregion intermedialMapper
    }
}
