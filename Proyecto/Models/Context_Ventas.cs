using System;
using CallSouth.VentasCRM.Clases;
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
        public DbSet<Usuarios> usuarios { get; set; }
        public DbSet<Cargador_Validador> cargador_Validadors { get; set; }


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
        public DbSet<Panel_Hopper> panel_Hoppers { get; set; }
        public DbSet<Carga_Resultado_Ripley> carga_Resultado_Ripleys  { get; set; }
        public DbSet<Calidad_Admin_List> calidad_Admin_Lists { get; set; }
        public DbSet<Calidad_Admin_List_Total> calidad_Admin_List_Totals { get; set; }
        public DbSet<Calidad_Admin_List_Apeladas> calidad_Admin_List_Apeladas { get; set; }
        public DbSet<Calidad_Admin_List_Asignadas> calidad_Admin_List_Asignadas { get; set; }
        public DbSet<Calidad_Admin_List_Apelados> calidad_Admin_List_Apelados { get; set; }


        public DbSet<CargaBasesFlujo> cargaBasesFlujos { get; set; }
        public DbSet<Resultante_ABCDIN_2> resultante_ABCDIN_2s { get; set; }
        public DbSet<CallIntro_Gestion_Dia> callIntro_Gestion_Dias { get; set; }
        public DbSet<CallIntro_Gestion_Venta> callIntro_Gestion_Ventas { get; set; }
        public DbSet<tbl_Horas_Logeo_Full> tbl_Horas_Logeo_Fulls { get; set; }
        public DbSet<CallIntro_Gestion_NoVenta> callIntro_Gestion_NoVentas { get; set; }
        public DbSet<tbl_NoVentas> tbl_NoVentas { get; set; }
        public DbSet<SeguimientoBBDD> seguimientoBBDDs { get; set; }
        public DbSet<UltimoIntento> ultimoIntentos { get; set; }
        public DbSet<ValidacionVenta> validacionVentas { get; set; }
        public DbSet<EstadoVentas> estadoVentas { get; set; }
        public DbSet<PanelTematico> panelTematicos { get; set; }


    }
}
