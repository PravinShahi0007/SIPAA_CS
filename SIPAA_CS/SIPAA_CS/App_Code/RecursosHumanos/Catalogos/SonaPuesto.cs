using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

namespace SIPAA_CS.App_Code
{
    class SonaPuesto
    {

        //se declaran variables
        public int popcion;
        public string prespuesta;
        public string ptextobuscar;

        public SonaPuesto()
        {


            popcion = 0;
            prespuesta = "";
            ptextobuscar = "";

        }

        //data table 
        public DataTable obtptos(int popcion, string ptextobuscar)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_puestos_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = ptextobuscar;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.asignarConexion(cmd);

            DataTable dtptos = new DataTable();
            Adapter.Fill(dtptos);
            return dtptos;

        }
    }
}