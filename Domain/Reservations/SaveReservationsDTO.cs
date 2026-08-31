using System;

namespace ReservationArquitectureLayerUDEC.Domain.Reservations
{
    public class SaveReservationsDTO
    {
        public Guid? ReservaID { get; set; }
        public Guid UsuarioID { get; set; }
        public Guid HotelID { get; set; }
        public int NumeroHabitaciones { get; set; }
        public int NumeroPersonas { get; set; }
        public string? Observaciones { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
