using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReservationArquitectureLayerUDEC.Domain.Reservations;
using ReservationArquitectureLayerUDEC.DTO.Generics;
using ReservationArquitectureLayerUDEC.DTO.Reservation;
using ReservationArquitectureLayerUDEC.LogicServices.Contracts.IReservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationArquitectureLayerUDEC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservations _reservations;

        public ReservationController(IReservations reservations)
        {
            _reservations = reservations;
        }

        // Constructor que inyecta la capa de lógica de reservas mediante DI
        // Permite que los endpoints del controlador deleguen la operación al servicio de lógica

        [HttpPost]
        [Route(nameof(ReservationController.SaveReservation))]
        // Endpoint POST: recibe los datos de una reserva y delega la creación al servicio de lógica
        // Retorna un ResponseDTO que indica éxito o error del proceso
        public ResponseDTO SaveReservation(SaveReservationsDTO saveReservationsDTO)
        {
            ResponseDTO response = _reservations.SaveReservation(saveReservationsDTO);
            return response;
        }

        [HttpPut]
        [Route(nameof(ReservationController.UpdateReservation))]
        // Endpoint PUT: recibe los datos para actualizar una reserva existente y delega la actualización
        // Retorna un ResponseDTO con el resultado de la operación
        public ResponseDTO UpdateReservation(SaveReservationsDTO saveReservationsDTO)
        {
            ResponseDTO response = _reservations.UpdateReservation(saveReservationsDTO);
            return response;
        }

        [HttpDelete]
        [Route(nameof(ReservationController.InactivateReservation))]
        // Endpoint DELETE: inactiva (soft-delete) la reserva asociada al usuario indicado
        // Recibe el identificador del usuario y retorna el resultado en un ResponseDTO
        public ResponseDTO InactivateReservation(Guid user)
        {
            ResponseDTO response = _reservations.InactivateReservation(user);
            return response;
        }

        [HttpGet]
        [Route(nameof(ReservationController.GetReservation))]
        // Endpoint GET: obtiene una lista de reservas filtradas opcionalmente por fecha inicial, fecha final, hotel y cliente
        // Devuelve una lista de DTOs con la información formateada de las reservas
        public List<ReturnReservationDTO> GetReservation(DateTime? initialDate, DateTime? finalDate, Guid? hotel, Guid? customer)
        {
            List<ReturnReservationDTO> response = _reservations.GetReservation(initialDate, finalDate, hotel, customer);
            return response;
        }

        [HttpGet]
        [Route(nameof(ReservationController.SearchUser))]
        // Endpoint GET: valida las credenciales del usuario (email y contraseña)
        // Retorna un ResponseDTO con el nombre del usuario cuando las credenciales son correctas
        public ResponseDTO SearchUser(string email, string password)
        {
            ResponseDTO response = _reservations.SearchUser(email, password);
            return response;
        }

        [HttpGet]
        [Route(nameof(ReservationController.GetAllUsers))]
        // Endpoint GET: obtiene todos los usuarios en formato selector (valor/display)
        public List<ResultSelectors> GetAllUsers()
        {
            List<ResultSelectors> response = _reservations.GetAllUsers();
            return response;
        }

        [HttpGet]
        [Route(nameof(ReservationController.GetAllHoteles))]
        // Endpoint GET: obtiene todos los hoteles en formato selector (valor/display)
        public List<ResultSelectors> GetAllHoteles()
        {
            List<ResultSelectors> response = _reservations.GetAllHoteles();
            return response;
        }
    }
}
