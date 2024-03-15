using Application.Cliente;
using Application.DTO_s;
using Application.GestorReservas;
using Application.User;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.LoginService;

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
            application.MapPost(pattern: "/CrearUsuario", CrearUsuario);
        }
        #region endpoints GeneraReserva
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
        private static async Task<IResult> EliminarReserva(IGestorReservasService service, int reservaId)
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
        #endregion

        #region login
        [HttpPost("authenticate")]
        public static async Task<IResult> Login(ILoginService service, UserDto userDto) 
        {
            var user = await service.Get(userDto);

            if (user == null)
                return Results.BadRequest(new { message = "Credenciales Incorrectas" });

            string jwtToken = await service.GenerateToken(user);
            return Results.Ok(new { token = jwtToken });
        }

        [HttpPost("forgot-password")]
        public static async Task<IResult> ForgotPassword(ILoginService service,string email)
        {
            bool stateReset = await service.ForgotPassword(email);

            if (!stateReset)            
                return Results.BadRequest("Email incorrecto");
            
            return Results.Ok();
        }

        [HttpPost("reset-password")]
        public static async Task<IResult> ResetPassword(ILoginService service,ResetPasswordRequestDto request)
        {
            if (await service.ResetPassword(request))
                return Results.Ok();

            return Results.BadRequest();
        }

        [HttpPost("login")]
        public static async Task<IResult> Login(ILoginService service,string username, string password)
        {
            var loginResult = await service.Login(new UserDto { UserName = username, Password = password });

            if (loginResult.Item1 == false)
                return Results.BadRequest(loginResult.Item2);

            return Results.Ok(loginResult.Item2);

        }
        #endregion

        #region user
  
        [HttpPost]
        public static async Task<IResult> CrearUsuario(IUserService service,Usuario user)
        {
            var status = await service.CrearUsuario(user);
            if(!status)
                return Results.BadRequest(status.ToString());

            return  Results.Ok();
        }
        #endregion
    }
}
