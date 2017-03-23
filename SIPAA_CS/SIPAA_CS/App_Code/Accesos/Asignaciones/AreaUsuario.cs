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
    class AreaUsuario
    {
        public AreaUsuario()
        {

        }

        public List<int> ObtenerAreaxUsuario(string cvusuario,int idcompania)
        {

            List<int> ltAreasxUsuario = new List<int>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusuare_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@idcompania", SqlDbType.Int).Value = idcompania;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                int idarea = reader.GetInt32(reader.GetOrdinal("idplanta"));

                ltAreasxUsuario.Add(idarea);
            }

            objConexion.cerrarConexion();

            return ltAreasxUsuario;

        }
    }
}
