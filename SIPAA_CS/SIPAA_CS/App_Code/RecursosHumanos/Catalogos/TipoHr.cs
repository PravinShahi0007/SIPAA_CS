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
        public int iopcion, icvtipohr, irespuesta;
        public string sdescripcion, susuumod, sprgumodr;

        public TipoHr()
        {
            //inician variables
            iopcion = 0;
            icvtipohr = 0;
            sdescripcion = "";
            susuumod = "";
            sprgumodr = "";
            irespuesta = 0;
        }

        //metodo data table para llenar grid de busqueda
        public DataTable obttipohr(int iopcion, int icvtipohr, string sdescripcion, string susuumod, string sprgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtipohr_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.VarChar).Value = icvtipohr;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sdescripcion;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumodr;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dttipohr = new DataTable();
            Adapter.Fill(dttipohr);
            return dttipohr;
        }
        //metodo insertar, actualizar, eliminar registro
        public int uditipohr(int iopcion, int icvtipohr, string sdescripcion, string susuumod, string sprgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtipohr_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.VarChar).Value = icvtipohr;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sdescripcion;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumodr;
            objConexion.asignarConexion(cmd);

            int p_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return p_respuesta;

        }
        public int valtipohr(int iopcion, int icvtipohr, string sdescripcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtipohr_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.VarChar).Value = icvtipohr;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sdescripcion;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";
            objConexion.asignarConexion(cmd);

            int irespval = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return irespval;

        }

    }

}
