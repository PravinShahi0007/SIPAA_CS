using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.App_Code.Accesos.Asignaciones
{
    class DepartamentoUsuario
    {

        public DepartamentoUsuario()
        {

        }
        //VALIDACION PERMISO COMPAÑIA
        public List<string> ObtenerDepartamentosxUsuario(string cvusuario, string iddepartamento, string usuumod, string prgumod, int opcion)
        {

            List<string> ltDepartamentosxUsuario = new List<string>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusudep_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_iddepartamento", SqlDbType.VarChar).Value = iddepartamento;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                string iddep = reader.GetString(reader.GetOrdinal("iddepto"));

                ltDepartamentosxUsuario.Add(iddep);
                //MUESTRA LOS DEPARTAMENTOS ASIGNADOS A UN USUARIO
                Console.WriteLine(iddep);
            }

            objConexion.cerrarConexion();

            return ltDepartamentosxUsuario;

        }
        public DataTable ReporteDepartamentosUsuarios(string cvusuario, string iddepartmento, string usuumod, string prgumod, int opcion)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_accetusudep_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_iddepartamento", SqlDbType.VarChar).Value = iddepartmento;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtCompanias = new DataTable();
            Adapter.Fill(dtCompanias);

            return dtCompanias;
        }
    }
}
