using MedicAppAPI.Data;
using MedicAppAPI.Interfaces;
using MedicAppAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DataVerifier>();
builder.Services.AddScoped<IDoctor, DoctorService>();
builder.Services.AddScoped<IEspecialidad, EspecialidadService>();
builder.Services.AddScoped<IPaciente, PacienteService>();
builder.Services.AddScoped<IHorario, HorarioService>();

builder.Services.AddDbContext<MedicAppDb>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
