using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CallSouth.Ventas.Peru.Models
{

    public class Cargador
    {
        [Key]
        public string flujo { get; set; }
        public string nombre { get; set; }

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
