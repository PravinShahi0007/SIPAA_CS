using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.App_Code.Accesos.Asignaciones
{
    class AreaUsuario
    {
        public AreaUsuario()
        {

        }

        //VALIDACION PERMISO AREA (CENTRO DE COSTO)
        public List<int> ObtenerAreaxUsuario(int idcompania, int opcion)
        {

            List<int> ltAreasxUsuario = new List<int>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusuare_s";
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.Add("@cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_idcompania", SqlDbType.Int).Value = idcompania;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                int idarea = reader.GetInt32(reader.GetOrdinal("idplanta"));
                //int idcomp = reader.GetInt32(reader.GetOrdinal("idcompania"));

                ltAreasxUsuario.Add(idarea);
                //MUESTRA LAS AREAS ASIGNADAS A UN USUARIO
                Console.WriteLine(idarea);
                //Console.WriteLine(idcomp);

            }

            objConexion.cerrarConexion();

            return ltAreasxUsuario;
            
        }

        public DataTable ObtenerAreaUsuario( int idcompania)
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusuare_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            //cmd.Parameters.Add("@cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@idcompania", SqlDbType.Int).Value = idcompania;


            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtAreaUsuario = new DataTable();
            Adapter.Fill(dtAreaUsuario);
            return dtAreaUsuario;

        }

        //REPORTE
        public DataTable ReporteAreaUsuario(string cvusuario,string idcompania, string idplanta, string usuumod, string prgumod, int opcion)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusuare_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_idcompania", SqlDbType.VarChar).Value = idcompania;
            cmd.Parameters.Add("@p_idplanta", SqlDbType.VarChar).Value = idplanta;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.VarChar).Value = opcion;


            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtAreaUsuario = new DataTable();
            Adapter.Fill(dtAreaUsuario);
            return dtAreaUsuario;

        }


    }
}
