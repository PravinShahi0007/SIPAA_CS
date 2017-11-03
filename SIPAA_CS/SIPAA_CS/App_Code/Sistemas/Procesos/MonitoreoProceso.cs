using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:27/10/2017     Última Modificacion: dd-mm-aaaa
//Descripción: monitores trabajos sql
//***********************************************************************************************


namespace SIPAA_CS.App_Code.Sistemas.Procesos
{
    class MonitoreoProceso
    {
        public MonitoreoProceso()
        {
        }
        //llena dgv,cb
        public DataTable dtdgvcb(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_adm_monitoresuj";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtdgvcbos = new DataTable();
            Adapter.Fill(dtdgvcbos);
            return dtdgvcbos;
        }

        //
        public int iverifproc(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_adm_monitoresuj";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            objConexion.asignarConexion(cmd);

            int iverif = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverif;
        }

    }
}
