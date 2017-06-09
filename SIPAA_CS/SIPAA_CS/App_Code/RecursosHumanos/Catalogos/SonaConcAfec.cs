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
    class SonaConcAfec
    {
        //variables
        public int iOpcion;
        public int iIdAfecta;
        public string sDescripcion;

        public SonaConcAfec()
        {
            //inician variables
            iOpcion = 0;
            iIdAfecta = 0;
            sDescripcion = "";
        } // public SonaConcAfec()

        public DataTable obtConcAfec(int iOpcion, int iIdAfecta, string sDescripcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_sonaconcafec_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@p_IdAfecta", SqlDbType.Int).Value = iIdAfecta;
            cmd.Parameters.Add("@p_Descripcion", SqlDbType.VarChar).Value = sDescripcion;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtConcAfec = new DataTable();
            Adapter.Fill(dtConcAfec);
            return dtConcAfec;
        } // public DataTable obtConcAfec(int iOpcion, int iIdAfecta, string sDescripcion)
    } // class SonaConcAfec
} // namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
