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
    class JustificaIncidenciad
    {

        public JustificaIncidenciad()
        {

        }

        //llena dgv,cb
        public DataTable dtdatos(int iopcion, int icvjustinc, int icvincidencia, int icvjustab, string sdescjustab, int istjustab, string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechjustinc_d_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvjustinc", SqlDbType.Int).Value = icvjustinc;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = icvincidencia;
            cmd.Parameters.Add("@p_cvjustab", SqlDbType.Int).Value = icvjustab;
            cmd.Parameters.Add("@p_descjustab", SqlDbType.VarChar).Value = sdescjustab;
            cmd.Parameters.Add("@p_stjustab", SqlDbType.Int).Value = istjustab;
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
        public int cruddatos(int iopcion, int icvjustinc, int icvincidencia, int icvjustab, string sdescjustab, int istjustab, string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechjustinc_d_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvjustinc", SqlDbType.Int).Value = icvjustinc;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = icvincidencia;
            cmd.Parameters.Add("@p_cvjustab", SqlDbType.Int).Value = icvjustab;
            cmd.Parameters.Add("@p_descjustab", SqlDbType.VarChar).Value = sdescjustab;
            cmd.Parameters.Add("@p_stjustab", SqlDbType.Int).Value = istjustab;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);

            int iverifact = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifact;
        }
    }
}
