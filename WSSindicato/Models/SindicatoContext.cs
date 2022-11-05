using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WSSindicato.Models
{
    public partial class SindicatoContext : DbContext
    {
        public SindicatoContext()
        {
        }

        public SindicatoContext(DbContextOptions<SindicatoContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Afiliados> Afiliados { get; set; }
        public virtual DbSet<AsignacionHorarioChofer> AsignacionHorarioChofers { get; set; }
        public virtual DbSet<Castigos> Castigos { get; set; }
        public virtual DbSet<Comunidades> Comunidades{ get; set; }
        public virtual DbSet<DispositivosGps> DispositivosGps { get; set; }
        public virtual DbSet<Grupos>Grupos { get; set; }
        public virtual DbSet<Horarios> Horarios{ get; set; }
        public virtual DbSet<HorariosPorRutas> HorariosPorRutas { get; set; }
        public virtual DbSet<Rutas> Rutas { get; set; }
        public virtual DbSet<TiposVehiculos> TiposVehiculos { get; set; }
        public virtual DbSet<Trakings> Trakings { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Sindicato;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.TipoUsuario)
                    .HasColumnName("tipoUsuario")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
