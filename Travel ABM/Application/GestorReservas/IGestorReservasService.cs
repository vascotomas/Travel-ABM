using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GestorReservas
{
    public interface IGestorReservasService
    {
        Task<List<Tour>> ObtenerTours();
        Task AgregarTour(Tour tour);
        Task<List<Reserva>> ObtenerReservas();
        Task ReservarTour(Reserva reserva);
        Task EliminarReserva(int reservaId);
    }
}
