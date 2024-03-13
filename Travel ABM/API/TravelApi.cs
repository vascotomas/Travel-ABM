using Application.Cliente;
using Application.GestorReservas;
using Domain;

namespace API
{
    public static class TravelApi
    {
        public static async void ConfigureApi(this WebApplication application)
        {
            application.MapPost(pattern: "/Tour", AgregarTour);
            application.MapGet(pattern: "/Tour", ObtenerTours);
            application.MapPost(pattern: "/Reserva", ReservarTour);
            application.MapGet(pattern: "/Reserva", ObtenerReservas);
            application.MapDelete(pattern: "/Reserva", EliminarReserva);
        }

        private static async Task<IResult> AgregarTour(Tour tour, IGestorReservasService service)
        {
            try
            {
                await service.AgregarTour(tour);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> ObtenerTours(IGestorReservasService service)
        {
            try
            {
                return Results.Ok(await service.ObtenerTours());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> ObtenerReservas(IGestorReservasService service)
        {
            try
            {
                return Results.Ok(await service.ObtenerReservas());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> ReservarTour(Reserva reserva, IGestorReservasService reservasService, IClienteService clienteService)
        {
            try
            {
                var cliente = await clienteService.ObtenerCliente(reserva.IdCliente);
                if (cliente == null) return Results.BadRequest();

                await reservasService.ReservarTour(reserva);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> EliminarReserva(IGestorReservasService service,int reservaId)
        {
            try
            {
                await service.EliminarReserva(reservaId);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
