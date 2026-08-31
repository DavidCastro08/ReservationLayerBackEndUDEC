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

        [HttpPost]
        [Route(nameof(ReservationController.SaveReservation))]
        public ResponseDTO SaveReservation(SaveReservationsDTO saveReservationsDTO)
        {
            ResponseDTO response = _reservations.SaveReservation(saveReservationsDTO);
            return response;
        }

        [HttpPut]
        [Route(nameof(ReservationController.UpdateReservation))]
        public ResponseDTO UpdateReservation(SaveReservationsDTO saveReservationsDTO)
        {
            ResponseDTO response = _reservations.UpdateReservation(saveReservationsDTO);
            return response;
        }

        [HttpDelete]
        [Route(nameof(ReservationController.InactivateReservation))]
        public ResponseDTO InactivateReservation(Guid user)
        {
            ResponseDTO response = _reservations.InactivateReservation(user);
            return response;
        }

        [HttpGet]
        [Route(nameof(ReservationController.GetReservation))]
        public List<ReturnReservationDTO> GetReservation(DateTime? initialDate, DateTime? finalDate, Guid? hotel, Guid? customer)
        {
            List<ReturnReservationDTO> response = _reservations.GetReservation(initialDate, finalDate, hotel, customer);
            return response;
        }

        [HttpGet]
        [Route(nameof(ReservationController.SearchUser))]
        public ResponseDTO SearchUser(string email, string password)
        {
            ResponseDTO response = _reservations.SearchUser(email, password);
            return response;
        }

        [HttpGet]
        [Route(nameof(ReservationController.GetAllUsers))]
        public List<ResultSelectors> GetAllUsers()
        {
            List<ResultSelectors> response = _reservations.GetAllUsers();
            return response;
        }

        [HttpGet]
        [Route(nameof(ReservationController.GetAllHoteles))]
        public List<ResultSelectors> GetAllHoteles()
        {
            List<ResultSelectors> response = _reservations.GetAllHoteles();
            return response;
        }
    }
}
