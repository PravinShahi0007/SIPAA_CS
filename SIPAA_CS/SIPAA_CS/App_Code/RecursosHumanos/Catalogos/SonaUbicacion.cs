using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//***********************************************************************************************
//Autor: Jose Luis Alvarez Delgado
//Fecha creación: 03-Abr-2017       Última Modificacion: 
//Descripción: Ubicaciones, no existia la clase, aunque llamaba un SP no se de donde.
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class SonaUbicacion
    {
        //variables
        public int iopcion = 0;
        public string srespuesta = "";
        public string stextoabuscar = "";

        public DataTable obtenerubicaciones(int iopcion, string stextoabuscar)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonaubicacion_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion oConexion = new Conexion();
            SqlConnection sqlcn = oConexion.conexionSonarh();
            
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = stextoabuscar;
            oConexion.asignarConexions(cmd);

            SqlDataAdapter daubicacion = new SqlDataAdapter(cmd);
            oConexion.cerrarConexions();

            DataTable dtubicacion = new DataTable();
            daubicacion.Fill(dtubicacion);
            return dtubicacion;
        }

    }
}
