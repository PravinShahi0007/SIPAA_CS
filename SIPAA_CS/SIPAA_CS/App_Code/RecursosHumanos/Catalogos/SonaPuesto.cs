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
//Fecha creación: 13-mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Muestra y busca puestos sonarh
//***********************************************************************************************

namespace SIPAA_CS.App_Code
{
    class SonaPuesto
    {

        //se declaran variables
        public int iopcion;
        public string srespuesta;
        public string stextobuscar;

        public SonaPuesto()
        {
            iopcion = 0;
            srespuesta = "";
            stextobuscar = "";
        }

        //data table 
        public DataTable obtptos(int iopcion, string stextobuscar)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonaPuesto_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = stextobuscar;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.asignarConexion(cmd);

            DataTable dtptos = new DataTable();
            Adapter.Fill(dtptos);
            return dtptos;
        }
    }
}