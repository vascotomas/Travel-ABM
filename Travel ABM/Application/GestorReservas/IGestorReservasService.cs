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
        Task<List<Tour>> ObtenerTodosLosTours();
        Task AgregarTour(Tour tour);
        Task<List<Tour>> ObtenerTodasLasReservas();
        Task ReservarTour(int tourId, int idCliente, DateTime fechaReserva);
        Task EliminarReserva(int reservaId);
    }
}
