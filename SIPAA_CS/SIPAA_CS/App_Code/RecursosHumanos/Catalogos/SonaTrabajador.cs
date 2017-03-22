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
    class SonaTrabajador
    {
        //declaracion de variables
        public int popcion;
        public string prespuesta;
        public string ptextoabuscar;

        public SonaTrabajador()
        {
            popcion = 0;
            prespuesta = "";
            ptextoabuscar = "";
        }

        //se crea un datatable (trae los registros de la BD
        //del tipo DT el metodo se llama obtenerempleados y recibe una opcion y un texto a buscar
        public DataTable obtenerempleados(int popcion, string ptextoabuscar)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_trabajador_s"; 
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            SqlConnection sqlcn = objConexion.conexionSonarh();

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value=ptextoabuscar;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexions();

            DataTable dtEmpleados = new DataTable();
            dadapter.Fill(dtEmpleados);
            return (dtEmpleados);
        }

    }
}
