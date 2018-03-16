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

        //llena dgv,cb, permisos
        public DataTable dtpermisos(string sp_cvusuario, string sp_idtrab, string sp_nombre, string sp_passw, string sp_stusuario,
                                    string sp_usuumod, string sp_prgumod, int ip_opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceusuario_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = sp_cvusuario;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.VarChar).Value = sp_idtrab;
            cmd.Parameters.Add("@p_nombre", SqlDbType.VarChar).Value = sp_nombre;
            cmd.Parameters.Add("@p_passw", SqlDbType.VarChar).Value = sp_passw;
            cmd.Parameters.Add("@p_stusuario", SqlDbType.VarChar).Value = sp_stusuario;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = sp_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sp_prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = ip_opcion;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtperm = new DataTable();
            Adapter.Fill(dtperm);
            return dtperm;
        }

    }
}
