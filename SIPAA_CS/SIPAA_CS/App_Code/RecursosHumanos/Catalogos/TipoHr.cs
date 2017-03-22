using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación: 14-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra tipos de horario
//***********************************************************************************************

namespace SIPAA_CS.App_Code
{
    class TipoHr
    {
        //variables
        public int p_opcion;
        public int p_cvtipohr;
        public string p_descripcion;
        public string p_usuumod;
        public string p_prgumodr;
        public int p_respuesta;

        public TipoHr()
        {
        //inician variables
        p_opcion = 0;
        p_cvtipohr = 0;
        p_descripcion = "";
        p_usuumod = "";
        p_prgumodr = "";
        p_respuesta = 0;
        }

        //metodo data table para llenar grid de busqueda
        public DataTable obttipohr(int p_opcion, int p_cvtipohr, string p_descripcion, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechctipohr_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.VarChar).Value = p_cvtipohr;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dttipohr = new DataTable();
            Adapter.Fill(dttipohr);
            return dttipohr;
        }
        //metodo insertar, actualizar, eliminar registro
        public int uditipohr(int p_opcion, int p_cvtipohr, string p_descripcion, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechctipohr_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.VarChar).Value = p_cvtipohr;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;
            objConexion.asignarConexion(cmd);

            int p_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return p_respuesta;

        }

    }

}
