using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GestorReservas
{
    public class GestorReservasService : IGestorReservasService
    {

        private readonly IGestorReservaRepository _reservaRepository;

        public GestorReservasService(IGestorReservaRepository reserva)
        {
            _reservaRepository = reserva;
        }
        public async Task<List<Reserva>> ObtenerReservas() => await _reservaRepository.ObtenerReservas();
        public async Task EliminarReserva(int reservaId) => await _reservaRepository.EliminarReserva(reservaId);
        public async Task<List<Tour>> ObtenerTours() => await _reservaRepository.ObtenerTours();
        public async Task AgregarTour(Tour tour) => await _reservaRepository.AgregarTour(tour);
        public async Task ReservarTour(Reserva reserva) => await _reservaRepository.AgregarReserva(reserva);
    }

}

