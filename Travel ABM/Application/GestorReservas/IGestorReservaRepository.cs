using Domain;

namespace Application.GestorReservas
{
    public interface IGestorReservaRepository
    {
        Task<Reserva> ObtenerReservaPorId(int reservaId);
        Task<List<Reserva>> ObtenerTodasLasReservas();
        Task AgregarReserva(Reserva reserva);
        Task AgregarTour(Tour tour);

        Task EliminarReserva(int reservaId);
    }
}