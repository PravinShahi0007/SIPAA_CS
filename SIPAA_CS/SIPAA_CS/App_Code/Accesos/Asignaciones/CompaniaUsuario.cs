using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPAA_CS.App_Code;

namespace SIPAA_CS.App_Code
{
    class CompaniaUsuario
    {
        public CompaniaUsuario()
        {

        }
        //VALIDACION PERMISO COMPAÑIA
        public List<int> ObtenerCompaniasxUsuario(string cvusuario)
        {

            List<int> ltCompaniasxUsuario = new List<int>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusucom_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@cvusuario", SqlDbType.VarChar).Value = cvusuario;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                int idcompania = reader.GetInt32(reader.GetOrdinal("idcompania"));

                ltCompaniasxUsuario.Add(idcompania);
                //MUESTRA LAS COMPAÑIAS ASIGNADAS A UN USUARIO
                Console.WriteLine(idcompania);
            }

            objConexion.cerrarConexion();

            return ltCompaniasxUsuario;

        }

        public DataTable ReporteCompaniasUsuarios(string cvusuario, string idcompania,  string usuumod, string prgumod, int opcion)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_accetusucom_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_idcompania", SqlDbType.VarChar).Value = idcompania;
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
