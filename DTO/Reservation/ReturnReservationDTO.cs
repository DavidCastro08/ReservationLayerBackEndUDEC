using System;

namespace ReservationArquitectureLayerUDEC.DTO.Reservation
{
    public class ReturnReservationDTO
    {
        public string? reservaID { get; set; }
        public string usuarioID { get; set; }
        public string hotelID { get; set; }
        public string numeroHabitaciones { get; set; }
        public string numeroPersonas { get; set; }
        public string? observaciones { get; set; }
        public string estado { get; set; }
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }
    }
}
