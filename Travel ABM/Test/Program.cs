using API;
using Application.LoginService;
using Application.User;
using Application.Utils;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services.LoginService;
string sqlconnection = "Server = PCMAURO; Database=Travel;Trusted_Connection=True;TrustServerCertificate=true;";

var serviceProvider = new ServiceCollection()
                .AddDbContext<TravelContext>(options =>
                    options.UseSqlServer(sqlconnection))
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<ILoginService, LoginService>()
                .AddTransient<ILoginRepository, LoginRepository>()
                .BuildServiceProvider();

var userService = serviceProvider.GetRequiredService<IUserService>();
var loginService = serviceProvider.GetRequiredService<ILoginService>();


Console.WriteLine("Bienvenido!");
int opcionLogueo = PedirOpcionLogueo();

switch (opcionLogueo)
{
    case 1:
        {
            userService.CrearUsuario(new Domain.Usuario
            {
                Name = "Tomas",
                LastName = "Vasco",
                Email = "iantom@correo.com",
                Password = "1234asd",
                BirthDate = DateTime.Now,
                UserName = "tomas_123"
            });
        }

        break;
    case 2:
        if (loginService.Login(new Application.DTO_s.UserDto
        {
            UserName = "tomas_123",
            Password = "1234asd",
            Email = "iantom@correo.com"
        }).Result.Item1)

            MostrarMenu();

        else
        {
            Console.WriteLine("Error en el login. Saliendo del programa.");
        }
        break;

}


static int PedirOpcionLogueo()
{
    int opcion;
    do
    {
        Console.WriteLine("Opciones de Logueo:");
        Console.WriteLine("1. Crear Usuario");
        Console.WriteLine("2. Login");

        Console.Write("Ingrese el número de la opción: ");
        if (!int.TryParse(Console.ReadLine(), out opcion))
        {
            Console.WriteLine("Por favor, ingrese un número válido.");
            opcion = 0;
        }
        else if (opcion != 1 && opcion != 2)
        {
            Console.WriteLine("Por favor, ingrese 1 o 2.");
        }
    } while (opcion != 1 && opcion != 2);

    return opcion;
}

static void MostrarMenu()
{
    Console.WriteLine("Menú:");
    Console.WriteLine("1. Agregar Tour");
    Console.WriteLine("2. Obtener Tours");
    Console.WriteLine("3. Obtener Reservas");
    Console.WriteLine("4. Reservar Tour");
    Console.WriteLine("5. Eliminar Reserva");

    Console.Write("Ingrese el número de la opción: ");
    int opcionMenu = int.Parse(Console.ReadLine());

    switch (opcionMenu)
    {
        case 1:
            Console.WriteLine("Implementación de Agregar Tour...");
            break;
        case 2:
            Console.WriteLine("Implementación de Obtener Tours...");
            break;
        case 3:
            Console.WriteLine("Implementación de Obtener Reservas...");
            break;
        case 4:
            Console.WriteLine("Implementación de Reservar Tour...");
            break;
        case 5:
            Console.WriteLine("Implementación de Eliminar Reserva...");
            break;
        default:
            Console.WriteLine("Opción inválida.");
            break;
    }
}

