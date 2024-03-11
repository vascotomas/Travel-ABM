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
        public async Task<List<Reserva>> ObtenerTodasLasReservas()
        {
            return await _reservaRepository.ObtenerTodasLasReservas();
        }

        public async Task<bool> EliminarReserva(int reservaId)
        {
            throw new NotImplementedException();

            //_reservaRepository.EliminarReserva(reservaId);
        }

        public async Task<List<Tour>> ObtenerTodosLosTours()
        {
            throw new NotImplementedException();
        }

        public Task AgregarTour(Tour tour)
        {
            return _reservaRepository.AgregarTour(tour);
        }

        public async Task ReservarTour(int tourId, int idCliente, DateTime fechaReserva) => await _reservaRepository.
                                                                                                   AgregarReserva(new Reserva
                                                                                                   {
                                                                                                       TourId = tourId,
                                                                                                       IdCliente = idCliente,
                                                                                                       FechaReserva = fechaReserva
                                                                                                   });
    }

}

