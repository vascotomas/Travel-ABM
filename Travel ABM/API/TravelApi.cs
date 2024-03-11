using Application.GestorReservas;
using Domain;

namespace API
{
    public static class TravelApi
    {
        public static void ConfigureApi(this WebApplication application)
        {
            application.MapPost(pattern: "Tour", AgregarTour);

        }

        private static async Task<IResult> AgregarTour(Tour tour, IGestorReservasService service)
        {
            try
            {
                await service.AgregarTour(tour);
                return  Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
