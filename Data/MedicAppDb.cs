using MedicAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicAppAPI.Data
{
    public class MedicAppDb : DbContext
    {

        public MedicAppDb(DbContextOptions<MedicAppDb> options) : base(options)
        {

        }

        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>()
                .Property(p => p.Correo)
                .IsRequired(false);

            modelBuilder.Entity<Especialidad>()
                .HasData(
                new Especialidad { EspecialidadID = 1, Nombre = "Clínica Médica" },
                new Especialidad { EspecialidadID = 2, Nombre = "Cardiología" },
                new Especialidad { EspecialidadID = 3, Nombre = "Gastroenterología" },
                new Especialidad { EspecialidadID = 4, Nombre = "Neurología" },
                new Especialidad { EspecialidadID = 5, Nombre = "Traumatología" },
                new Especialidad { EspecialidadID = 6, Nombre = "Neumonología" },
                new Especialidad { EspecialidadID = 7, Nombre = "Otorrinolaringología" },
                new Especialidad { EspecialidadID = 8, Nombre = "Urología" }
                );

            modelBuilder.Entity<Doctor>()
                .HasData(
                new Doctor { DoctorID = 1, Nombre = "Carlos", Apellido = "Pérez", EspecialidadID = 1}
                );

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Doctor)
                .WithMany(d  => d.Horarios)
                .HasForeignKey(h => h.DoctorID)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
