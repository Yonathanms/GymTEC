using GymTEC.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Habilitar CORS para permitir solicitudes desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactAPP",
        policy =>
        {
            policy.WithOrigins("http://localhost:53503") // URL de la aplicación frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Permitir credenciales si es necesario
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Swagger
builder.Services.AddSwaggerGen();           // Swagger

builder.Services.AddDbContext<GymTECDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar CORS antes de los MapControllers
app.UseCors("AllowReactAPP");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();