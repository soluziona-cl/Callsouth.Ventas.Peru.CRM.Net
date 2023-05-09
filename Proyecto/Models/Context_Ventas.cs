using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CallSouth.Ventas.Peru.Models
{
    public partial class Context_Ventas : DbContext
    {
        public Context_Ventas()
        {
        }

        public Context_Ventas(DbContextOptions<Context_Ventas> options)
            : base(options)
        {
        }

        public DbSet<TblOpcionLlamadum> TblOpcionLlamada { get; set; }



        public DbSet<Login_out> Login_out { get; set; }
        public DbSet<Login_out_result> login_Out_Results { get; set; }
        public DbSet<Login> Logins { get; set; }


        public DbSet<SaveUrl> saveUrls { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<Session_out> Session_out { get; set; }
        public DbSet<Flujo_Respuesta> flujo_Respuestas { get; set; }
        public DbSet<Flujo_ingreso> flujo_Ingresos { get; set; }
        public DbSet<Menu_Header> menu_Headers { get; set; }
        public DbSet<Menu_Body> menu_Body { get; set; }
        public DbSet<Flujo_Out_respuesta> flujo_Out_Respuestas { get; set; }
        public DbSet<Flujo_in> flujo_Ins { get; set; }
        public DbSet<Flujo_in_paso_1> flujo_In_Paso_1s { get; set; }
        public DbSet<Flujo_in_paso_2> flujo_In_Paso_2s { get; set; }
        public DbSet<Flujo_in_paso_3> flujo_In_Paso_3s { get; set; }
        public DbSet<Flujo_in_paso_4> flujo_In_Paso_4s { get; set; }
        public DbSet<Cargador> cargadors { get; set; }
        public DbSet<Gestion_Carga> gestion_Cargas { get; set; }
        public DbSet<Gestion_Ejecutivo> gestion_Ejecutivos { get; set; }
        public DbSet<Gestion_Dash_dia> gestion_Dash_Dias { get; set; }
        public DbSet<Gestion_NC> gestion_NCs { get; set; }
        public DbSet<Gestion_NC_Radar> gestion_NC_Radars { get; set; }
        public DbSet<CRM_Carga_Resumen_Dash> cRM_Carga_Resumen_Dashes { get; set; }
        public DbSet<CRM_Carga_Resumen_DashAdmin> cRM_Carga_Resumen_DashAdmins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=conn");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblOpcionLlamadum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbl_Opcion_Llamada");

                entity.Property(e => e.Agenda)
                    .HasMaxLength(100)
                    .HasColumnName("agenda");

                entity.Property(e => e.Detalle)
                    .HasMaxLength(300)
                    .HasColumnName("detalle");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Padre).HasColumnName("padre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
