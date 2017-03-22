using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: Jose Luis Alvarez Delgado
//Fecha creación: 21-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra Mensajes del Sistema
//***********************************************************************************************

namespace SIPAA_CS.App_Code
{
    class MensajesSonarh
    {
        //se declaran variables
        public int popcion= 0;
        public string prespuesta="";
        public string ptextoabuscar="";
        public int pidtrab = 0;
        public int pcvmensaje = 0;
        public string pdescripcion = "";
        public DateTime pfecinicio;
        public DateTime pfecfin;
        public string pusuumod="";
        public string pprgumodr="";

        //Metodo data table para el llenado del grid
        public DataTable ObtenerMensajes(int popcion, string ptextoabuscar, int pidtrab, int pcvmensaje, DateTime pfecinicio, DateTime pfecfin, string pusuumod, string pprgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_mensajes_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = ptextoabuscar;
            objConexion.asignarConexion(cmd);

            //Ejecutar el SP con el SqlAdapter
            SqlDataAdapter damensajes = new SqlDataAdapter(cmd);

            //Cerrar conexion
            objConexion.cerrarConexion();
            DataTable dtmensajes = new DataTable();
            damensajes.Fill(dtmensajes);
            return dtmensajes;
        }

        public int mensajesudi(int popcion, int pidtrab, int pcvmensaje, string pdescripcion, string pusuumod, string pprgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_mensajes_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = pidtrab;
            cmd.Parameters.Add("@p_cvmensaje", SqlDbType.Int).Value = pcvmensaje;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = pdescripcion;
            //cmd.Parameters.Add("@p_fecinicio", SqlDbType.Date).Value = pfecinicio;
            //cmd.Parameters.Add("@p_fecfin", SqlDbType.Date).Value = pfecfin;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = pusuumod;
            cmd.Parameters.Add("@p_prgumodr", SqlDbType.VarChar).Value = pprgumodr;

            objConexion.asignarConexion(cmd);
            int p_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();

            return (p_respuesta);
        }
    }    
}
