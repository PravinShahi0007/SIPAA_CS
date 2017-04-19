using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class SonaUbicacion
    {
        public DataTable obtenerSonaUbicacion(string ubicacion, int opcion)
        {
            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonaubicacion_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_ubicacion", SqlDbType.VarChar).Value = ubicacion;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;
            
            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtUbicacion = new DataTable();
            Adapter.Fill(dtUbicacion);

            return dtUbicacion;
        }
    }
}
