using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CallSouth.Ventas.Peru.Models
{


    public partial class CargaBasesFlujo
    {

        [Key]
        public int id_registro { get; set; }
        public int Campaign { get; set; }
        public int ListId { get; set; }
        public int LeadId { get; set; }
        public string Agente { get; set; }
        public string FechaGestion { get; set; }
        public string Llamado { get; set; }
        public string LlamadoDetalle { get; set; }
        public string ObservacionAgenda { get; set; }
        public string FechaAgenda { get; set; }
        public string Interesa { get; set; }
        public string OpcionNoInteresa { get; set; }
        public string ValidaDatos { get; set; }
        public string EstadoGestion { get; set; }
        public int Intentos { get; set; }
        public string Grabacion { get; set; }
        public string EstadoCargadoOrkesta { get; set; }
        public string Carga { get; set; }
        public int FechaCarga { get; set; }
        public string FechaCargaBase { get; set; }
        public string EstadoFinalRegistros { get; set; }
        public string NombreCarga { get; set; }
        public string EstadoOrkestaDialer { get; set; }
        public int BlockingCarga { get; set; }
        public string Epoch { get; set; }
        public string Marca { get; set; }
        public string MarcaSegmentacion { get; set; }
        public string StatusCarga { get; set; }
        public string Conjunto { get; set; }
        public string SubConjunto { get; set; }
        public string TipoCarga { get; set; }
        public int MarcaLiberaAgendaAuto { get; set; }
        public int IntetoQ { get; set; }
        public string IntetoQFecha { get; set; }
        public string Validacion { get; set; }
        public string NumeroValido { get; set; }
        public string ValidacionFinal { get; set; }
        public string CampaingId { get; set; }
        public string FechaGestion2 { get; set; }
        public string FechaGestion3 { get; set; }
        public string CampaignFlujo { get; set; }
        public string chubb_numero_documento { get; set; }
        public string chubb_tipo_documento { get; set; }
        public string chubb_nombre { get; set; }
        public string chubb_tipo_tarjeta { get; set; }
        public string chubb_fecha_nacimiento { get; set; }
        public string chubb_edad { get; set; }
        public string chubb_sexo { get; set; }
        public string chubb_celular1 { get; set; }
        public string chubb_celular2 { get; set; }
        public string chubb_fijo1 { get; set; }
        public string chubb_fijo2 { get; set; }
        public string chubb_email { get; set; }
        public string chubb_direccion { get; set; }
        public string chubb_distrito { get; set; }
        public string chubb_provincia { get; set; }
        public string chubb_departamento { get; set; }
        public string chubb_fecha_colocacion { get; set; }
        public string chubb_tienda_colocacion { get; set; }
        public string chubb_fecha_ult_consumo { get; set; }
        public string chubb_tienda_ult_consumo { get; set; }
        public string chubb_condicion_laboral { get; set; }
        public string chubb_zona { get; set; }
        public string chubb_recencia { get; set; }
        public string chubb_cantidad_seguros { get; set; }
        public string chubb_seguros_contratados { get; set; }
        public string chubb_clase_puntos_beneficios { get; set; }
        public string chubb_producto_ripley { get; set; }
        public string chubb_fecha_compra_ultimo_seguro { get; set; }
        public string chubb_tiene_seguro { get; set; }
        public string chubb_fecha_pago_tc { get; set; }
        public string chubb_marca_call { get; set; }
        public string chubb_marca_pd { get; set; }
        public string chubb_canal_consumo { get; set; }
        public string chubb_segmento_rfm_spos { get; set; }
        public string chubb_segmento_rfm_tienda { get; set; }
        public string chubb_tipo_captacion { get; set; }
        public string chubb_nombre_call { get; set; }
        public string chubb_tipo_base { get; set; }
        public string chubb_campaña { get; set; }
        public string chubb_variable_1 { get; set; }
        public string chubb_variable2 { get; set; }
        public string chubb_variable3 { get; set; }
        public string chubb_variable4 { get; set; }
        public string chubb_variable5 { get; set; }
        public string chubb_campocarterizar { get; set; }

    }
    public class Calidad_Admin_List
    {
        [Key]
        public string campaignflujo { get; set; }
        public string companyname { get; set; }
        public string codigo { get; set; }
        public string company { get; set; }
        public int? total { get; set; }
        public int? informados { get; set; }
        public int? auditadas { get; set; }
        public int? asignadas { get; set; }
        public int? pendienteasignacion { get; set; }


    }


    public class UltimoIntento
    {
        [Key]
        public string dni { get; set; }
        public string intentos_contacto { get; set; }
        public string fecha_mejor_gestion { get; set; }
        public string hora_mejor_gestion { get; set; }
        public string call { get; set; }
        public string estado { get; set; }
        public string motivo { get; set; }
        public string nombre_de_bbdd { get; set; }
        public string codigo_del_ejecutivo { get; set; }
        public string nombre_ejecutivo { get; set; }

    }

    public class EstadoVentas
    {
        [Key]
        //public string decil { get; set; }
        public int? Ventas { get; set; }
        public int? Venta_S { get; set; }
        public int? N_Calidad { get; set; }
        public int? NN { get; set; }
        public int? VentaInformada { get; set; }
        public int? fecha { get; set; }
    }



    public class ValidacionVenta
    {
        [Key]
        public string dni_cliente { get; set; }
        public string fecha_de_venta { get; set; }
        public string nombre_cliente { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string nombre_ejecutivo { get; set; }
        public string codigo_ejecutivo { get; set; }
        public string nombre_producto { get; set; }
        public string primer_estado_auditoria { get; set; }
        public string primer_motivo_auditoria { get; set; }
        public string primer_fecha_auditoria { get; set; }
        public string segundo_estado_auditoria { get; set; }
        public string segundo_motivo_auditoria { get; set; }
        public string segundo_fecha_auditoria { get; set; }
        public string nombre_callcenter { get; set; }
        public string anio { get; set; }
        public string mes { get; set; }
        public string calidad_final { get; set; }
        public string telefono_venta { get; set; }
    }




    public class SeguimientoBBDD
    {
        [Key]
        public string Segmento { get; set; }
        public string Detalle { get; set; }
        public int? cargado { get; set; }
        public double? Participacion { get; set; }
        public int? Recorrido { get; set; }
        public double? PorRecorrido { get; set; }
        public int? VentaBrutas { get; set; }
        public double? PorVentas { get; set; }
        public double? PorEfectividad { get; set; }
        public double? Intensidad { get; set; }
        public double? IntensidadTelefonos { get; set; }
        public int? recorridoTotal { get; set; }
        public double? PorRecorridoTotal { get; set; }
        public double? PorEfectividadTotal { get; set; }
        public int? PorGeneral { get; set; }
        public int? PorTitular { get; set; }

    }


    public class tbl_NoVentas
    {
        [Key]
        public string nombreagente { get; set; }
        public int id_registro { get; set; }
        public DateTime? fecha_auditoria { get; set; }
        public string NoVenta_result_json { get; set; }
        public string NoVenta_Auditor { get; set; }
        public string Chubb_Campana { get; set; }
        public string lead_id { get; set; }
        public string agente { get; set; }
        public string fecha_gestion { get; set; }
        public string Opcion_No_Interesa { get; set; }
        public string grabacion { get; set; }
        public string grabacion_duracion { get; set; }
        public string Estado_Calidad { get; set; }
        public DateTime? NoVenta_Fecha_Auditor { get; set; }
        public string company { get; set; }
        public string tipo_base { get; set; }
    }

    public class CallIntro_Gestion_NoVenta
    {
        [Key]
        public int id_registro { get; set; }
        public string Chubb_Campana { get; set; }
        public string lead_id { get; set; }
        public string agente { get; set; }
        public string fecha_gestion { get; set; }
        public string Opcion_No_Interesa { get; set; }
        public string grabacion { get; set; }
        public string grabacion_duracion { get; set; }
        public string Estado_Calidad { get; set; }
        public string nombre { get; set; }
    }



    public class tbl_Horas_Logeo_Full
    {
        [Key]
        public string campaign_id { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string calls { get; set; }
        public string talk { get; set; }
        public string full_name { get; set; }
        public string agente { get; set; }
        public string pause_sec { get; set; }
        public string wait_sec { get; set; }
        public string dispo_sec { get; set; }
        public string estado { get; set; }
        public string dead_sec { get; set; }
        public string log_in { get; set; }
        public string log_out { get; set; }
        public string total { get; set; }
    }


    public class CallIntro_Gestion_Venta
    {

        [Key]
        public int id { get; set; }
        public DateTime? Fecha_Venta { get; set; }
        public string lead_id { get; set; }
        public string list_id { get; set; }
        public string campaign { get; set; }
        public string FonoVenta { get; set; }
        public string FonoCelular { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string agente { get; set; }
        public string Calidad_Evaluador { get; set; }
        public DateTime? Calidad_fecha { get; set; }
        public string Calidad_Estado { get; set; }
        public string recording { get; set; }
        public string Calidad_Motivo { get; set; }
        public string Calidad_Sub_Motivo { get; set; }
        public string Calidad_Observacion { get; set; }
        public string Codigo_Fono { get; set; }
        public int? orkesta_cargado { get; set; }
        public string depto { get; set; }
        public string Apelacion { get; set; }
        public DateTime? Apelacion_date { get; set; }
        public string Respuesta_Apelacion { get; set; }
        public DateTime? Respuesta_date { get; set; }
        public string Estado_Respuesta_Apelacion { get; set; }
        public DateTime? FechaVentaInformada { get; set; }
        public DateTime? Fecha_Regrabacion { get; set; }
        public int? lead_id_regrabacion { get; set; }
        public string Estado_Regrabacion { get; set; }
        public string recording_regrabacion { get; set; }
        public int? orkesta_regrabado_cargado { get; set; }
        public string list_id_regrabacion { get; set; }
        public string Acepta_Regrabacion { get; set; }
        public string Acepta_Motivo_Regrabacion { get; set; }
        public string campaign_Regrabacion { get; set; }
        public string agenda_observacion_Regrabacion { get; set; }
        public string agenda_fecha_Regrabacion { get; set; }
        public string calidad_regrabacion_estado { get; set; }
        public string calidad_regrabacion_estado_motivo { get; set; }
        public string calidad_regrabacion_estado_observacion { get; set; }
        public string calidad_regrabacion_fecha { get; set; }
        public string calidad_regrabacion_auditor { get; set; }
        public string Observacion_Segmentador { get; set; }
        public int? derivar_apelacion { get; set; }
        public string epoch { get; set; }
        public string epoch_regra { get; set; }
        public string tipo_carga { get; set; }
        public int? Estado_Calidad { get; set; }
        public int? pivot_record { get; set; }
        public string result_json { get; set; }
        public string campaign_flujo { get; set; }
        public string company_id { get; set; }
        public string Decil { get; set; }
        public string recording_len { get; set; }
        public string Calidad_Evaluador_Asignada { get; set; }
        public string Respuesta_Apelacion_observacion { get; set; }
        public string NRO_UNICO_CARGA { get; set; }
        public string FEC_VENTA { get; set; }
        public string TIPO_ASEG { get; set; }
        public string PARENTESCO_ID { get; set; }
        public string TIPO_DOC_ID { get; set; }
        public string NRO_DOC { get; set; }
        public string APE_PATERNO { get; set; }
        public string APE_MATERNO { get; set; }
        public string PRIMER_NOMBRE { get; set; }
        public string SEGUNDO_NOMBRE { get; set; }
        public string FEC_NACIMIENTO { get; set; }
        public string SEXO_ID { get; set; }
        public string ESTADO_CIVIL_ID { get; set; }
        public string TELEFONO_MOVIL { get; set; }
        public string PARTICIPACION { get; set; }
        public string PROFESION_ID { get; set; }
        public string ACTIVIDAD_ID { get; set; }
        public string CATEGORIA_ID { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }
        public string PRODUCTO_ID { get; set; }
        public string PRODUCTO { get; set; }
        public string PLAN_ID { get; set; }
        public string MONEDA_ID { get; set; }
        public string FRECUENCIA_PAGO_ID { get; set; }
        public string FORMA_PAGO_ID { get; set; }
        public string TIPO_TARJETA_ID { get; set; }
        public string NRO_TARJETA { get; set; }
        public string FEC_EXP_TARJETA { get; set; }
        public string NRO_CUOTAS { get; set; }
        public string COD_VENDEDOR { get; set; }
        public string SUCURSAL_ID { get; set; }
        public string CANAL_VENTA_ID { get; set; }
        public string DPS { get; set; }
        public string FLG_POLIZA_ELECTRONICA { get; set; }
        public string OBSERVACION { get; set; }
        public string Calidad_result_json { get; set; }
        public string Calidad_fecha_evaluacion { get; set; }
        public string codigo_interno { get; set; }
        public string NotaCalidad { get; set; }
    }

    public class CallIntro_Gestion_Dia
    {
        
        [Key]
            public int id { get; set; }
            public DateTime? fecha_gestion { get; set; }
            public string campaign { get; set; }
            public string list_id { get; set; }
            public string lead_id { get; set; }
            public string agente { get; set; }
            public string campaign_name { get; set; }
            public string campaign_id { get; set; }
            public string conecta { get; set; }
            public string no_conecta_detalle { get; set; }
            public string si_conecta { get; set; }
            public string si_conecta_interesa { get; set; }
            public string si_conecta_no_interesa { get; set; }
            public string no_conecta_fecha { get; set; }
            public string no_conecta_hora { get; set; }
            public string no_conecta_agenda { get; set; }
            public string result_json { get; set; }
       


    }

    public class Calidad_Admin_List_Total
    {
        [Key]
        public string lead_id { get; set; }
        public string rut { get; set; }
        public string nombres { get; set; }
        public string fechaventa { get; set; }
        public string recording_len { get; set; }
        public string fechainformada { get; set; }
        public string evaluador { get; set; }
        public string codigointerno { get; set; }




    }

    public class Reporte_Resultante_Calidad_Inchape
    {
        [Key]
        public string cliente { get; set; }
        public string documento { get; set; }
        public string asesor { get; set; }
        public string p_1 { get; set; }
        public string p_2 { get; set; }
        public string p_3 { get; set; }
        public string p_4 { get; set; }
        public string p_5 { get; set; }
        public string p_6 { get; set; }
        public string p_7 { get; set; }
        public string p_8 { get; set; }
        public string p_9 { get; set; }
        public string p_10 { get; set; }
        public string p_11 { get; set; }
        public string p_12 { get; set; }
        public string p_13 { get; set; }
        public string p_14 { get; set; }
        public string p_15 { get; set; }
        public string p_16 { get; set; }
        public string p_17 { get; set; }
        public string p_18 { get; set; }
        public string p_19 { get; set; }
        public string p_20 { get; set; }
        public string p_21 { get; set; }
        public string p_22 { get; set; }
        public string p_23 { get; set; }
        public string p_24 { get; set; }
        public string p_25 { get; set; }
        public string p_26 { get; set; }
        public string Nota { get; set; }
        public string fecha_evaluacion { get; set; }
        public string fecha_venta { get; set; }
        public string TMO { get; set; }
        public string nombre_cliente { get; set; }
        public string auditor { get; set; }
        public string FonoVenta { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public string calificacion_final { get; set; }
        public string cli_id { get; set; }
        public string nivel1 { get; set; }
        public string nivel2 { get; set; }
        public string nivel3 { get; set; }
        public string nivel4 { get; set; }
    }


    public class Trama_Resultante_Intento_Renuncia
    {

        [Key]
        public string fecha { get; set; }
        public string call_id { get; set; }
        public string user_neotel { get; set; }
        public string inicio_llamada { get; set; }
        public string hora_ringing { get; set; }
        public string hora_inicio_agente { get; set; }
        public string hora_fin_agente { get; set; }
        public string cli_id { get; set; }
        public string cli_fecha_tipificacion { get; set; }
        public string cli_hora_tipificacion { get; set; }
        public string nombre_plan { get; set; }
        public string cli_nombre_cliente { get; set; }
        public string tipo_doc { get; set; }
        public string nro_documento { get; set; }
        public string fecha_nacimiento { get; set; }
        public string sexo { get; set; }
        public string telefono_1 { get; set; }
        public string telefono_2 { get; set; }
        public string cli_aemail { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public string direccion { get; set; }
        public string nivel_1 { get; set; }
        public string nivel_2 { get; set; }
        public string nivel_3 { get; set; }
        public string nivel_4 { get; set; }
       
    }
    public class Calidad_Admin_List_Asignadas
    {
        [Key]
        public string lead_id { get; set; }
        public string rut { get; set; }
        public string nombres { get; set; }
        public string fechaventa { get; set; }
        public string recording_len { get; set; }
        public string fechainformada { get; set; }
        public string asesor { get; set; }
        public string campaign_flujo { get; set; }
        public string calidad_estado { get; set; }




    }



    public class Calidad_Admin_List_Apelados
    {
        [Key]
        public string lead_id { get; set; }
        public string rut { get; set; }
        public string nombres { get; set; }
        public string fechaventa { get; set; }
        public string recording_len { get; set; }
        public string fechainformada { get; set; }
        public string asesor { get; set; }
        public string campaign_flujo { get; set; }
        public string apelacion { get; set; }
        public string respuesta_apelacion { get; set; }
        public string obs_calidad { get; set; }
        public string fecha_calidad { get; set; }
        public string fecha_apelacion { get; set; }
        public string resp_apelacion { get; set; }


    }


    public class Calidad_Admin_List_Apeladas
    {
        [Key]
        public string campaignflujo { get; set; }
        public string companyname { get; set; }
        public string company { get; set; }
        public string codigo { get; set; }
        public int? total { get; set; }
        public int? informados { get; set; }
        public int? auditadas { get; set; }
        public int? asignadas { get; set; }
        public int? pendienteasignacion { get; set; }
        public int? apeladas { get; set; }




    }

    public class Cargador
    {
        [Key]
        public string flujo { get; set; }
        public string nombre { get; set; }

    }


    public class Carga_Resultado_Ripley
    {

        [Key]
       
        public string rut { get; set; }
        public string qintentos { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string callcenter { get; set; }
        public string estadofinal { get; set; }
        public string motivo { get; set; }
        public string bbdd { get; set; }
        public string rutagente { get; set; }
        public string nomagente { get; set; }
        public string categoria { get; set; }
        public string patente { get; set; }



    }

    public class Panel_Hopper
    {
        [Key]
        public string id { get; set; }
        
        public string hopper_id { get; set; }
        public string lead_id { get; set; }
        public string campaign_id { get; set; }
        public string estado { get; set; }
        public string agente { get; set; }
        public string vendor_lead_code { get; set; }
        public string campaign_name { get; set; }
        public string full_name { get; set; }
        public string tipo_registro { get; set; }
        public string llamado { get; set; }
        public string llamado_detalle { get; set; }
        public string interesa { get; set; }
        public string nointeresa { get; set; }
        public string nuevo { get; set; }
        public string conecta { get; set; }
        public string noconecta { get; set; }
        public string total { get; set; }
    }

    public class Usuarios
    {
        [Key]
        public string id { get; set; }
        public string nombre { get; set; }
        public string cargo { get; set; }
        public string usuario { get; set; }
        public string mail { get; set; }
        public string estado { get; set; }
        public string estadoDetalle { get; set; }
        public string cliente { get; set; }
        public string rol { get; set; }
        public string rolDetalle { get; set; }

    }

    public class TblOpcionLlamadum
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public int? Padre { get; set; }
        public string Agenda { get; set; }
    }
    public class Gestion_Carga
    {
        [Key]
        public string carga_nombre { get; set; }
        public string Cargado { get; set; }
        public string Acepta { get; set; }
        public string Contactado { get; set; }
        public string Tercero { get; set; }
        public string NoConecta { get; set; }
        public string Recorrido { get; set; }
        public string Porinteresa { get; set; }
        public string PorContactado { get; set; }
        public string Pendiente { get; set; }
    

    }

     
    public class Gestion_Ejecutivo
    {
        [Key]
        public string fecha { get; set; }
        public string Cargado { get; set; }
        public string Acepta { get; set; }
        public string Contactado { get; set; }
        public string Tercero { get; set; }
        public string NoConecta { get; set; }
        public string Recorrido { get; set; }
        public string Porinteresa { get; set; }
        public string PorContactado { get; set; }
        public string Pendiente { get; set; }
        public string TMO { get; set; }
        public string HH { get; set; }
        public string Rec_HH { get; set; }
    

    }

    public class Gestion_NC
    {
        [Key]
        //public string tipificacion { get; set; }
        public string sub_tipificacion { get; set; }
        public int? cantidad { get; set; }

    }

    public class Gestion_NC_Radar
    {
        [Key]
        public string sub_tipificacion { get; set; }
        public int? ao { get; set; }
        public int? mes { get; set; }
        public int? dia { get; set; }

    }

    public class Gestion_Dash_dia
    {
        [Key]
        public string intervalo { get; set; }        
        public string fecha { get; set; }
        public string hora { get; set; }
        public string Cargado { get; set; }
        public string Acepta { get; set; }
        public string Contactado { get; set; }
        public string Tercero { get; set; }
        public string NoConecta { get; set; }
        public string Recorrido { get; set; }
        public string Porinteresa { get; set; }
        public string PorContactado { get; set; }
        public string Pendiente { get; set; }
        public string TMO { get; set; }
        public string HH { get; set; }
        public string Rec_HH { get; set; }


    }

    public class Menu_Header
    {

        [Key]
        public string menu_header { get; set; }


    }

    public class Menu_Body
    {

        [Key]
        public string menu_body { get; set; }


    }

    public class Session
    {
        [Key]
        public string user { get; set; }
        public string gui { get; set; }

    }

    public class SaveUrl
    {

        [Key]
        public string respuesta { get; set; }

    }

    public class CRM_Carga_Resumen_Dash
    {

        [Key]
        public string id { get; set; }
        public string fecha_carga_base { get; set; }
        public string nombre_carga { get; set; }
        public string libera_agenda { get; set; }
        public string block_base { get; set; }
        public string Cantidad { get; set; }

    }

    public class Filtros
    {
        [Key]
        public string detalle { get; set; }

    }
    public class Flujo_ingreso_Filtros
    {
        [Key]
        public string dato { get; set; }
        public string dato_1 { get; set; }
        public string dato_2 { get; set; }
        public string dato_3 { get; set; }
        public string dato_4 { get; set; }
        public string dato_5 { get; set; }
        public string dato_6 { get; set; }
        public string dato_7 { get; set; }
        public string dato_8 { get; set; }
        public string dato_9 { get; set; }
        public string dato_10 { get; set; }
        public string dato_11 { get; set; }
        public string dato_12 { get; set; }
        public string dato_13 { get; set; }
        public string dato_14 { get; set; }
        public string dato_15 { get; set; }
        public string dato_16 { get; set; }
        public string dato_17 { get; set; }
        public string dato_18 { get; set; }
        public string dato_19 { get; set; }
        public string dato_20 { get; set; }
        public string dato_21 { get; set; }
        public string dato_22 { get; set; }
        public string dato_23 { get; set; }
        public string dato_24 { get; set; }
        public string dato_25 { get; set; }
        public string dato_26 { get; set; }
        public string dato_27 { get; set; }
        public string dato_28 { get; set; }
        public string dato_29 { get; set; }
        public string dato_30 { get; set; }
        public string dato_31 { get; set; }
        public string dato_32 { get; set; }
        public string dato_33 { get; set; }
        public string dato_34 { get; set; }
        public string dato_35 { get; set; }
        public string dato_36 { get; set; }
        public string dato_37 { get; set; }
        public string dato_38 { get; set; }
        public string dato_39 { get; set; }
        public string dato_40 { get; set; }
        public string dato_41 { get; set; }
       

    }


    public class Cargador_Validador
    {
        [Key]
        public string TipoBase { get; set; }
        public string Nombre_Carga_Full { get; set; }
        public string Validacion { get; set; }
        public string Cantidad { get; set; }

    }
    public class CRM_Carga_Resumen_DashAdmin
    {

        [Key]
        public string id { get; set; }
        public string fecha_carga_base { get; set; }
        public string nombre_carga { get; set; }
        public string libera_agenda { get; set; }
        public string block_base { get; set; }
        public string Cantidad { get; set; }
        public string list_id { get; set; }

    }


    public class PanelTematico
    {
        [Key]
        public string agentes { get; set; }
        public string clientes_recorridos { get; set; }
        public string primera_gestion { get; set; }
        public string regestión { get; set; }
        public string clientes_unicos { get; set; }
        public string marcaciones { get; set; }
        public string intensidad { get; set; }
        public string horas_logueo { get; set; }
        public string horas_habladas { get; set; }
        public string por_productividad { get; set; }
        public string tiempo_ocio { get; set; }
        public string tiempo_aux { get; set; }
        public string servicios_higienicos { get; set; }
        public string tiempo_administrativo { get; set; }
        public string apoyo_supervisión { get; set; }
        public string problemas_tecnicos { get; set; }
        public string descanso_medico { get; set; }
        public string almuerzo { get; set; }
        public string reunión_grupal { get; set; }
        public string feedback { get; set; }
        public string llenado_solicitud { get; set; }
        public string descanso { get; set; }
        public string contactados { get; set; }
        public string contactados_clientes { get; set; }
        public string por_contactabilidad { get; set; }
        public string por_contactabilidad_directa { get; set; }
        public string ventas_brutas { get; set; }
        public string venta_primera { get; set; }
        public string venta_regestion { get; set; }
        public string ventas_netas { get; set; }
        public string por_efectividad { get; set; }
        public string por_tasa_de_cierre { get; set; }
        public string n { get; set; }
        public string s { get; set; }
        public string ns { get; set; }
        public string nn { get; set; }
        public string fallas_tecnicas { get; set; }
        public string fallas_tecnicas_recuperadas { get; set; }
        public string fallas_tecnicas_no_recuperadas { get; set; }
    }






    public class Flujo_Respuesta
    {
        [Key]
        public string id { get; set; }
        public string detalle { get; set; }

    }

    public class Flujo_ingreso
    {
        [Key]
        public string dato { get; set; }
        public string dato_1 { get; set; }
        public string dato_2 { get; set; }
        public string dato_3 { get; set; }
        public string dato_4 { get; set; }
        public string dato_5 { get; set; }
        public string dato_6 { get; set; }
        public string dato_7 { get; set; }
        public string dato_8 { get; set; }
        public string dato_9 { get; set; }
    }
    public class Flujo_out_mes
    {
        [Key]
        public int id { get; set; }
        public string Detalle { get; set; }

    }

    public class Session_out
    {
        [Key]
        public int respuesta { get; set; }

    }
    public class Login
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class Login_out
    {
        [Key]
        public int cliente { get; set; }
        public string id_usuario { get; set; }
        public int id { get; set; }
        public string gui { get; set; }
        public string ruta { get; set; }

    }
    public class Login_out_result
    {
        [Key]
        public int cliente { get; set; }
        public string id_usuario { get; set; }
        public int id { get; set; }
        public string gui { get; set; }
        public string ruta { get; set; }
        public string token { get; set; }


    }

    public class Resultante_ABCDIN_2
    {
        [Key]
        public string rut { get; set; }
        public string campana { get; set; }
        public string producto        { get; set; }
        public string fono        { get; set; }
        public string fecha        { get; set; }
        public string cantidad        { get; set; }
        public string contacto        { get; set; }
        public string venta        { get; set; }



    }



    public class Flujo_Out
    {
        [Key]
        public string respuesta { get; set; }

    }
     public class Flujo_Out_respuesta
    {
        [Key]
        public string nroSimulacion { get; set; }
        public string status { get; set; }
        public string statusCode { get; set; }
        public string message { get; set; }

    }

    public class Flujo_in
    {
        [Key]
      //  public string ID { get; set; }
        public int Simulacion { get; set; }
        public int PasoWeb { get; set; }
        public string MarcaModeloAnioTipoVehiculo { get; set; }
        public int Rut { get; set; }
        public string DV { get; set; }
        public int DuenoVehiculo { get; set; }
        public int RutDueno { get; set; }
        public string DVDueno { get; set; }
        public int Deducible { get; set; }
        public int ResponsabilidadCivil { get; set; }
        public int AutoReemplazo { get; set; }
        public int VehiculoNuevo { get; set; }
        public string Patente { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Campana { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public int Prioridad { get; set; } 
        public string fechaCotizacion { get; set; }
     //   public string detalle { get; set; }


    }



    public class Flujo_in_paso_1
    {
        [Key]

        public string Simulacion { get; set; }
        public string PasoWeb { get; set; }
        public string MarcaModeloAnioTipoVehiculo { get; set; }
        public string Rut { get; set; }
        public string DV { get; set; }
        public string DuenoVehiculo { get; set; }
        public string RutDueno { get; set; }
        public string DVDueno { get; set; }      
        public string Campana { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
     


    }
    public class Flujo_in_paso_2
    {
        [Key]

        public string Simulacion { get; set; }
        public string PasoWeb { get; set; }
        public string Deducuble { get; set; }
        public string ResponsabilidadCivil { get; set; }
        public string AutoReemplazo { get; set; }
        


    }
    public class Flujo_in_paso_3
    {
        [Key]

        public string Simulacion { get; set; }
        public string PasoWeb { get; set; }
        public string VehiculoNuevo { get; set; }
        public string Patente { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string fechaCotizacion { get; set; }

    
    }
    public class Flujo_in_paso_4
    {
        [Key]

        public string Simulacion { get; set; }
        public string PasoWeb { get; set; }     
        public string Campana { get; set; }
     

    }
}
