using System;
using System.Collections.Generic;

#nullable disable

namespace ReservationLinKtic.Infrastructure
{
    public partial class Usuario
    {
        public Usuario()
        {
            Reservas = new HashSet<Reserva>();
        }

        public Guid UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Contraseña { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
