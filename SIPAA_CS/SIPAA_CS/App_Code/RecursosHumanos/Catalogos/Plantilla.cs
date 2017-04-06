using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class Plantilla
    {
        //declaracion de variables
        public int ipopcion = 0;
        public string sprespuesta = "";
        public int ipcvplantilla = 0;
        public string spdescripcion = "";
        public string spusuumod = "";
        public string spprgumod = "";

        //metodo tipo datatable para obtener las plantillas 
        public DataTable ObtenerPlantillas(int ipopcion, int ipcvplantilla, string spdescripcion, string spusuumod, string spprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechcplantilla_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion oConexion = new Conexion();
            //paso de parametros a la sentencia
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = ipopcion;
            cmd.Parameters.Add("@p_cvplantilla", SqlDbType.Int).Value = ipcvplantilla;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = spdescripcion;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = spusuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = spprgumod;
            //asigno la conexion
            oConexion.asignarConexion(cmd);
            //ejecutar el SP con el sqladapter
            SqlDataAdapter daplantillas = new SqlDataAdapter(cmd);
            //cerrar la conexion
            oConexion.cerrarConexion();
            //se crea el data table
            DataTable dtplantillas = new DataTable();
            daplantillas.Fill(dtplantillas);
            return dtplantillas;
        }

        //metodo actualizar eliminar insertar registro
        public int fuid_sp_plantillas(int ipopcion, int ipcvplantilla, string spdescripcion, string spusuumod, string spprgumod)
        {
            int ip_respuesta = 0;
            int ip_respuestavalidacion = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechcplantilla_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion oConexion = new Conexion();
            //asigno la conexion
            oConexion.asignarConexion(cmd);
            if (ipopcion==1)
            {
                //paso de parametros a la sentencia
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = ipopcion;
                cmd.Parameters.Add("@p_cvplantilla", SqlDbType.Int).Value = ipcvplantilla;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = spdescripcion;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = spusuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = spprgumod;
                oConexion.asignarConexion(cmd);
                ip_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
                if (ip_respuesta > 0)
                {
                    ip_respuestavalidacion = 99; // Intento de duplicar un registro                    
                }
            }

            if (ip_respuesta==0)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = ipopcion;
                cmd.Parameters.Add("@p_cvplantilla", SqlDbType.Int).Value = ipcvplantilla;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = spdescripcion;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = spusuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = spprgumod;
                oConexion.asignarConexion(cmd);
                ip_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
                oConexion.cerrarConexion();
            }
            if (ip_respuestavalidacion == 99)
            {
                ip_respuesta = ip_respuestavalidacion;
            }
            oConexion.cerrarConexion();
            return (ip_respuesta);
        }
    }
}
