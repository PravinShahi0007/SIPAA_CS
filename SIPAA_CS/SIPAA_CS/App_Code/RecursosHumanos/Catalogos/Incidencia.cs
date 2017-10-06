using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

namespace SIPAA_CS.App_Code
{
    class Incidencia
    {
        //SE DECLARAN VARIABLES
        public int CVIncidencia = 0;
        public string Descripcion = "";
        public int CVRepresenta = 0;
        public string Representa = "";
        public string UsuuMod = "";
        public string Estatus = "";
        public DateTime FhuMod  = DateTime.Today;
        public string PrguMod = "";
        public int CVTipo =0;
        public string TipoIncidencia = "";


        public DataTable ObtenerRepresentaxIncidencia(Incidencia objIncidencia,int Opcion) {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincrep_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_incidencia", SqlDbType.VarChar).Value = objIncidencia.Descripcion;
            cmd.Parameters.Add("P_representa", SqlDbType.VarChar).Value = objIncidencia.Representa;
            cmd.Parameters.Add("cvrepresenta", SqlDbType.Int).Value = objIncidencia.CVRepresenta;
            cmd.Parameters.Add("Opcion", SqlDbType.VarChar).Value = Opcion;
            cmd.Parameters.Add("usuumod", SqlDbType.VarChar).Value = objIncidencia.UsuuMod;
            cmd.Parameters.Add("prgumod", SqlDbType.VarChar).Value = objIncidencia.PrguMod;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);

            return dtIncidencia;
        }


