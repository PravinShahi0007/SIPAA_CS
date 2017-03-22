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
    class CompaniaUsuario
    {
        public CompaniaUsuario()
        {

        }

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
            }

            objConexion.cerrarConexion();

            return ltCompaniasxUsuario;

        }



    }
}
