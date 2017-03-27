using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class SonaTipoNomina
    {
        //variables
        public int iOpcion;
        public int iIdCompania;
        public int iIdTipoNomina;
        public string sDescripcion;
        public string sRespuesta;

        public SonaTipoNomina()
        {
            iOpcion = 0;
            iIdCompania = 0;
            iIdTipoNomina = 0;
            sDescripcion = "";
            sRespuesta = "";
        } // public SonaTipoNomina()

        // Metodo DataTable para regresar Tipos de Nomina
        public DataTable obtTipoNomina(int p_opcion, int p_IdCompania, int p_IdTipoNomina, string p_TextoBuscar)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonatiponomina_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_IdCompania", SqlDbType.Int).Value = p_IdCompania;
            cmd.Parameters.Add("@p_IdTipoNomina", SqlDbType.Int).Value = p_IdCompania;
            cmd.Parameters.Add("@p_Descripcion", SqlDbType.VarChar).Value = p_TextoBuscar;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.asignarConexion(cmd);

            DataTable dtCompania = new DataTable();
            Adapter.Fill(dtCompania);
            return dtCompania;

        } // public DataTable obtTipoNomina()
    } // class SonaTipoNomina
} // namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
