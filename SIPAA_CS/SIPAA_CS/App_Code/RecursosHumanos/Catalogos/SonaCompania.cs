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
    class SonaCompania
    {

        public static class TrabajadorInfo
        {
            public static string IdTrab;
            public static string Nombre;
            public static string IdTrabSupervisor;
            public static string NombreSupervisor;
            public static int cvReloj;
        }


        //se declaran variables
        public int iopcion;
        public string srespuesta;
        public string stextobuscar;

        public SonaCompania()
        {


            iopcion = 0;
            srespuesta = "";
            stextobuscar = "";

        }

        public int IdCompania;
        public string DescripcionCia;
        public string DireccionCia;
        public int IdPlanta;
        public string DireccionPlanta;

        //data table 
        public DataTable obtcomp(int iopcion, string stextobuscar)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonacompania_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
           
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = stextobuscar;
            objConexion.asignarConexions(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.asignarConexion(cmd);
            
            DataTable dtcomp = new DataTable();
            Adapter.Fill(dtcomp);
            return dtcomp;
            ////////////////////////////
          
            ///////////////////////



        }


        public DataTable ObtenerPlantelxCompania(int opcion, string idcompania, string descripcion, string planta)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonaplanta_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;
            cmd.Parameters.Add("@P_idcompania", SqlDbType.VarChar).Value = idcompania;
            cmd.Parameters.Add("@p_DescripcionCia", SqlDbType.VarChar).Value = descripcion;
            cmd.Parameters.Add("@P_Planta", SqlDbType.VarChar).Value = planta;
            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();
            DataTable dtPlanta = new DataTable();
            Adapter.Fill(dtPlanta);
            return dtPlanta;
   }


        public DataTable ObtenerUbicacionPlantel(int iOpcion, string PlantaDesc)
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_sonaubicacion_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@p_ubicacion", SqlDbType.VarChar).Value = PlantaDesc;
           

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtProceso = new DataTable();
            Adapter.Fill(dtProceso);
            return dtProceso;
        }


        public DataTable ObtenerPlantel(int iOpcion,int iIdCompania,string sCompania,string sPlanta)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_sonaplanta_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@P_Opcion", SqlDbType.VarChar).Value = iOpcion;
            cmd.Parameters.Add("@P_idCompania", SqlDbType.VarChar).Value = iIdCompania;
            cmd.Parameters.Add("@P_DescripcionCia", SqlDbType.VarChar).Value = sCompania;
            cmd.Parameters.Add("@P_Planta", SqlDbType.VarChar).Value = sPlanta;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtProceso = new DataTable();
            Adapter.Fill(dtProceso);
            return dtProceso;
        }


    }
}
