using Application.GestorReservas;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GestorReservaRepository : IGestorReservaRepository
    {
        private readonly TravelContext _context;

        public GestorReservaRepository(TravelContext context)
        {
            _context = context;
        }

        public async Task<Reserva> ObtenerReservaPorId(int reservaId) => await _context.Reserva.FirstOrDefaultAsync(r => r.Id == reservaId);


        public async Task<List<Reserva>> ObtenerTodasLasReservas() => await _context.Reserva.ToListAsync();


        public async Task AgregarReserva(Reserva reserva)
        {
            await _context.Reserva.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarReserva(int reservaId)
        {
            var reserva = await _context.Reserva.FirstOrDefaultAsync(r => r.Id == reservaId);
            if (reserva != null)
                _context.Reserva.Remove(reserva);
        }

        public async Task AgregarTour(Tour tour) => await _context.Tour.AddAsync(tour);

        /*public async Task<bool> GuardarCambiosAsync()
        {
            int affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }*/
    }


}