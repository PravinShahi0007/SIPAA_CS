using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;
using Neurotec.Biometrics;
using System.Drawing;
using System.Windows.Forms;

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


        public DataTable ObtenerInformacionTrabajador(int popcion, string ptextoabuscar)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonatrabajador_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            SqlConnection sqlcn = objConexion.conexionSonarh();

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = ptextoabuscar;

            objConexion.asignarConexions(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexions();

            DataTable dtEmpleados = new DataTable();
            dadapter.Fill(dtEmpleados);
            return (dtEmpleados);
        }

        public DataTable ObtenerPerfilTrabajador(string sIdtrab,int iOpcion,string sCheca,string sEstatus,int iCvtipohr,string sUsuumod,string sPrgumod)
        {
            SqlCommand cmd = new SqlCommand();
            Conexion objConexion = new Conexion();
            Usuario objusuario = new Usuario();


            cmd.CommandText = "usp_rechtrabperfil_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sIdtrab;
            cmd.Parameters.Add("@P_opcion", SqlDbType.VarChar).Value = iOpcion;
            cmd.Parameters.Add("@P_checa", SqlDbType.VarChar).Value = sCheca;
            cmd.Parameters.Add("@P_activo", SqlDbType.VarChar).Value = sEstatus;
            cmd.Parameters.Add("@P_cvtipohr", SqlDbType.Int).Value = iCvtipohr;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = sUsuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = sPrgumod;
            
            objConexion.asignarConexion(cmd);
            
            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtTrabajador = new DataTable();
            dadapter.Fill(dtTrabajador);
            return (dtTrabajador);
        }


        public DataTable GestionHuella(byte[] arrImagen, string sIdtrab,string sUsuumod,string sPrgmod,int iOpcion)
        {

            SqlCommand cmd = new SqlCommand();
            Conexion objConexion = new Conexion();
            Usuario objusuario = new Usuario();

            

            cmd.CommandText = "usp_rechhuella_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sIdtrab;
            cmd.Parameters.Add("@P_template",SqlDbType.Image).Value = arrImagen;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = sUsuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = sPrgmod;
            cmd.Parameters.Add("@P_opcion", SqlDbType.Int).Value = iOpcion;

 
            objConexion.asignarConexion(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtTrabajador = new DataTable();
            dadapter.Fill(dtTrabajador);
            return (dtTrabajador);


        }

    }
}
