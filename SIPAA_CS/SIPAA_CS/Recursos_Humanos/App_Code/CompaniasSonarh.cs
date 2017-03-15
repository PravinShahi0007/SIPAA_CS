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

        public int IdCompania;
        public string DescripcionCia;
        public string DireccionCia;
        public int IdPlanta;
        public string DireccionPlanta;

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


        public DataTable ObtenerPlantelxCompania(int IdCompania, string PlantaDesc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechPlantaxCompania_S";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            SqlConnection sqlcn = objConexion.conexionSonarh();

            cmd.Parameters.Add("@IdCompania", SqlDbType.Int).Value = IdCompania;
            cmd.Parameters.Add("@Planta", SqlDbType.VarChar).Value = PlantaDesc;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexions();

            DataTable dtPlanta = new DataTable();
            Adapter.Fill(dtPlanta);
            return dtPlanta;
   }


        public DataTable ObtenerUbicacionPlantel( string PlantaDesc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechplantelubicacion_S";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            SqlConnection sqlcn = objConexion.conexionSonarh();

            
            cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar).Value = PlantaDesc;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexions();

            DataTable dtPlanta = new DataTable();
            Adapter.Fill(dtPlanta);
            return dtPlanta;
        }

    }
}
