using System;
using System.Collections.Generic;

#nullable disable

namespace ReservationLinKtic.Infrastructure
{
    public partial class Reserva
    {
        public Guid ReservaId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid HotelId { get; set; }
        public DateTime FechaReserva { get; set; }
        public int NumeroHabitaciones { get; set; }
        public int NumeroPersonas { get; set; }
        public string Observaciones { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }

        public virtual Hotele Hotel { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
