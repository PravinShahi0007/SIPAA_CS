using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
{
    class TrabajadorPerfil
    {

        public TrabajadorPerfil()
        {

        }
        //llena dgv,cb
        public DataTable dtdgvcb(string sidtrab, int iopcion, string scheca, string sactivo, int icvtipohr,
                                string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtrabperfil_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sidtrab;
            cmd.Parameters.Add("@P_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@P_checa", SqlDbType.VarChar).Value = scheca;
            cmd.Parameters.Add("@P_activo", SqlDbType.VarChar).Value = sactivo;
            cmd.Parameters.Add("@P_cvtipohr", SqlDbType.Int).Value = icvtipohr;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtdgvcbos = new DataTable();
            Adapter.Fill(dtdgvcbos);
            return dtdgvcbos;
        }

        //vuid inicio trab perfil
        public int vuidtrabperf(string sidtrab, int iopcion, string scheca, string sactivo, int icvtipohr,
                                string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtrabperfil_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sidtrab;
            cmd.Parameters.Add("@P_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@P_checa", SqlDbType.VarChar).Value = scheca;
            cmd.Parameters.Add("@P_activo", SqlDbType.VarChar).Value = sactivo;
            cmd.Parameters.Add("@P_cvtipohr", SqlDbType.Int).Value = icvtipohr;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);

            int iverifact = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifact;
        }

    }
}