        public DataTable ObtenerIncidenciaxTipo(Incidencia objIncidencia, int Opcion)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechinctipo_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.Add("P_cvincidencia", SqlDbType.Int).Value = "32";
            cmd.Parameters.Add("P_cvincidencia", SqlDbType.Int).Value = objIncidencia.CVIncidencia;
            cmd.Parameters.Add("P_incidencia", SqlDbType.VarChar).Value = objIncidencia.Descripcion;
            cmd.Parameters.Add("P_cvtipo", SqlDbType.Int).Value = objIncidencia.CVTipo;
            cmd.Parameters.Add("P_tipo", SqlDbType.VarChar).Value = objIncidencia.TipoIncidencia;
            cmd.Parameters.Add("P_estatus", SqlDbType.VarChar).Value = objIncidencia.Estatus;
            cmd.Parameters.Add("P_Opcion", SqlDbType.Int).Value = Opcion;
            cmd.Parameters.Add("usuumod", SqlDbType.VarChar).Value = objIncidencia.UsuuMod;
            cmd.Parameters.Add("prgumod", SqlDbType.VarChar).Value = objIncidencia.PrguMod;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);

            return dtIncidencia;
        }


        public DataTable ReporteRegistroGeneradoDetalle(string sIdTrab,DateTime dtFechaInicio
                                                        ,DateTime dtFechaFin,string sUbicacion
                                                        ,string sCompania)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechregistrogeneradodetalle_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_idtrab", SqlDbType.VarChar).Value = sIdTrab;
            cmd.Parameters.Add("P_fechainicio", SqlDbType.DateTime).Value = dtFechaInicio;
             cmd.Parameters.Add("P_fechafin", SqlDbType.DateTime).Value = dtFechaFin;
             cmd.Parameters.Add("P_Ubicacion", SqlDbType.VarChar).Value = sUbicacion ;
             cmd.Parameters.Add("P_Compania", SqlDbType.VarChar).Value = sCompania;


            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);

            return dtIncidencia;
        }


        public DataTable ReporteResumen(string sIdTrab, DateTime dtFechaInicio
                                                     , DateTime dtFechaFin, string sDepto, string sCompania,string sTNom
                                                     , string sUbicacion,string sArea)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechresumen_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_idtrab", SqlDbType.VarChar).Value = sIdTrab;
            cmd.Parameters.Add("P_fechainicio", SqlDbType.DateTime).Value = dtFechaInicio;
            cmd.Parameters.Add("P_fechafin", SqlDbType.DateTime).Value = dtFechaFin;
            cmd.Parameters.Add("P_Depto", SqlDbType.VarChar).Value = sDepto;
            cmd.Parameters.Add("P_cia", SqlDbType.VarChar).Value = sCompania;
            cmd.Parameters.Add("P_tnom", SqlDbType.VarChar).Value = sTNom;
            cmd.Parameters.Add("P_Ubicacion", SqlDbType.VarChar).Value = sUbicacion;
            cmd.Parameters.Add("P_plantel", SqlDbType.VarChar).Value = sArea;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);

            return dtIncidencia;
        }


        public DataTable ReporteObservaciones(string sIdTrab, DateTime dtFechaInicio
                                                   , DateTime dtFechaFin, string sDepto, string sCompania, string sTNom
                                                   , string sUbicacion, string sArea,string sIncidencia)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechobservaciones_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_idtrab", SqlDbType.VarChar).Value = sIdTrab;
            cmd.Parameters.Add("P_fechainicio", SqlDbType.DateTime).Value = dtFechaInicio;
            cmd.Parameters.Add("P_fechafin", SqlDbType.DateTime).Value = dtFechaFin;
            cmd.Parameters.Add("P_Depto", SqlDbType.VarChar).Value = sDepto;
            cmd.Parameters.Add("P_cia", SqlDbType.VarChar).Value = sCompania;
            cmd.Parameters.Add("P_tnom", SqlDbType.VarChar).Value = sTNom;
            cmd.Parameters.Add("P_Ubicacion", SqlDbType.VarChar).Value = sUbicacion;
            cmd.Parameters.Add("P_plantel", SqlDbType.VarChar).Value = sArea;
            cmd.Parameters.Add("P_Incidencia", SqlDbType.VarChar).Value = sIncidencia;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);

            return dtIncidencia;
        }


        public DataTable ReporteIncidenciasPasadasNomina(string sIdTrab, DateTime dtFechaInicio
                                                 , DateTime dtFechaFin,string sCompania, string sTNom
                                                 ,string sUbicacion)
        {
            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincidenciaspasadasnomina_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_idtrab", SqlDbType.VarChar).Value = sIdTrab;
            cmd.Parameters.Add("P_fechainicio", SqlDbType.DateTime).Value = dtFechaInicio;
            cmd.Parameters.Add("P_fechafin", SqlDbType.DateTime).Value = dtFechaFin;
            cmd.Parameters.Add("P_cia", SqlDbType.VarChar).Value = sCompania;
            cmd.Parameters.Add("P_tnom", SqlDbType.VarChar).Value = sTNom;
            cmd.Parameters.Add("P_Ubicacion", SqlDbType.VarChar).Value = sUbicacion;


            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);

            return dtIncidencia;
        }

        public DataTable ReporteIncidenciasPendientesAutorizar(string sIdTrab, DateTime dtFechaInicio, DateTime dtFechaFin, string sCompania, string sTNom, string sUbicacion)
        {
            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincidenciaspendientes_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_idtrab", SqlDbType.VarChar).Value = sIdTrab;
            cmd.Parameters.Add("P_fechainicio", SqlDbType.DateTime).Value = dtFechaInicio;
            cmd.Parameters.Add("P_fechafin", SqlDbType.DateTime).Value = dtFechaFin;
            cmd.Parameters.Add("P_cia", SqlDbType.VarChar).Value = sCompania;
            cmd.Parameters.Add("P_tnom", SqlDbType.VarChar).Value = sTNom;
            cmd.Parameters.Add("P_Ubicacion", SqlDbType.VarChar).Value = sUbicacion;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);
            return dtIncidencia;
        }

        public DataTable ReporteFechasHorasRegistro(string sIdTrab, DateTime dtFechaInicio, DateTime dtFechaFin, string sCompania, string sTNom, string sUbicacion)
        {//hay q modificarlo
            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechfechashorasregistro_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_idtrab", SqlDbType.VarChar).Value = sIdTrab;
            cmd.Parameters.Add("P_fechainicio", SqlDbType.DateTime).Value = dtFechaInicio;
            cmd.Parameters.Add("P_fechafin", SqlDbType.DateTime).Value = dtFechaFin;
            cmd.Parameters.Add("P_cia", SqlDbType.VarChar).Value = sCompania;
            cmd.Parameters.Add("P_tnom", SqlDbType.VarChar).Value = sTNom;
            cmd.Parameters.Add("P_Ubicacion", SqlDbType.VarChar).Value = sUbicacion;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);
            return dtIncidencia;
        }
    }
}
