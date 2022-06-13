using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PostgreSQLEF.Models.DB
{
    public partial class ApplicationUsuariosContext : DbContext
    {
        public ApplicationUsuariosContext()
        {
        }

        public ApplicationUsuariosContext(DbContextOptions<ApplicationUsuariosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Prueba> Pruebas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;port=5432;Username=postgres;Password=515919;Database=ApplicationUsuarios");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prueba>(entity =>
            {
                entity.ToTable("prueba", "usuario");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario", "usuario");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Apellido).HasColumnName("apellido");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fechaCreacion")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");

                entity.Property(e => e.Nombre).HasColumnName("nombre");

                entity.Property(e => e.Telefono).HasColumnName("telefono");

                entity.Property(e => e.Usuario1).HasColumnName("Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
