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
    class UbicacionUsuario
    {

        public UbicacionUsuario()
        {

        }
        //VALIDA PERMISO HUBICACION DE UN USUARIO
        public List<int> ObtenerUbicacionesxUsuario(string cvusuario, int ubicacion, string usuumod, string prgumod, int opcion)
        {

            List<int> ltUbicacionesxUsuario = new List<int>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusuubi_sui";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_idubicacion", SqlDbType.Int).Value = ubicacion;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                int idubicacion = reader.GetInt32(reader.GetOrdinal("idubicacion"));

                ltUbicacionesxUsuario.Add(idubicacion);

                //MUESTRA LAS UBICACIONES ASIGNADAS A UN USUARIO
                Console.WriteLine(idubicacion);


            }

            objConexion.cerrarConexion();

            return ltUbicacionesxUsuario;

        }
    }

    

}
