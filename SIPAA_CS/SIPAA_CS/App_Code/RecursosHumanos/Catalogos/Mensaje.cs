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
    class Mensaje
    {
        //se declaran variables
        public int popcion= 0;
        public string prespuesta="";
        public int pidtrab = 0;
        public int pcvmensaje = 0;
        public string pdescripcion = "";
        public string pfecinicio = "";
        public string pfecfin = "";
        public string pusuumod="";
        public string pprgumod="";

        //Metodo data table para el llenado del grid
        public DataTable ObtenerMensajes(int popcion, int pidtrab, int pcvmensaje, string pdescripcion, string pfecinicio, string pfecfin, string pusuumod, string pprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_mensajes_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = pidtrab;
            cmd.Parameters.Add("@p_cvmensaje", SqlDbType.Int).Value = pcvmensaje;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = pdescripcion;
            cmd.Parameters.Add("@p_fecinicio", SqlDbType.VarChar).Value = pfecinicio;
            cmd.Parameters.Add("@p_fecfin", SqlDbType.VarChar).Value = pfecfin;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = pusuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = pprgumod;
            objConexion.asignarConexion(cmd);

            //Ejecutar el SP con el SqlAdapter
            SqlDataAdapter damensajes = new SqlDataAdapter(cmd);

            //Cerrar conexion
            objConexion.cerrarConexion();
            DataTable dtmensajes = new DataTable();
            damensajes.Fill(dtmensajes);
            return dtmensajes;
        }

        public int fudimensajes(int popcion, int pidtrab, int pcvmensaje, string pdescripcion, string pfecinicio, string pfecfin, string pusuumod, string pprgumod)
        {
            int p_respuesta = 0;
            int p_respuestaValidacion = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_mensajes_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            if (popcion == 1)
            {
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
                cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = pidtrab;
                cmd.Parameters.Add("@p_cvmensaje", SqlDbType.Int).Value = pcvmensaje;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = pdescripcion;
                cmd.Parameters.Add("@p_fecinicio", SqlDbType.VarChar).Value = pfecinicio;
                cmd.Parameters.Add("@p_fecfin", SqlDbType.VarChar).Value = pfecfin;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = pusuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = pprgumod;
                objConexion.asignarConexion(cmd);
                p_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
                if (p_respuesta > 0)
                {
                    p_respuestaValidacion = 99; // Intento de duplicar un registro
                }
            }

            if (p_respuesta == 0)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
                cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = pidtrab;
                cmd.Parameters.Add("@p_cvmensaje", SqlDbType.Int).Value = pcvmensaje;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = pdescripcion;
                cmd.Parameters.Add("@p_fecinicio", SqlDbType.VarChar).Value = pfecinicio;
                cmd.Parameters.Add("@p_fecfin", SqlDbType.VarChar).Value = pfecfin;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = pusuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = pprgumod;
                objConexion.asignarConexion(cmd);
                //
                p_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
                //
                objConexion.cerrarConexion();
            }
            if (p_respuestaValidacion == 99)
            {
                p_respuesta = p_respuestaValidacion;
            }
            //
            objConexion.cerrarConexion();
            return (p_respuesta);
        }
    }
}
