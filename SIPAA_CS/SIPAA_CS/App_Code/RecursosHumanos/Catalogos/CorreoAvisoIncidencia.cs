using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class CorreoAvisoIncidencia
    {

        public CorreoAvisoIncidencia()
        {

        }

        //llena dgv,cb
        public DataTable dtdgvcbcorreo(int iopcion, int icvcorreo, string snombre, int itipo, int iformapago, string scorreo, string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechcorreoapn_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvcorreo", SqlDbType.Int).Value = icvcorreo;
            cmd.Parameters.Add("@p_nombre", SqlDbType.VarChar).Value = snombre;
            cmd.Parameters.Add("@p_tipo", SqlDbType.Int).Value = itipo;
            cmd.Parameters.Add("@p_formapago", SqlDbType.Int).Value = iformapago;
            cmd.Parameters.Add("@p_correo", SqlDbType.VarChar).Value = scorreo;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtdgvcboscorreo = new DataTable();
            Adapter.Fill(dtdgvcboscorreo);
            return dtdgvcboscorreo;
        }

        //crud correos
        public int crudcorreo(int iopcion, int icvcorreo, string snombre, int itipo, int iformapago, string scorreo, string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechcorreoapn_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvcorreo", SqlDbType.Int).Value = icvcorreo;
            cmd.Parameters.Add("@p_nombre", SqlDbType.VarChar).Value = snombre;
            cmd.Parameters.Add("@p_tipo", SqlDbType.Int).Value = itipo;
            cmd.Parameters.Add("@p_formapago", SqlDbType.Int).Value = iformapago;
            cmd.Parameters.Add("@p_correo", SqlDbType.VarChar).Value = scorreo;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);

            int iverifact = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifact;
        }




    }
}
