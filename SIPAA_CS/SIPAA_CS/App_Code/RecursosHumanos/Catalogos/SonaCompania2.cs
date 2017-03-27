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
    class SonaCompania2
    {
        //variables
        public int p_opcion;
        public int p_idcompania;
        public string p_descripcion;
        public string p_rfc;
        public string p_usuumod;
        public string p_prgumodr;
        public int p_respuesta;

        public SonaCompania2()
        {
            //inician variables
            p_opcion = 0;
            p_idcompania = 0;
            p_descripcion = "";
            p_rfc = "";
            p_usuumod = "";
            p_prgumodr = "";
            p_respuesta = 0;
        } // public SonaCompania2()

        // Metodo DataTable para regresar Compañias
        public DataTable obtCompania2(int p_opcion, int p_IdCompania, string p_TextoBuscar)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonacompania_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_IdCompania", SqlDbType.Int).Value = p_IdCompania;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_TextoBuscar;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.asignarConexion(cmd);

            DataTable dtCompania = new DataTable();
            Adapter.Fill(dtCompania);
            return dtCompania;

        } // public DataTable obtCompania2(int p_opcion, string p_TextoBuscar)
    } // class SonaCompania2
} // namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
