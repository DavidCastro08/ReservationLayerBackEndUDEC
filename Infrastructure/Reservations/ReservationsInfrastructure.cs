using Microsoft.EntityFrameworkCore;
using ReservationArquitectureLayerUDEC.Domain.Reservations;
using ReservationArquitectureLayerUDEC.Infrastructure.Contracts.IReservations;
using System;
using System.Linq;

namespace ReservationArquitectureLayerUDEC.Infrastructure.Reservations
{
    public class ReservationsInfrastructure: IReservationsInfrastructure
    {
        #region Fileds
        private readonly ReservationArquitectureLayerUDECContext Context;
        #endregion Fields

        #region Constants

        #endregion Constants

        #region Builder
        public ReservationsInfrastructure(ReservationArquitectureLayerUDECContext _Context) : base()
        {
            Context = _Context;
        }
        // Constructor que recibe el contexto de EF Core para acceder a la base de datos
        // Guarda el contexto en la instancia para ser usado por los métodos de acceso a datos
        #endregion Builder

        public void SaveReservation(SaveReservationsDTO saveReservationsDTO)
        {
            Reserva registerToSave = MapperFromSaveReservationsDTOToReservas(saveReservationsDTO);
            Context.Reservas.Add(registerToSave);
            Context.SaveChanges();
        }

        // Inserta una nueva entidad Reserva en la base de datos a partir del DTO proporcionado
        // Usa el mapper privado para convertir del DTO al modelo de entidad y persiste los cambios

        public void UpdateReservation(SaveReservationsDTO updateReservationsDTO)
        {
            Reserva registerToSave = MapperFromSaveReservationsDTOToReservas(updateReservationsDTO);
            Context.Reservas.Update(registerToSave);
            Context.SaveChanges();
        }

        // Actualiza una entidad Reserva existente con los valores del DTO proporcionado
        // Realiza un Update en el DbContext y persiste los cambios

        public IQueryable<Reserva> SearchReservationByUser(Guid user)
        {
            return Context.Reservas.Where(x=>x.UsuarioId==user && x.Estado==true).AsNoTracking();
        }

        // Devuelve una consulta (IQueryable) con las reservas activas asociadas al usuario indicado
        // No realiza seguimiento (AsNoTracking) para consultas de solo lectura
        public IQueryable<Reserva> TotalRoomsReservedByHotel(Guid hotel)
        {
            return Context.Reservas.Where(x=>x.HotelId==hotel).AsNoTracking();
        }

        // Devuelve una consulta con todas las reservas relacionadas a un hotel específico
        // Se utiliza para calcular el total de habitaciones reservadas por hotel
        public IQueryable<Hotele> TotalRoomsByHotel(Guid hotel)
        {
            return Context.Hoteles.Where(x => x.HotelId== hotel).AsNoTracking();
        }

        // Devuelve la información del hotel (incluye número de habitaciones) para el hotel indicado
        public IQueryable<Reserva> SearchReservationById(Guid id)
        {
            return Context.Reservas.Where(x => x.ReservaId == id).AsNoTracking();
        }

        // Busca una reserva por su identificador
        public void InactivateReservation(Reserva reserve)
        {
            Context.Reservas.Update(reserve);
            Context.SaveChanges();
        }

        // Marca una reserva como inactiva (soft-delete) actualizando la entidad y guardando cambios

        public IQueryable<Reserva> GetReservations(DateTime? initialDate, DateTime? finalDate, Guid? hotel, Guid? customer)
        {
            return Context.Reservas
                .Where(x => (hotel == null || x.HotelId == hotel) &&
                            (customer == null || x.UsuarioId == customer) &&
                            (initialDate == null || x.FechaInicial >= initialDate) &&
                            (finalDate == null || x.FechaFinal <= finalDate)).OrderByDescending(x=>x.Usuario.Nombre)
                .AsNoTracking();
        }

        // Construye y devuelve una consulta de reservas filtrada por fechas, hotel y cliente opcionales
        // Ordena los resultados por el nombre del usuario y usa AsNoTracking para lectura

        public IQueryable<Usuario> SearchUser(string email, string password)
        {
            return Context.Usuarios
                .Where(x => x.Correo == email && x.Contraseña == password)
                .AsNoTracking();
        }

        // Busca usuarios que coincidan con el email y la contraseña proporcionados
        // Devuelve IQueryable para que la capa superior pueda proyectar los datos necesarios
        public IQueryable<Usuario> GetAllUsers()
        {
            return Context.Usuarios
                .AsNoTracking();
        }

        // Devuelve todos los usuarios (solo lectura)

        public IQueryable<Hotele> GetAllHoteles()
        {
            return Context.Hoteles
                .AsNoTracking();
        }

        // Devuelve todos los hoteles (solo lectura)



        #region mappers
        private Reserva MapperFromSaveReservationsDTOToReservas(SaveReservationsDTO saveReservationsDTO)
        {
            Reserva reserva = new Reserva();
            reserva.ReservaId = (Guid)saveReservationsDTO.ReservaID;
            reserva.UsuarioId = saveReservationsDTO.UsuarioID;
            reserva.HotelId= saveReservationsDTO.HotelID;
            reserva.FechaReserva = DateTime.Now;
            reserva.NumeroHabitaciones = saveReservationsDTO.NumeroHabitaciones;
            reserva.NumeroPersonas= saveReservationsDTO.NumeroPersonas;
            reserva.Estado = saveReservationsDTO.Estado;
            reserva.Observaciones = saveReservationsDTO.Observaciones;
            reserva.FechaInicial = saveReservationsDTO.FechaInicial;
            reserva.FechaFinal= saveReservationsDTO.FechaFinal;
            reserva.Estado = true;
            return reserva;
        }
        // Mapea desde el DTO de entrada SaveReservationsDTO hacia la entidad Reserva usada por EF
        // Inicializa campos necesarios (fecha de reserva, ids, estado, etc.)
        #endregion mappers


    }
}
