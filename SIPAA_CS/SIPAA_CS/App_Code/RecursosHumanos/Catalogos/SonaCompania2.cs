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
        public int iOpcion;
        public string sDescripcion;
        public string sRfc;
        public string sUsuumod;
        public string sPrgumodr;
        public int iRespuesta;

        public SonaCompania2()
        {
            //inician variables
            iOpcion = 0;
            sDescripcion = "";
            sRfc = "";
            sUsuumod = "";
            sPrgumodr = "";
            iRespuesta = 0;
        } // public SonaCompania2()

        // Metodo DataTable para regresar Compañias
        public DataTable obtCompania2(int iOpcion, string sTextoBuscar)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonacompania_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sTextoBuscar;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.asignarConexion(cmd);

            DataTable dtCompania = new DataTable();
            Adapter.Fill(dtCompania);
            return dtCompania;

        } // public DataTable obtCompania2(int p_opcion, string p_TextoBuscar)
    } // class SonaCompania2
} // namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
