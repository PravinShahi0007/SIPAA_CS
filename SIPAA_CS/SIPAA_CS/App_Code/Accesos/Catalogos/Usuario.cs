using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.App_Code
{
    class Usuario
    {

        public string CVUsuario;
        public int Idtrab;
        public string Nombre;
        public string Pass;
        public int StUsuario;
        public string UsuuMod;
        public DateTime FhumMod;
        public string PrguMod;
        public int st;
        public int enc;
        public int opcion;




        public List<Usuario> ObtenerUsuariosxBusqueda(string Nombre, string idTrab)
        {
            List<Usuario> ltUsuarios = new List<Usuario>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"sp_BuscarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = Nombre;
            cmd.Parameters.Add("@IDTRAB", SqlDbType.VarChar).Value = idTrab;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                Usuario objUsuario = new Usuario();
                objUsuario.CVUsuario = reader.GetString(reader.GetOrdinal("CVUSUARIO"));
                objUsuario.Idtrab = reader.GetInt32(reader.GetOrdinal("IDTRAB"));
                objUsuario.Nombre = reader.GetString(reader.GetOrdinal("NOMBRE"));
                objUsuario.Pass = reader.GetString(reader.GetOrdinal("PASSW"));
                objUsuario.UsuuMod = reader.GetString(reader.GetOrdinal("USUUMOD"));
                objUsuario.FhumMod = reader.GetDateTime(reader.GetOrdinal("FHUMOD"));
                objUsuario.PrguMod = reader.GetString(reader.GetOrdinal("PRGUMOD"));


                ltUsuarios.Add(objUsuario);
            }

            objConexion.cerrarConexion();

            return ltUsuarios;

        }


        public DataTable ObtenerDataTableUsuarios(List<Usuario> ltUsuario)
        {


            DataTable dtUsuarios = new DataTable();
            dtUsuarios.Columns.Add("CvUsuario");
            dtUsuarios.Columns.Add("IdTrab");
            dtUsuarios.Columns.Add("Nombre");

            for (int iContador = 0; iContador < ltUsuario.Count(); iContador++)
            {

                Usuario objUsuarioActual = new Usuario();
                objUsuarioActual = ltUsuario.ElementAt(iContador);
                DataRow row = dtUsuarios.NewRow();
                row["CvUsuario"] = objUsuarioActual.CVUsuario.ToString();
                row["IdTrab"] = objUsuarioActual.Idtrab.ToString();
                row["Nombre"] = objUsuarioActual.Nombre;

                dtUsuarios.Rows.Add(row);

            }


            return dtUsuarios;

        }


        public void AsignarPerfilaUsuario(string CVUsuario, int CVPerfil, string UsuuMod, string PrguMod)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_acceasignaperfil_ui";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CVUsuario", SqlDbType.VarChar).Value = CVUsuario;
            cmd.Parameters.Add("@CvPerfil", SqlDbType.Int).Value = CVPerfil;
            cmd.Parameters.Add("@USUUMOD", SqlDbType.VarChar).Value = UsuuMod;
            cmd.Parameters.Add("@PRGUMOD", SqlDbType.VarChar).Value = PrguMod;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.ExecuteNonQuery();

            objConexion.cerrarConexion();


        }



        public List<string> ObtenerListaModulosxUsuario(string CVUsuario)
        {

            Perfil objPerfil = new Perfil();
            List<int> ltPerfiles = objPerfil.ObtenerPerfilesxUsuario(CVUsuario,0,4);

            List<string> ltModulosxUsuario = new List<string>();


            foreach (int iCV in ltPerfiles)
            {

                Modulo objModulo = new Modulo();
                // int iCVPerfil = ltPerfiles.ElementAt(iCV);
                List<string> ltModulos = objModulo.obtenerModulosxPerfil(iCV);

                foreach (string obj in ltModulos)
                {

                    ltModulosxUsuario.Add(obj);
                }

            }
            return ltModulosxUsuario;

            //for (int iContador = 0; iContador < ltPerfiles.Count(); iContador++)
            //{
            //    Modulo objModulo = new Modulo();
            //    int iCVPerfil = ltPerfiles.ElementAt(iContador);
            //    List<string> ltModulos = objModulo.obtenerModulosxPerfil(iCVPerfil);

            //    for (int iCont = 0; iCont < ltModulos.Count(); iContador++)
            //    {
            //        string strCVMod = ltModulos.ElementAt(iCont);
            //        ltModulosxUsuario.Add(strCVMod);
            //    }
            //}

        }

        public Usuario ObtenerListaTrabajadorUsuario(int Idtrab)
        {
            SqlCommand cmd = new SqlCommand();
            Conexion objConexion = new Conexion();
            Usuario objusuario = new Usuario();
            

            cmd.CommandText = "usp_TrabajadorUsu_s";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = Idtrab;
            cmd.Parameters.Add("@Nom", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Sta", SqlDbType.Int, 50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Enc", SqlDbType.Int, 50).Direction = ParameterDirection.Output;
            objConexion.asignarConexion(cmd);
            cmd.ExecuteNonQuery();

            objusuario.Nombre = Convert.ToString(cmd.Parameters["@Nom"].Value.ToString());
            objusuario.st = Convert.ToInt32(cmd.Parameters["@Sta"].Value.ToString());
            objusuario.enc = Convert.ToInt32(cmd.Parameters["@Enc"].Value.ToString());
            
            objConexion.cerrarConexion();
            return objusuario;
        }

        public int AsignarAccesoUsuario(string cvusuario, int idtrab, string nombre, string passw, int stusuario, string usuumod, string prgmod, int opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_acceusuario_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = idtrab;
            cmd.Parameters.Add("@p_nombre", SqlDbType.VarChar).Value = nombre;
            cmd.Parameters.Add("@p_passw", SqlDbType.VarChar).Value = passw;
            cmd.Parameters.Add("@p_stusuario", SqlDbType.Int).Value = stusuario;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgmod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.VarChar).Value = opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            int regreso = Convert.ToInt32(cmd.ExecuteScalar());

            objConexion.cerrarConexion();

            return regreso;
        }

        public DataTable ObtenerAccesosUsuario(string cvusuario, int idtrab, string nombre, string passw, int stusuario, string usuumod, string prgmod, int opcion)
        {

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"SELECT CVUSUARIO
                                        ,IDTRAB
                                        ,NOMBRE
                                        ,STUSUARIO                                 
                                  FROM [dbo].[ACCECUSUARIO] 
                                    WHERE CVUSUARIO LIKE '%'+ @CVUsuario +'%' ";
            //cmd.CommandText = "sp_AdministracionAccesoUsuario";

            cmd.Parameters.Add("@CVUsuario", SqlDbType.VarChar).Value = cvusuario;
            //cmd.Parameters.Add("@IdTrab", SqlDbType.Int).Value = idtrab;
            //cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = nombre;
            //cmd.Parameters.Add("@Passw", SqlDbType.VarChar).Value = passw;
            //cmd.Parameters.Add("@StUsuario", SqlDbType.Int).Value = stusuario;
            //cmd.Parameters.Add("@UsuUmod", SqlDbType.VarChar).Value = usuumod;
            //cmd.Parameters.Add("@PrguMod", SqlDbType.VarChar).Value = prgmod;
            //cmd.Parameters.Add("@Opcion", SqlDbType.Int).Value = opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dgvAccesoUsu = new DataTable();

            Adapter.Fill(dgvAccesoUsu);

            return dgvAccesoUsu;
        }

        public int EliminarAccesoUsuario(string cvusuario, int idtrab, string nombre, string passw, int stusuario, string usuumod, string prgmod, int opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_administracionaccesousuario_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CVUsuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@IdTrab", SqlDbType.Int).Value = idtrab;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = nombre;
            cmd.Parameters.Add("@Passw", SqlDbType.VarChar).Value = passw;
            cmd.Parameters.Add("@StUsuario", SqlDbType.Int).Value = stusuario;
            cmd.Parameters.Add("@UsuUmod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@PrguMod", SqlDbType.VarChar).Value = prgmod;
            cmd.Parameters.Add("@Opcion", SqlDbType.Int).Value = opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            //cmd.ExecuteNonQuery();
            int regreso = Convert.ToInt32(cmd.ExecuteScalar());

            objConexion.cerrarConexion();

            return regreso;
        }

        public static class LoginInfo
        {
            public static string IdTrab;
            public static string Nombre;
            public static DataTable dtPermisosTrabajador;
            public static List<string> ltPermisosPantalla;
        }


        public DataTable ObtenerListaUsuarios(string cvusuario, int idtrab, string nombre, string pass, int stusuario, string usumod, string prgmod,int opcion)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceusuario_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = idtrab;
            cmd.Parameters.Add("@p_nombre", SqlDbType.VarChar).Value = nombre;
            cmd.Parameters.Add("@p_passw", SqlDbType.VarChar).Value = pass;
            cmd.Parameters.Add("@p_stusuario", SqlDbType.Int).Value = stusuario;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgmod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;


            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtUsuario = new DataTable();
            Adapter.Fill(dtUsuario);
            return dtUsuario;
        }

        public void AsignarCompaniaUsuario(string cvusuario, int idcompania, string usuumod, string prgumod)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusucom_ui";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@idcompania", SqlDbType.Int).Value = idcompania;
            cmd.Parameters.Add("@usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@prgumod", SqlDbType.VarChar).Value = prgumod;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.ExecuteNonQuery();
            objConexion.cerrarConexion();
            
        }

        public void AsignarAreaUsuario(string cvusuario, int idcompania, int idplanta, string usuumod,string prgumod)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusuare_ui";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@idcompania", SqlDbType.Int).Value = idcompania;
            cmd.Parameters.Add("@idplanta", SqlDbType.Int).Value = idplanta;
            cmd.Parameters.Add("@usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@prgumod", SqlDbType.VarChar).Value = prgumod;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.ExecuteNonQuery();
            objConexion.cerrarConexion();

        }

        public void AsignarUbicacionUsuario(string cvusuario, int idubicacion, string usuumod, string prgumod, int opcion)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusuubi_sui";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_idubicacion", SqlDbType.Int).Value = idubicacion;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.ExecuteNonQuery();
            objConexion.cerrarConexion();

        }

        public void AsignarDepartamentosUsuario(string cvusuario, string iddepartamento, string usuumod, string prgumod, int opcion)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accetusudep_sui";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_iddepartamento", SqlDbType.VarChar).Value = iddepartamento;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.ExecuteNonQuery();
            objConexion.cerrarConexion();

        }

        public DataTable ReporteUsuarios(string cvusuario, int idtrab, string nombre, string passw, int stusuario, string usuumod, string prgmod, int opcion)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceusuario_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = idtrab;
            cmd.Parameters.Add("@p_nombre", SqlDbType.VarChar).Value = nombre;
            cmd.Parameters.Add("@p_passw", SqlDbType.VarChar).Value = passw;
            cmd.Parameters.Add("@p_stusuario", SqlDbType.Int).Value = stusuario;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgmod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;



            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);

            return dtIncidencia;
        }

        public static class Permisos
        {
            public static bool Crear;
            public static bool Actualizar;
            public static bool Eliminar;
            public static bool Imprimir;
            public static bool Lectura;
        }
    }

}
