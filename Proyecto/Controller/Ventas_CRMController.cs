using CallSouth.Ventas.Peru.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;


namespace CallSouth.Ventas.Peru.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Ventas_CRMController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly Context_Ventas _context;
        private readonly IFileProvider fileProvider;

        private readonly IWebHostEnvironment Environment;
        private readonly IConfiguration Configuration;

        public Ventas_CRMController(IJwtAuthenticationManager jwtAuthenticationManager, Context_Ventas context, IFileProvider fileProvider, IWebHostEnvironment _environment, IConfiguration _configuration)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
            this.fileProvider = fileProvider;
            Environment = _environment;
            Configuration = _configuration;
        }

        #region Usuarios


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Admin/Usuario")]
        public async Task<List<Flujo_Respuesta>> Get_AdminUsuario(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_usuario  @pTYPE ='" + flujo_In.dato +
               "', @Nombre = '" + flujo_In.dato_1 +
               "', @Cargo = '" + flujo_In.dato_2 +
               "', @usuario = '" + flujo_In.dato_3 +
               "', @password = '" + flujo_In.dato_4 +
               "', @mail = '" + flujo_In.dato_5 +
               "', @id_tbl = '" + flujo_In.dato_6 +
               "', @estado = '" + flujo_In.dato_7 +
               "', @cliente = '" + flujo_In.dato_8 + "'";


            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        #endregion

        #region Flujos CRM

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Flujo_Company")]
        public async Task<List<Flujo_Respuesta>> Get_Company(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_report_flujos  @company_id= " + flujo_In.dato;
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Company")]
        public async Task<List<Flujo_Respuesta>> Get_CompanyId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_company  @company_id= '" + flujo_In.dato + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Company/Campaign")]
        public async Task<List<Flujo_Respuesta>> Get_CompanyCampaign(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_company_campaign  @company_id= '" + flujo_In.dato + "',@campaign= '" + flujo_In.dato_1 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Company/Campaign/Carga")]
        public async Task<List<Flujo_Respuesta>> Get_CompanyCampaignCarga(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_company_campaign_carga  @campaing= '" + flujo_In.dato + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();
        }
        #endregion

        #region DashBoard

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Dashboard/Url")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_DashboardUrl(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_online_obtener_url  @lead_id= '" + flujo_In.dato + "',@flujo= '" + flujo_In.dato_2 + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Dashboard/LiberarCalidad")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_DashboardLiberarCalidad(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_online_liberar_registro_calidad  @lead_id= '" + flujo_In.dato + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }
        #endregion

        #region Carga Detalles

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle")]
        public async Task<List<Flujo_Respuesta>> Get_CargaDetalle(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_nombre_carga_detalle_v2  @campaing='" + flujo_In.dato + "',@carga='" + flujo_In.dato_1 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Actualizacion/Registros")]
        public async Task<List<Flujo_Respuesta>> Get_ActualizacionRegistros(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_actualizacion_datos_ventas  @id='" + flujo_In.dato + "',@correo='" + flujo_In.dato_1 + "',@fecha_nacimiento='" + flujo_In.dato_2 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/UltimoIntento")]
        public async Task<List<UltimoIntento>> Get_CargaDetalleUltimoIntento(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_ultimointento  @carga_id='" + flujo_In.dato + "', @flujo_id = '" + flujo_In.dato_1 + "'";
            return await _context.ultimoIntentos.FromSqlRaw(stored).AsNoTracking().ToListAsync();



        }

        
        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/ValidacionVenta")]
        public async Task<List<ValidacionVenta>> Get_CargaValidacionVenta(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_validacionventa  @carga_id='" + flujo_In.dato + "', @flujo_id = '" + flujo_In.dato_1 + "'";
            return await _context.validacionVentas.FromSqlRaw(stored).AsNoTracking().ToListAsync();



        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/EstadoVentas")]
        public async Task<List<Flujo_Respuesta>> Get_CargaEstadoVentas(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_formato_estadoventas  @tipo='" + flujo_In.dato + "', @ini = '" + flujo_In.dato_1 + "', @fin = '" + flujo_In.dato_2 + "', @campa = '" + flujo_In.dato_3 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();



        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Panel/Tematico")]
        public async Task<List<PanelTematico>> Get_PanelTematico(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_panelcontroltematico  @id='" + flujo_In.dato + "', @ini = '" + flujo_In.dato_1 + "', @fin = '" + flujo_In.dato_2  + "'";
            return await _context.panelTematicos.FromSqlRaw(stored).AsNoTracking().ToListAsync();



        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/ValidacionVenta/Calidad")]
        public async Task<List<ValidacionVenta>> Get_CargaValidacionVentacalidad(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_validacionventa_resultante  @fecha='" + flujo_In.dato + "', @flujo_id = '" + flujo_In.dato_1 + "'";
            return await _context.validacionVentas.FromSqlRaw(stored).AsNoTracking().ToListAsync();



        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id")]
        public async Task<List<CargaBasesFlujo>> Get_CargaDetalleId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_nombre_carga_detalle_Id  @carga_id='" + flujo_In.dato + "', @flujo_id = '" + flujo_In.dato_1 + "'";
            return await _context.cargaBasesFlujos.FromSqlRaw(stored).AsNoTracking().ToListAsync();



        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/Fechas")]
        public async Task<List<CargaBasesFlujo>> Get_CargaDetalleIdReporteFechas(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_nombre_carga_detalle_Id_Fechas  @ini='" + flujo_In.dato + "', @fin = '" + flujo_In.dato_1 + "', @company = '" + flujo_In.dato_2 + "'";


            return await _context.cargaBasesFlujos.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/Carga")]
        public async Task<List<CargaBasesFlujo>> Get_CargaDetalleIdReporteCargas(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_nombre_carga_detalle_Id_Fechas_Cargas  @ini='" + flujo_In.dato + "', @fin = '" + flujo_In.dato_1 + "', @company = '" + flujo_In.dato_2 + "'";


            return await _context.cargaBasesFlujos.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/ReporteRipley")]
        public async Task<List<Carga_Resultado_Ripley>> Get_CargaDetalleIdReporteRipley(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_nombre_carga_detalle_Id_reporteRipley  @carga_id='" + flujo_In.dato + "', @flujo_id = '" + flujo_In.dato_1 + "'";
            return await _context.carga_Resultado_Ripleys.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/Fechas/ReporteRipley")]
        public async Task<List<Carga_Resultado_Ripley>> Get_CargaDetalleIdReporteFechasRipley(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_nombre_carga_detalle_Id_reporteRipley_Fechas  @ini='" + flujo_In.dato + "', @fin = '" + flujo_In.dato_1 + "', @company = '" + flujo_In.dato_2 + "'";


            return await _context.carga_Resultado_Ripleys.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/Carga/ReporteRipley")]
        public async Task<List<Carga_Resultado_Ripley>> Get_CargaDetalleIdReporteCargasRipley(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_nombre_carga_detalle_Id_reporteRipley_Fechas_Cargas  @ini='" + flujo_In.dato + "', @fin = '" + flujo_In.dato_1 + "', @company = '" + flujo_In.dato_2 + "'";


            return await _context.carga_Resultado_Ripleys.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/Agentes")]
        public async Task<List<Flujo_Respuesta>> Get_CargaDetalleIdAgentes(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_agentes_new  @flujo='" + flujo_In.dato + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }




        #endregion

        #region Carterizacion

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carterizacion/Id/Agenda/Liberar")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionIdAgendaLiberar(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_asigna_total_agendamiento  @carga='" + flujo_In.dato + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carterizacion/Id/Agenda/Block")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionIdAgendaBlock(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_asigna_total_agendamiento_block  @carga='" + flujo_In.dato + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carterizacion/Id/Cargas/Liberar")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionIdCargasLiberar(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carga_cargadetalle_liberacarga  @carga='" + flujo_In.dato + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carterizacion/Id/Cargas/Block")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionIdCargasBlock(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carga_cargadetalle_liberacarga_block  @carga='" + flujo_In.dato + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carterizacion/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carga_carterizacion_modalcarteras  @carga='" + flujo_In.dato + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carterizacion/Id/Carteras")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionIdCarteras(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carga_carterizacion  @carga='" + flujo_In.dato + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carterizacion/Id/Carteras/Nueva")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionIdCarterasNueva(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carga_carterizacion_cartera_nuevo  @cantidad='" + flujo_In.dato + "', @carga='" + flujo_In.dato_1 + "', @nombre='" + flujo_In.dato_2 + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/Carteras/Nueva/Asignar")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionIdCarterasNuevaAsignar(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carterizacion_guarda_carterizado_nuevo  @cantidad='" + flujo_In.dato +
                "', @nombre='" + flujo_In.dato_1 +
                "', @agente='" + flujo_In.dato_2 +
                "', @block='" + flujo_In.dato_3 +
                "', @blockanexo='" + flujo_In.dato_4 +
                "', @conjunto='" + flujo_In.dato_5 +
                "', @company='" + flujo_In.dato_6 + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/Carteras/Reciclar/Detalle")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionIdCarterasReciclarDetalle(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_detalle_conjunto_item  @conjunto='" + flujo_In.dato +
                "', @marca='" + flujo_In.dato_1 +
            "', @carga='" + flujo_In.dato_2 + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Id/Carteras/Reciclar/Detalle/Reciclar")]
        public async Task<List<Flujo_Respuesta>> Get_CarterizacionIdCarterasReciclarDetalleReciclar(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carterizacion_guarda_carterizado_recicla_flujo   @cantidad='" + flujo_In.dato +
                "', @nombre='" + flujo_In.dato_1 +
                "', @agente='" + flujo_In.dato_2 +
                "', @block='" + flujo_In.dato_3 +
                "', @blockanexo='" + flujo_In.dato_4 +
                "', @new_conjunto='" + flujo_In.dato_5 +
                "', @modal_flujo='" + flujo_In.dato_6 +
                "', @company='" + flujo_In.dato_7 +
                "', @carga='" + flujo_In.dato_8 + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        #endregion

        #region Detalle Agendamientos

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Agendamientos")]
        public async Task<List<Flujo_Respuesta>> Get_CargaDetalleAgendamientos(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_asigna_total_agenda  @carga='" + flujo_In.dato +
                "',@flujo='" + flujo_In.dato_1 +
                "',@agente='" + flujo_In.dato_2 +
                "',@cantidad='" + flujo_In.dato_3 +
                "',@otro='" + flujo_In.dato_4 +
                "',@clean='" + flujo_In.dato_5 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Route("CRM/Calidad/Auditoria/Reportes")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaReportes(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_new_reportes   @ini= '" + flujo_In.dato + "',@fin= '" + flujo_In.dato_2 + "',@flujo= " + flujo_In.dato_3;
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/ListadoAgendamientos")]
        public async Task<List<Flujo_Respuesta>> Get_CargaDetalleListadoAgendamientos(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_listado_agendamiento  @carga='" + flujo_In.dato +
                "',@agente='" + flujo_In.dato_1 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }
        #endregion

        #region Gestion

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Gestion/Mes/Venta")]
        public async Task<List<Flujo_Respuesta>> Get_GestionMesVenta(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_mes_ventas  @company='" + flujo_In.dato + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Gestion/Mes/Venta/Carga")]
        public async Task<List<Flujo_Respuesta>> Get_GestionMesVentaCarga(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_mes_ventas_tipo  @company='" + flujo_In.dato + "', @carga='" + flujo_In.dato_1 + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }
        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Gestion/Venta/Dia")]
        public async Task<List<Flujo_Respuesta>> Get_GestionVentaDia(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia  @fecha='" + flujo_In.dato + "', @company='" + flujo_In.dato_1 + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Gestion/Venta/Dia/Carga")]
        public async Task<List<Flujo_Respuesta>> Get_GestionVentaDiaCarga(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_tipo  @fecha='" + flujo_In.dato + "', @company='" + flujo_In.dato_1 + "', @carga='" + flujo_In.dato_2 + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        #endregion

        #region Agente


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Agente/Dia")]
        public async Task<List<Flujo_Respuesta>> Get_AgenteaDia(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_visualizar_reporte_agente_dia  @fecha='" + flujo_In.dato + "', @fecha_fin='" + flujo_In.dato_1 + "', @company='" + flujo_In.dato_2 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Agente/Dia/Tipo")]
        public async Task<List<Flujo_Respuesta>> Get_AgenteaDiaTipo(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_visualizar_reporte_agente_dia_tipo  @fecha='" + flujo_In.dato + "', @fecha_fin='" + flujo_In.dato_1 + "', @company='" + flujo_In.dato_2 + "', @tipo='" + flujo_In.dato_3 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Agente/Dia/Carga")]
        public async Task<List<Flujo_Respuesta>> Get_AgenteaDiaCarga(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_visualizar_reporte_agente_dia_carga  @fecha='" + flujo_In.dato + "', @fecha_fin='" + flujo_In.dato_1 + "', @company='" + flujo_In.dato_2 + "', @carga='" + flujo_In.dato_3 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Agente/WK")]
        public async Task<List<Flujo_Respuesta>> Get_AgenteWK(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_visualizar_reporte_agente_venta_wk  @fecha='" + flujo_In.dato + "', @company='" + flujo_In.dato_1 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Agente/WK/Carga")]
        public async Task<List<Flujo_Respuesta>> Get_AgenteWKCarga(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_visualizar_reporte_agente_venta_wk_carga  @fecha='" + flujo_In.dato + "', @company='" + flujo_In.dato_1 + "', @carga='" + flujo_In.dato_2 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }
        #endregion

        #region Segmentador


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Segmentador")]
        public async Task<List<Flujo_Respuesta>> Get_Segmentador(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_segmentador_flujo  @fecha='" + flujo_In.dato + "', @company='" + flujo_In.dato_1 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Gestion/Regrabaciones")]
        public async Task<List<Flujo_Respuesta>> Get_GestionRegrabacion(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_listar_regrabaciones  @fecha='" + flujo_In.dato + "', @company='" + flujo_In.dato_1 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        #endregion

        #region Ftp


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Ftp")]
        public async Task<List<Flujo_Respuesta>> Get_Ftp(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_ventas_ftp_flujo  @fecha='" + flujo_In.dato + "', @company='" + flujo_In.dato_1 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Ftp/Salco2")]
        public async Task<List<Flujo_Respuesta>> Get_FtpSalco2(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_ventas_ftp_salco_sav_asegurado  @fecha='" + flujo_In.dato + "', @company='" + flujo_In.dato_1 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Ftp/Grabaciones")]
        public async Task<List<Flujo_Respuesta>> Get_FtpGrabaciones(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_ventas_ftp_flujo_grabaciones  @fecha='" + flujo_In.dato + "', @company='" + flujo_In.dato_1 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }
        #endregion

        #region Grabaciones


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Grabaciones/Listado")]
        public async Task<List<Flujo_Respuesta>> Get_GrabacionesListado(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_listado_grabaciones_orkesta  @lead_id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Grabaciones/listado_Grabaciones_Regrabaciones")]
        public async Task<List<Flujo_Respuesta>> Get_Grabacioneslistado_Grabaciones_Regrabaciones(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_listado_grabaciones_orkesta_regrabaciones  @lead_id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Grabaciones/Listado/Asigna")]
        public async Task<List<Flujo_Respuesta>> Get_GrabacionesListadoAsigna(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_listado_grabaciones_orkesta_unico  @id='" + flujo_In.dato + "', @record='" + flujo_In.dato_1 + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Grabaciones/listado_Grabaciones_Regrabaciones/Asigna")]
        public async Task<List<Flujo_Respuesta>> Get_Grabacioneslistado_Grabaciones_RegrabacionesAsigna(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_listado_grabaciones_orkesta_unico_regrabaciones   @id='" + flujo_In.dato + "', @record='" + flujo_In.dato_1 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        #endregion                      

        #region Calidad

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabaciones(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion   @ini='" + flujo_In.dato + "', @fin='" + flujo_In.dato_1 + "', @company='" + flujo_In.dato_2 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Mes")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadMes()
        {
            string stored = "exec sp_calidad_admin_mes  ";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador")]
        public async Task<List<Calidad_Admin_List>> Get_CalidadAdministrador(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list   @ini='" + flujo_In.dato + "';";

            return await _context.calidad_Admin_Lists.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Apeladas")]
        public async Task<List<Calidad_Admin_List_Apeladas>> Get_CalidadAdministradorApeladas(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list_apelados   @ini='" + flujo_In.dato + "';";

            return await _context.calidad_Admin_List_Apeladas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Total")]
        public async Task<List<Calidad_Admin_List_Total>> Get_CalidadAdministradortotal(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list_id_total   @ini='" + flujo_In.dato + "', @id='" + flujo_In.dato_1 + "';";

            return await _context.calidad_Admin_List_Totals.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Total/Pendiente")]
        public async Task<List<Calidad_Admin_List_Total>> Get_CalidadAdministradortotalPendiente(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list_id_noasignadas   @ini='" + flujo_In.dato + "', @id='" + flujo_In.dato_1 + "';";

            return await _context.calidad_Admin_List_Totals.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Reasigna/Edad")]


        public async Task<List<Flujo_Respuesta>> Get_ReasignaAgente(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carterizacion_edad  @campaign_flujo='" + flujo_In.dato + "', @nombre_carga = '" + flujo_In.dato_1 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Reasigna/Localizacion")]
        public async Task<List<Flujo_Respuesta>> Get_ReasignaLocalizacion(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carterizacion_regiones  @campaign_flujo='" + flujo_In.dato + "', @nombre_carga = '" + flujo_In.dato_1 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Reasigna/Semana")]
        public async Task<List<Flujo_Respuesta>> Get_ReasignaSemana(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carterizacion_semana  @campaign_flujo='" + flujo_In.dato + "', @nombre_carga = '" + flujo_In.dato_1 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Reasigna/Segmento")]
        public async Task<List<Flujo_Respuesta>> Get_ReasignaSegmento(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carterizacion_segmento  @campaign_flujo='" + flujo_In.dato + "', @nombre_carga = '" + flujo_In.dato_1 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Reasigna/SegmentoSemana")]
        public async Task<List<Flujo_Respuesta>> Get_ReasignaSegmentoSemana(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_carterizacion_segmentosemana  @campaign_flujo='" + flujo_In.dato + "', @nombre_carga = '" + flujo_In.dato_1 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Total/Pendiente/Full")]
        public async Task<List<Calidad_Admin_List_Total>> Get_CalidadAdministradortotalPendiente_Full(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list_id_noasignadas  @ini='" + flujo_In.dato + "', @id='" + flujo_In.dato_1 + "';";

            return await _context.calidad_Admin_List_Totals.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Total/Asignados/Detalle")]
        public async Task<List<Calidad_Admin_List_Asignadas>> Get_CalidadAdministradortotalAsignadosDetalle(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list_id_asignadas   @ini='" + flujo_In.dato + "', @id='" + flujo_In.dato_1 + "';";

            return await _context.calidad_Admin_List_Asignadas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Total/NoAsignados/Detalle")]
        public async Task<List<Calidad_Admin_List_Asignadas>> Get_CalidadAdministradortotalNoAsignadosDetalle(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list_id_noasignadas_total   @ini='" + flujo_In.dato + "', @id='" + flujo_In.dato_1 + "';";

            return await _context.calidad_Admin_List_Asignadas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Total/Apelados/Detalle")]
        public async Task<List<Calidad_Admin_List_Apelados>> Get_CalidadAdministradortotalApelados(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list_id_apelaciones   @ini='" + flujo_In.dato + "', @id='" + flujo_In.dato_1 + "';";

            return await _context.calidad_Admin_List_Apelados.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Agentes")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadAdministradorAgentes()
        {
            string stored = "exec sp_calidad_admin_agentes ;";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Rut")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadAdministradorRut(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_rut  @rut='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/AsignaRegistros")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadAdministradorAsignaRegistros(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            Input = Input.Replace("{", "{{").Replace("}", "}}");

            string stored = "exec sp_calidad_admin_asigna_registros @objeto='" + Input.ToString() + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).ToListAsync();


        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Grabaciones/Agente/Mio")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadAdministradorAsignaRegistrosAgente(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list_id_asignadas_agente   @ini='" + flujo_In.dato_1 + "', @agente='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Grabaciones/Agente/NoVenta")]
        public async Task<List<CallIntro_Gestion_NoVenta>> Get_CalidadAdministradorAsignaRegistrosNoVenta(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_listado_noventa   @ini='" + flujo_In.dato_1  + "';";

            return await _context.callIntro_Gestion_NoVentas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Administrador/Grabaciones/Agente/Auditor")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadAdministradorAsignaRegistrosAgenteAuditor(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_calidad_admin_list_id_asignadas_agente   @ini='" + flujo_In.dato_1 + "', @agente='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Grabaciones/Search")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_GrabacionesSearch(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_listar_grabaciones_flujo @flujo= '" + flujo_In.dato + "',@lead_id= '" + flujo_In.dato_2 + "',@rut= '" + flujo_In.dato_3
                + "',@ini='" + flujo_In.dato_4 + "',@fin= '" + flujo_In.dato_5 + "',@agente= '" + flujo_In.dato_6 + "',@company= '" + flujo_In.dato_7 + "'";


            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Apelaciones")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesApelaciones(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_apelacion   @ini='" + flujo_In.dato + "', @fin='" + flujo_In.dato_1 + "', @company='" + flujo_In.dato_2 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Apelaciones/Respuesta")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesApelacionesRespuesta(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_apelacion_resp   @ini='" + flujo_In.dato + "', @fin='" + flujo_In.dato_1 + "', @company='" + flujo_In.dato_2 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Regrabacion")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacion(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_regrabado   @ini='" + flujo_In.dato + "', @fin='" + flujo_In.dato_1 + "', @company='" + flujo_In.dato_2 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Auditoria/Liberar")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaLiberar(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_liberar_registros_calidad   @lead= '" + flujo_In.dato + "'";


            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/RollBack")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_RollBack(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_actualiza_calidad_estado_rollback @id='" + flujo_In.dato + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/RollBack/NoVenta")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_RollBackNoVenta(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_actualiza_calidad_estado_rollback_NoVenta @id='" + flujo_In.dato + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Auditoria/Save")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaSave(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            Input = Input.Replace("{", "{{").Replace("}", "}}");


            string stored = "exec sp_inserta_venta_auditoria_new   @id= '" + Input.ToString() + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Auditoria/Save/NoVenta")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaSaveNoVenta(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            Input = Input.Replace("{", "{{").Replace("}", "}}");


            string stored = "exec sp_inserta_venta_auditoria_new_noventa   @id= '" + Input.ToString() + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Auditoria/Agente")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaAgente(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_name_id_new   @id= '" + flujo_In.dato + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/EstadoCalidad")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadEstado()
        {
            string stored = "exec sp_estado_Calidad  ";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Auditoria/Motivos")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaMotivos(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_estado_Calidad_Motivo   @id_estado= '" + flujo_In.dato + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/BICE/Ripley/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionBICERipleyId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_bice_ripley   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }
        
        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/Chubb/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionChubbId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_chubb   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Chubb/Id/NoVenta")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadNoVenta(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_chubb_noventa   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/BICE/ABCDIN/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionBICEABCDINId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_bice_abcdin   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/Entel/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionEntelId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_entel   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Auditoria/Save/Regrabacion")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaSaveRegrabacion(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_inserta_venta_auditoria_regrabacion   @id= '" + flujo_In.dato + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Auditoria/Save/Regrabacion/SinContacto")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaSaveRegrabacionSincontacto(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_inserta_venta_auditoria_regrabacion_sincontacto   @id= '" + flujo_In.dato + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/BICE/Ripley/Id/R")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionBICERipleyIdR(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_bice_ripley_R   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/BICE/ABCDIN/Id/R")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionBICEABCDINIdR(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_bice_abcdin_R   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/Entel/Id/R")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionEntelIdR(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_entel_R   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }




        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/Chubb/Lapolar/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionChubbLapolarId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_chubb_lapolar   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/Bice/Lider/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionBiceLiderId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_bice_lider   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/Salco/SAV/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionSalcoSAVId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_salco_sav   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/Metlife/RS/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionMetlifeRSId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_metlife_rs   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Grabaciones/Grabaciones/Metlife/BICE/Id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadGrabacionesRegrabacionMetlifeBICEId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_venta_dia_grabacion_id_metlife_bice   @id='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        #endregion

        #region Apelacion

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Apelacion/Auditoria/id")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadApelacionAuditoriaId(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_gestion_apelacion_id  @id= '" + flujo_In.dato + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Apelacion/Auditoria/id/respuesta")]
        public async Task<List<Flujo_Respuesta>> Get_CalidadApelacionAuditoriaIdRespuesta(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_inserta_venta_auditoria_apelacion_respuesta  @id= '" + flujo_In.dato + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Apelacion/Auditoria/ASaveApela")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaASaveApela(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_inserta_venta_auditoria_apelacion_new   @id= '" + flujo_In.dato + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Apelacion/Auditoria/ASaveNoApela")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadAuditoriaASaveNoApela(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_inserta_venta_auditoria_Noapelacion_new   @id= '" + flujo_In.dato + "'";


            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        #endregion

        #region Mandato

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Mandato")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_MandatoResumenDash(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_mostrar_mandatos  @ini='" + flujo_In.dato + "', @fin='" + flujo_In.dato_1 + "', @flujo='" + flujo_In.dato_2 + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Mandato/Ingreso")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_MandatoIngreso(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_inserta_mandato  @mandato= '" + flujo_In.dato + "',@poliza= '" + flujo_In.dato_2 + "',@lead= '" + flujo_In.dato_3 + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Mandato/Anular")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_AnularMandato(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_anular_mandato  @mandato= '" + flujo_In.dato + "',@poliza= '" + flujo_In.dato_2 + "',@lead= '" + flujo_In.dato_3 + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        #endregion

        #region Altas

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Altas/FTP")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_FTP(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_mostrar_ventas_altas  @fecha= '" + flujo_In.dato + "',@flujo= '" + flujo_In.dato_1 + "';";


            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Reporte")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadChubb(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_reporte_calidad_chubb  @fecha= '" + flujo_In.dato + "',@company= '" + flujo_In.dato_1 + "';";


            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Calidad/Reporte/NoVenta")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CalidadChubbNoVenta(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_reporte_calidad_chubb_noventa  @fecha= '" + flujo_In.dato + "',@company= '" + flujo_In.dato_1 + "';";


            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Altas/FTP/Informar")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_FTPInformar(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_informar_altas  @lead= " + flujo_In.dato;


            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }


        #endregion


        #region DashBoard

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Dash/Hopper")]
        public async Task<List<Panel_Hopper>> Get_DashHopper(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_conector_symp_hopper  @id='" + flujo_In.dato + "';";
            return await _context.panel_Hoppers.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        #endregion



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Detalle/Full")]
        public async Task<List<Flujo_Respuesta>> Get_CargaDetalleFull(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_nombre_carga_detalle_v3  @campaing='" + flujo_In.dato + "',@carga='" + flujo_In.dato_1 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }
        
        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Seguimiento/BDD")]
        public async Task<List<SeguimientoBBDD>> Get_SeguimientoBDD(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_seguimiento_bdd  @pTYPE='" + flujo_In.dato + "',@campa='" + flujo_In.dato_1 + "',@ini='" + flujo_In.dato_2 + "',@fin='" + flujo_In.dato_3 + "',@nombrecarga='" + flujo_In.dato_4 + "'";

            return await _context.seguimientoBBDDs.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Campaign")]
        public async Task<List<Flujo_Respuesta>> Get_Campaign(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_campaign_default  @campaign='" + flujo_In.dato + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/ABCDIN/Resultante/2")]
        public async Task<List<Resultante_ABCDIN_2>> Get_AbcdinResultante2(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_resultante_abcdin_2  @carga='" + flujo_In.dato + "';";
            return await _context.resultante_ABCDIN_2s.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/FlujosCarga")]
        public async Task<List<Flujo_Respuesta>> Get_FlujosCarga(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_url_carga  @company_id='" + flujo_In.dato + "',@flujo='" + flujo_In.dato_2 + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/DetalleCargas/CargasDetalleResumenDash")]
        public async Task<ActionResult<IEnumerable<CRM_Carga_Resumen_Dash>>> Get_CargasDetalleResumenDash(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_nombre_carga_resumen_new  @carga= '" + flujo_In.dato + "'";
            return await _context.cRM_Carga_Resumen_Dashes.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/DetalleCargas/Liberar")]
        public async Task<ActionResult<IEnumerable<Flujo_Out_respuesta>>> Get_CargasDetalleLiberar(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_carga_cargadetalle_liberacarga  @id= '" + flujo_In.dato + "'";
            return await _context.flujo_Out_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/DetalleCargas/Bloquear")]
        public async Task<ActionResult<IEnumerable<Flujo_Out_respuesta>>> Get_CargasDetalleBloquear(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_carga_cargadetalle_liberacarga_block  @id= '" + flujo_In.dato + "'";
            return await _context.flujo_Out_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/DetalleCargasAdmin/CargasDetalleResumenDashDetalle")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CargasDetalleResumenDashAdminDetalle(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_detalle_lista  @lista= '" + flujo_In.dato + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/DetalleCargasAdmin/CargasDetalleResumenDashDetalleLiberar")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CargasDetalleResumenDashAdminDetalleLiberar(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_lberar_registros_list  @codigo= '" + flujo_In.dato + "',@list= '" + flujo_In.dato_2 + "'";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("UF")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_UF()
        {

            string stored = "exec sp_uf ";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/DetalleCargasAdmin/CargasDetalleResumenDash")]
        public async Task<ActionResult<IEnumerable<CRM_Carga_Resumen_DashAdmin>>> Get_CargasDetalleResumenDashAdmin(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_nombre_carga_resumen_new_admin  @carga= '" + flujo_In.dato + "'";

            return await _context.cRM_Carga_Resumen_DashAdmins.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Agentes")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_CargasDetalleCarterizacionModalAgentes(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_agentes_new @flujo= '" + flujo_In.dato + "';";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Flujo_Menu_Body")]
        public async Task<List<Menu_Body>> Get_MenuBody(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_menu_body " +
                            "@id='" + flujo_In.dato + "'," +
                            "@menu_opcion='" + flujo_In.dato_2 + "';";

            return await _context.menu_Body.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Flujo_Menu_Header")]
        public async Task<List<Menu_Header>> Get_MenuHeader(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_menu_header " +
                            "@id='" + flujo_In.dato + "'," +
                            "@menu_opcion='" + flujo_In.dato_2 + "';";

            return await _context.menu_Headers.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/FlujoReporte")]
        public async Task<List<Flujo_Respuesta>> Get_FlujoReporte(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_reporte_default @campaign='" + flujo_In.dato + "';";

            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Route("CRM/Cargas/Id")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_DetalleCargasId(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_nombre_cargas_flujo_id_new  @id= '" + flujo_In.dato + "'";
            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/GestionCarga")]
        public async Task<List<Gestion_Carga>> Get_GestionCarga()
        {
            string stored = "exec sp_gestion_carga";
            return await _context.gestion_Cargas.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }



        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/GestionEjecutivo")]
        public async Task<List<Gestion_Ejecutivo>> Get_GestionEjecutivo()
        {
            string stored = "exec sp_gestion_ejecutivo";
            return await _context.gestion_Ejecutivos.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/GestionDashDia")]
        public async Task<List<Gestion_Dash_dia>> Get_GestionDashDia(Flujo_ingreso flujo_Ingreso)
        {
            string stored = "exec sp_dash_dia @flujo='" + flujo_Ingreso.dato + "'";
            return await _context.gestion_Dash_Dias.FromSqlRaw(stored).AsNoTracking().ToListAsync();


        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/GestionDashDiaNoConecta")]
        public async Task<List<Gestion_NC>> Get_GestionDashDiaNoConecta(Flujo_ingreso flujo_Ingreso)
        {
            string stored = "exec sp_noconecta_resultado @flujo='" + flujo_Ingreso.dato + "'";
            return await _context.gestion_NCs.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/GestionDashDiaNoConectaRadar")]
        public async Task<List<Gestion_NC_Radar>> Get_GestionDashDiaNoConectaRadar(Flujo_ingreso flujo_Ingreso)
        {
            string stored = "exec sp_noconecta_resultado @flujo='" + flujo_Ingreso.dato + "'";
            return await _context.gestion_NC_Radars.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        #region Cargas


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Validador")]
        public async Task<ActionResult<IEnumerable<Cargador_Validador>>> Get_CargadorValidador(Flujo_ingreso flujo_in)
        {
            string stored = "exec sp_info_carga_list @carga='" + flujo_in.dato + "';";

            return await _context.cargador_Validadors.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }


        [SupportedOSPlatform("windows")]
        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Chubb/Carga/SonrieSeguro")]
        public IActionResult UploadFile_Chubb(IFormFile postedFile, string flujo)
        {
            var cargador = new Cargador();
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;


            string dire = Directory.GetCurrentDirectory().ToString();


            if (postedFile == null || postedFile.Length == 0)
            {
                cargador.flujo = "Archivo No Seleccionado";
                return Ok(cargador);

            }

            var supportedTypes = new[] { "xlsx" };
            var fileExt = System.IO.Path.GetExtension(postedFile.FileName).Substring(1);

            if (!supportedTypes.Contains(fileExt))
            {
                cargador.flujo = "Archivo No Soportado";
                return Ok(cargador);
            }


            if (postedFile != null)
            {

                //try
                //{
                string path = Path.Combine(dire, "Uploads");


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    cargador.flujo = postedFile.GetFilename().ToString() + " Archivo Cargado";
                }

                //Read the connection string for the Excel file.
                string conString = this.Configuration.GetConnectionString("ExcelConString");
                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);


                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }


                string carga = "";




                foreach (DataRow row in dt.Rows)
                {
                    carga = row["Carga"].ToString();
                }

                conString = this.Configuration.GetConnectionString("constr");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    String sql = "SELECT count(*) Cantidad FROM dbo.Carga_Bases_Flujo where nombre_carga ='" + carga.ToString() + "'";

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        Int32 count = (Int32)command.ExecuteScalar();
                        if (count != 0)
                        {
                            con.Close();
                            cargador.flujo = "Nombre de Carga Duplicado En Base";
                            return Ok(cargador);
                        }
                        else
                        {

                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name.
                                sqlBulkCopy.DestinationTableName = "dbo.Carga_Bases_Flujo";

                                //[OPTIONAL]: Map the Excel columns with that of the database table.
                                sqlBulkCopy.ColumnMappings.Add("Carga", "nombre_carga");
                                sqlBulkCopy.ColumnMappings.Add("numero_documento", "Chubb_numero_documento");
                                sqlBulkCopy.ColumnMappings.Add("tipo_documento", "Chubb_tipo_documento");
                                sqlBulkCopy.ColumnMappings.Add("nombre", "Chubb_nombre");
                                sqlBulkCopy.ColumnMappings.Add("tipo_tarjeta", "Chubb_tipo_tarjeta");
                                sqlBulkCopy.ColumnMappings.Add("fecha_nacimiento", "Chubb_fecha_nacimiento");
                                sqlBulkCopy.ColumnMappings.Add("edad", "Chubb_edad");
                                sqlBulkCopy.ColumnMappings.Add("sexo", "Chubb_sexo");
                                sqlBulkCopy.ColumnMappings.Add("celular1", "Chubb_celular1");
                                sqlBulkCopy.ColumnMappings.Add("celular2", "Chubb_celular2");
                                sqlBulkCopy.ColumnMappings.Add("fijo1", "Chubb_fijo1");
                                sqlBulkCopy.ColumnMappings.Add("fijo2", "Chubb_fijo2");
                                sqlBulkCopy.ColumnMappings.Add("email", "Chubb_email");
                                sqlBulkCopy.ColumnMappings.Add("direccion", "Chubb_direccion");
                                sqlBulkCopy.ColumnMappings.Add("distrito", "Chubb_distrito");
                                sqlBulkCopy.ColumnMappings.Add("provincia", "Chubb_provincia");
                                sqlBulkCopy.ColumnMappings.Add("departamento", "Chubb_departamento");
                                sqlBulkCopy.ColumnMappings.Add("fecha_colocacion", "Chubb_fecha_colocacion");
                                sqlBulkCopy.ColumnMappings.Add("tienda_colocacion", "Chubb_tienda_colocacion");
                                sqlBulkCopy.ColumnMappings.Add("fecha_ult_consumo", "Chubb_fecha_ult_consumo");
                                sqlBulkCopy.ColumnMappings.Add("tienda_ult_consumo", "Chubb_tienda_ult_consumo");
                                sqlBulkCopy.ColumnMappings.Add("condicion_laboral", "Chubb_condicion_laboral");
                                sqlBulkCopy.ColumnMappings.Add("zona", "Chubb_zona");
                                sqlBulkCopy.ColumnMappings.Add("recencia", "Chubb_recencia");
                                sqlBulkCopy.ColumnMappings.Add("cantidad_seguros", "Chubb_cantidad_seguros");
                                sqlBulkCopy.ColumnMappings.Add("seguros_contratados", "Chubb_seguros_contratados");
                                sqlBulkCopy.ColumnMappings.Add("clase_puntos_beneficios", "Chubb_clase_puntos_beneficios");
                                sqlBulkCopy.ColumnMappings.Add("producto_ripley", "Chubb_producto_ripley");
                                sqlBulkCopy.ColumnMappings.Add("fecha_compra_ultimo_seguro", "Chubb_fecha_compra_ultimo_seguro");
                                sqlBulkCopy.ColumnMappings.Add("tiene_seguro", "Chubb_tiene_seguro");
                                sqlBulkCopy.ColumnMappings.Add("fecha_pago_tc", "Chubb_fecha_pago_tc");
                                sqlBulkCopy.ColumnMappings.Add("marca_call", "Chubb_marca_call");
                                sqlBulkCopy.ColumnMappings.Add("marca_pd", "Chubb_marca_pd");
                                sqlBulkCopy.ColumnMappings.Add("canal_consumo", "Chubb_canal_consumo");
                                sqlBulkCopy.ColumnMappings.Add("segmento_rfm_spos", "Chubb_segmento_rfm_spos");
                                sqlBulkCopy.ColumnMappings.Add("segmento_rfm_tienda", "Chubb_segmento_rfm_tienda");
                                sqlBulkCopy.ColumnMappings.Add("tipo_captacion", "Chubb_tipo_captacion");
                                sqlBulkCopy.ColumnMappings.Add("nombre_call", "Chubb_nombre_call");
                                sqlBulkCopy.ColumnMappings.Add("tipo_base", "Chubb_tipo_base");
                                sqlBulkCopy.ColumnMappings.Add("Campaña", "Chubb_Campana");
                                sqlBulkCopy.ColumnMappings.Add("Variable_1", "Chubb_Variable_1");
                                sqlBulkCopy.ColumnMappings.Add("Variable2", "Chubb_Variable2");
                                sqlBulkCopy.ColumnMappings.Add("Variable3", "Chubb_Variable3");
                                sqlBulkCopy.ColumnMappings.Add("Variable4", "Chubb_Variable4");
                                sqlBulkCopy.ColumnMappings.Add("Variable5", "Chubb_Variable5");
                                sqlBulkCopy.ColumnMappings.Add("CampoCarterizar", "Chubb_CampoCarterizar");


                                cargador.nombre = carga.ToString();
                                //con.Open();
                                sqlBulkCopy.WriteToServer(dt);


                                SqlCommand cmd_2 = new SqlCommand("sp_info_carga_chubb_sonrie_seguro", con);
                                cmd_2.CommandType = CommandType.StoredProcedure;
                                cmd_2.Parameters.AddWithValue("@carga", carga.ToString());
                                cmd_2.ExecuteNonQuery();

                                con.Close();
                            }
                        }


                    }
                }

                return Ok(cargador);
                //}
                //catch (Exception)
                //{
                //    cargador.flujo = "Formato No Valido (Columnas no Validas)";
                //    return Ok(cargador);

                //}

                //Create a Folder.

            }
            return Ok(cargador);

        }

        [SupportedOSPlatform("windows")]
        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Chubb/Carga/AccidenteAsistencia")]
        public IActionResult UploadFile_Chubb_Accidente_Asistencia(IFormFile postedFile, string flujo)
        {
            var cargador = new Cargador();
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;


            string dire = Directory.GetCurrentDirectory().ToString();


            if (postedFile == null || postedFile.Length == 0)
            {
                cargador.flujo = "Archivo No Seleccionado";
                return Ok(cargador);

            }

            var supportedTypes = new[] { "xlsx" };
            var fileExt = System.IO.Path.GetExtension(postedFile.FileName).Substring(1);

            if (!supportedTypes.Contains(fileExt))
            {
                cargador.flujo = "Archivo No Soportado";
                return Ok(cargador);
            }


            if (postedFile != null)
            {

                //try
                //{
                string path = Path.Combine(dire, "Uploads");


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    cargador.flujo = postedFile.GetFilename().ToString() + " Archivo Cargado";
                }

                //Read the connection string for the Excel file.
                string conString = this.Configuration.GetConnectionString("ExcelConString");
                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);


                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }


                string carga = "";




                foreach (DataRow row in dt.Rows)
                {
                    carga = row["Carga"].ToString();
                }

                conString = this.Configuration.GetConnectionString("constr");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    String sql = "SELECT count(*) Cantidad FROM dbo.Carga_Bases_Flujo where nombre_carga ='" + carga.ToString() + "'";

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        Int32 count = (Int32)command.ExecuteScalar();
                        if (count != 0)
                        {
                            con.Close();
                            cargador.flujo = "Nombre de Carga Duplicado En Base";
                            return Ok(cargador);
                        }
                        else
                        {

                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name.
                                sqlBulkCopy.DestinationTableName = "dbo.Carga_Bases_Flujo";

                                //[OPTIONAL]: Map the Excel columns with that of the database table.
                                sqlBulkCopy.ColumnMappings.Add("Carga", "nombre_carga");
                                sqlBulkCopy.ColumnMappings.Add("numero_documento", "Chubb_numero_documento");
                                sqlBulkCopy.ColumnMappings.Add("tipo_documento", "Chubb_tipo_documento");
                                sqlBulkCopy.ColumnMappings.Add("nombre", "Chubb_nombre");
                                sqlBulkCopy.ColumnMappings.Add("tipo_tarjeta", "Chubb_tipo_tarjeta");
                                sqlBulkCopy.ColumnMappings.Add("fecha_nacimiento", "Chubb_fecha_nacimiento");
                                sqlBulkCopy.ColumnMappings.Add("edad", "Chubb_edad");
                                sqlBulkCopy.ColumnMappings.Add("sexo", "Chubb_sexo");
                                sqlBulkCopy.ColumnMappings.Add("celular1", "Chubb_celular1");
                                sqlBulkCopy.ColumnMappings.Add("celular2", "Chubb_celular2");
                                sqlBulkCopy.ColumnMappings.Add("fijo1", "Chubb_fijo1");
                                sqlBulkCopy.ColumnMappings.Add("fijo2", "Chubb_fijo2");
                                sqlBulkCopy.ColumnMappings.Add("email", "Chubb_email");
                                sqlBulkCopy.ColumnMappings.Add("direccion", "Chubb_direccion");
                                sqlBulkCopy.ColumnMappings.Add("distrito", "Chubb_distrito");
                                sqlBulkCopy.ColumnMappings.Add("provincia", "Chubb_provincia");
                                sqlBulkCopy.ColumnMappings.Add("departamento", "Chubb_departamento");
                                sqlBulkCopy.ColumnMappings.Add("fecha_colocacion", "Chubb_fecha_colocacion");
                                sqlBulkCopy.ColumnMappings.Add("tienda_colocacion", "Chubb_tienda_colocacion");
                                sqlBulkCopy.ColumnMappings.Add("fecha_ult_consumo", "Chubb_fecha_ult_consumo");
                                sqlBulkCopy.ColumnMappings.Add("tienda_ult_consumo", "Chubb_tienda_ult_consumo");
                                sqlBulkCopy.ColumnMappings.Add("condicion_laboral", "Chubb_condicion_laboral");
                                sqlBulkCopy.ColumnMappings.Add("zona", "Chubb_zona");
                                sqlBulkCopy.ColumnMappings.Add("recencia", "Chubb_recencia");
                                sqlBulkCopy.ColumnMappings.Add("cantidad_seguros", "Chubb_cantidad_seguros");
                                sqlBulkCopy.ColumnMappings.Add("seguros_contratados", "Chubb_seguros_contratados");
                                sqlBulkCopy.ColumnMappings.Add("clase_puntos_beneficios", "Chubb_clase_puntos_beneficios");
                                sqlBulkCopy.ColumnMappings.Add("producto_ripley", "Chubb_producto_ripley");
                                sqlBulkCopy.ColumnMappings.Add("fecha_compra_ultimo_seguro", "Chubb_fecha_compra_ultimo_seguro");
                                sqlBulkCopy.ColumnMappings.Add("tiene_seguro", "Chubb_tiene_seguro");
                                sqlBulkCopy.ColumnMappings.Add("fecha_pago_tc", "Chubb_fecha_pago_tc");
                                sqlBulkCopy.ColumnMappings.Add("marca_call", "Chubb_marca_call");
                                sqlBulkCopy.ColumnMappings.Add("marca_pd", "Chubb_marca_pd");
                                sqlBulkCopy.ColumnMappings.Add("canal_consumo", "Chubb_canal_consumo");
                                sqlBulkCopy.ColumnMappings.Add("segmento_rfm_spos", "Chubb_segmento_rfm_spos");
                                sqlBulkCopy.ColumnMappings.Add("segmento_rfm_tienda", "Chubb_segmento_rfm_tienda");
                                sqlBulkCopy.ColumnMappings.Add("tipo_captacion", "Chubb_tipo_captacion");
                                sqlBulkCopy.ColumnMappings.Add("nombre_call", "Chubb_nombre_call");
                                sqlBulkCopy.ColumnMappings.Add("tipo_base", "Chubb_tipo_base");
                                sqlBulkCopy.ColumnMappings.Add("Campaña", "Chubb_Campana");
                                sqlBulkCopy.ColumnMappings.Add("Variable_1", "Chubb_Variable_1");
                                sqlBulkCopy.ColumnMappings.Add("Variable2", "Chubb_Variable2");
                                sqlBulkCopy.ColumnMappings.Add("Variable3", "Chubb_Variable3");
                                sqlBulkCopy.ColumnMappings.Add("Variable4", "Chubb_Variable4");
                                sqlBulkCopy.ColumnMappings.Add("Variable5", "Chubb_Variable5");
                                sqlBulkCopy.ColumnMappings.Add("CampoCarterizar", "Chubb_CampoCarterizar");


                                cargador.nombre = carga.ToString();
                                //con.Open();
                                sqlBulkCopy.WriteToServer(dt);


                                SqlCommand cmd_2 = new SqlCommand("sp_info_carga_chubb_accidente_asistencia", con);
                                cmd_2.CommandType = CommandType.StoredProcedure;
                                cmd_2.Parameters.AddWithValue("@carga", carga.ToString());
                                cmd_2.ExecuteNonQuery();

                                con.Close();
                            }
                        }


                    }
                }

                return Ok(cargador);
                //}
                //catch (Exception)
                //{
                //    cargador.flujo = "Formato No Valido (Columnas no Validas)";
                //    return Ok(cargador);

                //}

                //Create a Folder.

            }
            return Ok(cargador);

        }

        #endregion




        [AllowAnonymous]
        [HttpPost]
        [Route("CRM/Session_Check")]
        public async Task<ActionResult<IEnumerable<Session_out>>> GetSession_check(Session session)
        {
            string stored = "exec sp_checkSeesion @user='" + session.user + "', @gui='" + session.gui + "';";
            return await _context.Session_out.FromSqlRaw(stored).ToListAsync();


        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("CRM/CallIntroDia")]
        public async Task<ActionResult<IEnumerable<CallIntro_Gestion_Dia>>> GetSCallGestionDia(Flujo_ingreso flujo_in)
        {
            string stored = "exec sp_gestion_dia @ini='" + flujo_in.dato + "', @fin='" + flujo_in.dato_1 + "';";
            return await _context.callIntro_Gestion_Dias.FromSqlRaw(stored).ToListAsync();


        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("CRM/VentaDia")]
        public async Task<ActionResult<IEnumerable<CallIntro_Gestion_Venta>>> GetSCallGestionDiaVenta(Flujo_ingreso flujo_in)
        {
            string stored = "exec sp_gestion_dia_ventas @ini='" + flujo_in.dato + "', @fin='" + flujo_in.dato_1 + "';";
            return await _context.callIntro_Gestion_Ventas.FromSqlRaw(stored).ToListAsync();


        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CRM/NoVentaDia")]
        public async Task<ActionResult<IEnumerable<tbl_NoVentas>>> GetSCallGestionDiaNoVenta(Flujo_ingreso flujo_in)
        {
            string stored = "exec sp_gestion_dia_noventas @ini='" + flujo_in.dato + "', @fin='" + flujo_in.dato_1 + "';";
            return await _context.tbl_NoVentas.FromSqlRaw(stored).ToListAsync();


        }


        [AllowAnonymous]
        [HttpPost]
        [Route("CRM/Panel/Tematico/V2")]
        public async Task<List<PanelTematico>> Get_PanelTematicoAout(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_panelcontroltematico  @id='" + flujo_In.dato + "', @ini = '" + flujo_In.dato_1 + "', @fin = '" + flujo_In.dato_2 + "'";
            return await _context.panelTematicos.FromSqlRaw(stored).AsNoTracking().ToListAsync();



        }


        [AllowAnonymous]
        [HttpPost]
        [Route("CRM/TiemposDia")]
        public async Task<ActionResult<IEnumerable<tbl_Horas_Logeo_Full>>> GetSCallTiemposDia(Flujo_ingreso flujo_in)
        {
            string stored = "exec sp_tiempos_agentes @ini='" + flujo_in.dato + "', @fin='" + flujo_in.dato_1 + "';";
            return await _context.tbl_Horas_Logeo_Fulls.FromSqlRaw(stored).ToListAsync();


        }


        [AllowAnonymous]
        [HttpPost]
        [Route("CRM/Session_Out")]
        public async Task<ActionResult<IEnumerable<Session_out>>> GetSession_out(Session session)
        {
            string stored = "exec sp_SessionClear @user='" + session.user + "', @gui='" + session.gui + "';";
            return await _context.Session_out.FromSqlRaw(stored).ToListAsync();

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CRM/Login")]
        public async Task<ActionResult<IEnumerable<Login_out>>> GetLogin_out(Login login)
        {

            string stored = "exec sp_usuario_validar @Username='" + login.UserName + "', @Password='" + login.Password + "';";
            List<Login_out> list = await _context.Login_out.FromSqlRaw(stored).AsNoTracking().ToListAsync();

            var resultado = from s in list select s;

            List<Login_out_result> log_resul = new List<Login_out_result>();


            var id = 0;


            foreach (var lista in resultado)
                id = lista.id;


            if (id > 0)
            {
                var tokens = jwtAuthenticationManager.AuthenticateCRM(login.UserName, login.Password);

                if (tokens == null)
                {

                    return Unauthorized(new { status = true, statusCode = 401, message = "Usuario/Password Incorrecto" });

                }
                else
                {


                    foreach (var lista in resultado)
                    {
                        log_resul.Add(new Login_out_result
                        {
                            cliente = lista.cliente,
                            id_usuario = lista.id_usuario,
                            id = lista.id,
                            gui = lista.gui,
                            ruta = lista.ruta,
                            token = tokens
                        });


                    }



                    return Ok(log_resul);
                }

            }
            else

            {

                return Ok(list);
            }




        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt","text/plain"},
                {".pdf","application/pdf"},
                {".doc","application/vnd.ms-word"},
                {".docx","application/vnd.ms-word"},
                {".xls","application/vnd.ms-excel"},
                {".xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png","image/png"},
                {".jpg","image/jpeg"},
                {".jpeg","image/jpeg"},
                {".gif","image/gif"},
                {".csv","text/csv"}
            };
        }
    }
}
