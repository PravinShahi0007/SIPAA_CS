using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

namespace SIPAA_CS.Recursos_Humanos.App_Code
{
    class CompaniasSonarh
    {

        //se declaran variables
        public int popcion;
        public string prespuesta;
        public string ptextobuscar;

        public CompaniasSonarh()
        {


            popcion = 0;
            prespuesta = "";
            ptextobuscar = "";

        }

        //data table 
        public DataTable obtcomp(int popcion, string ptextobuscar)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_compania_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            SqlConnection sqlcn = objConexion.conexionSonarh();

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = ptextobuscar;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexions();

            DataTable dtcomp = new DataTable();
            Adapter.Fill(dtcomp);
            return dtcomp;

        }


    }
}
