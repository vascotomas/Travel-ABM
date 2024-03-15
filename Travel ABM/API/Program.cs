using API;
using Application.Cliente;
using Application.GestorReservas;
using Application.LoginService;
using Application.User;
using Application.Utils;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.LoginService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;

//DbContext
builder.Services.AddDbContext<TravelContext>(optionsAction: options => options.UseSqlServer(configuration.GetConnectionString("default")));
//Scoped
builder.Services.AddScoped<IGestorReservasService, GestorReservasService>();
builder.Services.AddScoped<IGestorReservaRepository, GestorReservaRepository>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddSingleton<IConfiguration>(configuration);


/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });*/


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    TravelContext context = scope.ServiceProvider.GetRequiredService<TravelContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureApi();
app.Run();

