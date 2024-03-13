using Domain;

namespace Application.GestorReservas
{
    public interface IGestorReservaRepository
    {
        Task<Reserva> ObtenerReservaPorId(int reservaId);
        Task<List<Reserva>> ObtenerReservas();
        Task<List<Tour>> ObtenerTours();

        Task AgregarReserva(Reserva reserva);
        Task AgregarTour(Tour tour);

        Task EliminarReserva(int reservaId);
    }
}