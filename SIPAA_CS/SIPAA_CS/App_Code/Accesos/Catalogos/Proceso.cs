using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.App_Code
{
    class Proceso
    {
        
        public Proceso()
        {
        }

        public DataTable ObtenerProceso(int cvproceso, string descripcion, int stproceso, string usuumod, string prgumod, int opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceproceso_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_cvproceso",SqlDbType.Int).Value = cvproceso;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = descripcion;
            cmd.Parameters.Add("@p_stproceso", SqlDbType.Int).Value = stproceso;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.VarChar).Value = opcion;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtProceso = new DataTable();
            Adapter.Fill(dtProceso);
            return dtProceso;
        }

        public int AgregarProceso(int cvproceso, string descripcion, int stproceso, string usuumod, string prgumod, int opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_acceproceso_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvproceso", SqlDbType.Int).Value = cvproceso;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = descripcion;
            cmd.Parameters.Add("@p_stproceso", SqlDbType.Int).Value = stproceso;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;
            

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            int regreso = Convert.ToInt32(cmd.ExecuteScalar());

            objConexion.cerrarConexion();

            return regreso;
        }

        public List<string> obtenerUsuariosxProceso(string cvusuario)
        {

            List<string> ltUsuariosxProceso = new List<string>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_formasignarproceso";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@cvusuario", SqlDbType.VarChar).Value = cvusuario;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                string usuarioproceso = Convert.ToString(reader.GetInt32(reader.GetOrdinal("cvproceso")));
                ltUsuariosxProceso.Add(usuarioproceso);
            }

            objConexion.cerrarConexion();

            return ltUsuariosxProceso;
        }

        public void AsignarUsuarioProceso(string cvusuario, int cvproceso,string passw, string usuumod, string prgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_asignarproceso";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@cvproceso", SqlDbType.Int).Value = cvproceso;
            cmd.Parameters.Add("@passw", SqlDbType.VarChar).Value = passw;
            cmd.Parameters.Add("@usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@prgumod", SqlDbType.VarChar).Value = prgumod;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.ExecuteNonQuery();

            objConexion.cerrarConexion();
        }

        public DataTable ReporteProcesos(int cvproceso, string descripcion, int stproceso, string usuumod, string prgumod, int opcion)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceproceso_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvproceso", SqlDbType.Int).Value = cvproceso;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = descripcion;
            cmd.Parameters.Add("@p_stproceso", SqlDbType.Int).Value = stproceso;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtProcesos = new DataTable();
            Adapter.Fill(dtProcesos);

            return dtProcesos;
        }

        public DataTable ReporteProcesosUsuarios(string cvusuario, int cvproceso, string passw, string usuumod, string prgumod, int opcion)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceusupro_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_cvproceso", SqlDbType.Int).Value = cvproceso;
            cmd.Parameters.Add("@p_passw", SqlDbType.VarChar).Value = passw;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtProcesos = new DataTable();
            Adapter.Fill(dtProcesos);

            return dtProcesos;
        }
    }


}
