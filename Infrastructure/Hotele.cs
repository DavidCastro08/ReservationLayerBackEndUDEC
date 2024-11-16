using System;
using System.Collections.Generic;

#nullable disable

namespace ReservationLinKtic.Infrastructure
{
    public partial class Hotele
    {
        public Hotele()
        {
            Reservas = new HashSet<Reserva>();
        }

        public Guid HotelId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public int NumeroHabitaciones { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
