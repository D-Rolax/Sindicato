using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class SindicatoContext : DbContext
    {
        public SindicatoContext(DbContextOptions<SindicatoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Afiliados> Afiliados { get; set; }
        public virtual DbSet<AsignacionHorarioChofer> AsignacionHorarioChofer { get; set; }
        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Castigos> Castigos { get; set; }
        public virtual DbSet<Comunidades> Comunidades { get; set; }
        public virtual DbSet<DispositivosGps> DispositivosGps { get; set; }
        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<Horarios> Horarios { get; set; }
        public virtual DbSet<HorariosPorRutas> HorariosPorRutas { get; set; }
        public virtual DbSet<Rutas> Rutas { get; set; }
        public virtual DbSet<TiposVehiculos> TiposVehiculos { get; set; }
        public virtual DbSet<Trakings> Trakings { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Afiliados>(entity =>
            {
                entity.HasIndex(e => e.TipoVehiculoId);

                entity.HasOne(d => d.TipoVehiculo)
                    .WithMany(p => p.Afiliados)
                    .HasForeignKey(d => d.TipoVehiculoId);
            });

            modelBuilder.Entity<AsignacionHorarioChofer>(entity =>
            {
                entity.HasIndex(e => e.AfiliadoId);

                entity.HasIndex(e => e.HorarioRutaId);

                entity.HasOne(d => d.Afiliado)
                    .WithMany(p => p.AsignacionHorarioChofer)
                    .HasForeignKey(d => d.AfiliadoId);

                entity.HasOne(d => d.HorarioRuta)
                    .WithMany(p => p.AsignacionHorarioChofer)
                    .HasForeignKey(d => d.HorarioRutaId);
            });

            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.HasIndex(e => e.ChoferId);

                entity.HasOne(d => d.Chofer)
                    .WithMany(p => p.Calificacion)
                    .HasForeignKey(d => d.ChoferId);
            });

            modelBuilder.Entity<Castigos>(entity =>
            {
                entity.HasIndex(e => e.ChoferId);

                entity.HasOne(d => d.Chofer)
                    .WithMany(p => p.Castigos)
                    .HasForeignKey(d => d.ChoferId);
            });

            modelBuilder.Entity<Comunidades>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DispositivosGps>(entity =>
            {
                entity.ToTable("DispositivosGPS");

                entity.Property(e => e.DireccionIp).HasColumnName("DireccionIP");
            });

            modelBuilder.Entity<HorariosPorRutas>(entity =>
            {
                entity.HasIndex(e => e.HorarioId);

                entity.HasIndex(e => e.RutaId);

                entity.HasOne(d => d.Horario)
                    .WithMany(p => p.HorariosPorRutas)
                    .HasForeignKey(d => d.HorarioId);

                entity.HasOne(d => d.Ruta)
                    .WithMany(p => p.HorariosPorRutas)
                    .HasForeignKey(d => d.RutaId);
            });

            modelBuilder.Entity<Rutas>(entity =>
            {
                entity.HasIndex(e => e.ComunidadId);

                entity.HasIndex(e => e.GrupoId);

                entity.HasOne(d => d.Comunidad)
                    .WithMany(p => p.Rutas)
                    .HasForeignKey(d => d.ComunidadId);

                entity.HasOne(d => d.Grupo)
                    .WithMany(p => p.Rutas)
                    .HasForeignKey(d => d.GrupoId);
            });

            modelBuilder.Entity<Trakings>(entity =>
            {
                entity.HasIndex(e => e.DispositivoGpsid);

                entity.HasIndex(e => e.HorarioChoferId);

                entity.Property(e => e.DispositivoGpsid).HasColumnName("DispositivoGPSId");

                entity.HasOne(d => d.DispositivoGps)
                    .WithMany(p => p.Trakings)
                    .HasForeignKey(d => d.DispositivoGpsid);

                entity.HasOne(d => d.HorarioChofer)
                    .WithMany(p => p.Trakings)
                    .HasForeignKey(d => d.HorarioChoferId);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
