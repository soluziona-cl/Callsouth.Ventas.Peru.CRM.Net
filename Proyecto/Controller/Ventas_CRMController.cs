using CallSouth.Ventas.Peru.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
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


        //[HttpPost]
        //[Authorize(Roles = "CRM_Supervisor")]
        //[Route("CRM/Session_Out")]
        //public async Task<ActionResult<IEnumerable<Session_out>>> GetSession_out(Session session)
        //{
        //    string stored = "exec sp_SessionClear @user='" + session.user + "', @gui='" + session.gui + "';";
        //    return await _context.Session_out.FromSqlRaw(stored).ToListAsync();

        //}

        //[HttpPost]
        //[Authorize(Roles = "CRM_Supervisor")]
        //[Route("CRM/Session_Check")]
        //public async Task<ActionResult<IEnumerable<Session_out>>> GetSession_check(Session session)
        //{
        //    string stored = "exec sp_checkSeesion @user='" + session.user + "', @gui='" + session.gui + "';";
        //    return await _context.Session_out.FromSqlRaw(stored).ToListAsync();


        //}

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
        [Route("CRM/DetalleCargasAdmin/CargasDetalleResumenDash")]
        public async Task<ActionResult<IEnumerable<CRM_Carga_Resumen_DashAdmin>>> Get_CargasDetalleResumenDashAdmin(Flujo_ingreso flujo_In)
        {

            string stored = "exec sp_nombre_carga_resumen_new_admin  @carga= '" + flujo_In.dato + "'";

            return await _context.cRM_Carga_Resumen_DashAdmins.FromSqlRaw(stored).AsNoTracking().ToListAsync();

        }

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
        [Route("CRM/Grabaciones/Search")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_GrabacionesSearch(Flujo_ingreso flujo_In)
        {
            string stored = "exec sp_listar_grabaciones_flujo @flujo= '" + flujo_In.dato + "',@lead_id= '" + flujo_In.dato_2 + "',@rut= '" + flujo_In.dato_3
                + "',@ini='" + flujo_In.dato_4 + "',@fin= '" + flujo_In.dato_5 + "',@agente= '" + flujo_In.dato_6 + "',@company= '" + flujo_In.dato_7 + "'";


            return await _context.flujo_Respuestas.FromSqlRaw(stored).AsNoTracking().ToListAsync();


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
            string stored = "exec sp_dash_dia @flujo='" +flujo_Ingreso.dato+"'" ;

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

        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga")]
        public async Task<IActionResult> UploadFile(IFormFile file, string flujo)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            //Desarrollo

            string dirPruebas = @"E:\";

            var path = Path.Combine(dirPruebas, "Upload/CallSouth/Macal", file.GetFilename());
            var cargador = new Cargador();

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);

                cargador.flujo = file.GetFilename();
            }

            return Ok(cargador);
        }

        [SupportedOSPlatform("windows")]
        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga_Macal")]
        public IActionResult UploadFile_SMS(IFormFile postedFile, string flujo)
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

                try
                {
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

                        String sql = "SELECT count(*) Cantidad FROM dbo.Carga_Bases_Flujos where carga_nombre ='" + carga.ToString() + "'";

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
                                    sqlBulkCopy.DestinationTableName = "dbo.Carga_Flujos";

                                    //[OPTIONAL]: Map the Excel columns with that of the database table.
                                    sqlBulkCopy.ColumnMappings.Add("EMAIL", "Correo");
                                    sqlBulkCopy.ColumnMappings.Add("TELEFONO", "Telefono");
                                    sqlBulkCopy.ColumnMappings.Add("PATENTE", "Patente");
                                    sqlBulkCopy.ColumnMappings.Add("MARCA", "Marca");
                                    sqlBulkCopy.ColumnMappings.Add("MODELO", "Modelo");
                                    sqlBulkCopy.ColumnMappings.Add("VERSION", "Version");
                                    sqlBulkCopy.ColumnMappings.Add("AO", "Ao");
                                    sqlBulkCopy.ColumnMappings.Add("PRECIOEVALUACION", "Precio");
                                    sqlBulkCopy.ColumnMappings.Add("URL", "Solicitud");

                                    cargador.nombre = carga.ToString();
                                    //con.Open();
                                    sqlBulkCopy.WriteToServer(dt);

                                    //String sql_2 = "UPDATE dbo.Confirmacion_Carga_Datos set TipoBase='SMS' where Nombre_Carga_Full ='" + carga.ToString() + "'";
                                    //using (SqlCommand command_2 = new SqlCommand(sql_2, con))
                                    //{
                                    //    command_2.ExecuteNonQuery();
                                    //}

                                    SqlCommand cmd = new SqlCommand("sp_load_data", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@nombre", carga.ToString());
                                    cmd.ExecuteNonQuery(); 
                                    
                                    
                                    
                                    SqlCommand cmd_2 = new SqlCommand("sp_info_carga", con);
                                    cmd_2.CommandType = CommandType.StoredProcedure;
                                    cmd_2.Parameters.AddWithValue("@carga", carga.ToString());
                                    cmd_2.ExecuteNonQuery();

                                    con.Close();
                                }
                            }


                        }
                    }

                    return Ok(cargador);
                }
                catch (Exception)
                {
                    cargador.flujo = "Formato No Valido (Columnas no Validas)";
                    return Ok(cargador);

                }

                //Create a Folder.

            }
            return Ok(cargador);

        }


        [HttpPost]
        [Authorize(Roles = "CRM_Supervisor")]
        [Route("CRM/Carga/Procesar")]
        public async Task<ActionResult<IEnumerable<Flujo_Respuesta>>> Get_Cargador(Flujo_ingreso flujo_In)
        {

            string filepath = @"E:\Upload\CallSouth\Macal\";
            string filepathtarget = @"E:\Upload\CallSouth\Macal\";

            string TableName = @"Carga_Flujos";

            string stored = "exec sp_import_Data_New @filepath='" + filepath +
                "',@filepathtarget = '" + filepathtarget +
                "',@pattern = '" + flujo_In.dato +
                "',@nombre = '" + flujo_In.dato_2 +
                "',@TableName = '" + TableName + "'; ";


            return await _context.flujo_Respuestas.FromSqlRaw(stored).ToListAsync();


        }

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
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
