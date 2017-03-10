using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.Recursos_Humanos.App_Code
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




        public List<Usuario> ObtenerListaUsuarios()
        {
            List<Usuario> ltUsuarios = new List<Usuario>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT [CVUSUARIO]
                              ,[IDTRAB]
                              ,[NOMBRE]
                              ,[PASSW]
                              ,[STUSUARIO]
                              ,[USUUMOD]
                              ,[FHUMOD]
                              ,[PRGUMOD]
                          FROM [dbo].[ACCECUSUARIO]";
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
            cmd.CommandText = "sp_AsignarPerfil";
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

        public DataTable ObtenerUsuariosxBusqueda(string Nombre, string idTrab)
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT [CVUsuario]
                                      ,[IdTrab]
                                      ,[Nombre]
                                     
                                  FROM [dbo].[ACCECUSUARIO] 
                                    WHERE NOMBRE LIKE '%'+ @NOMBRE +'%'
                                     AND IDTRAB LIKE  '%'+ @IDTRAB +'%'  ";


            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = Nombre;
            cmd.Parameters.Add("@IDTRAB", SqlDbType.VarChar).Value = idTrab;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtPerfiles = new DataTable();
            Adapter.Fill(dtPerfiles);
            return dtPerfiles;

        }

        public Usuario ObtenerListaTrabajadorUsuario(int Idtrab)
        {
            SqlCommand cmd = new SqlCommand();
            Conexion objConexion = new Conexion();
            SqlConnection sqlcn = objConexion.conexionSonarh();
            Usuario objusuario = new Usuario();
            
            cmd.Connection = sqlcn;

            sqlcn.Open();

            cmd.CommandText = "Trabajador_Usu";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = Idtrab;

            cmd.Parameters.Add("@Nom", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Sta", SqlDbType.Int, 50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Enc", SqlDbType.Int, 50).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            objusuario.Nombre = Convert.ToString(cmd.Parameters["@Nom"].Value.ToString());
            objusuario.st = Convert.ToInt32(cmd.Parameters["@Sta"].Value.ToString());
            objusuario.enc = Convert.ToInt32(cmd.Parameters["@Enc"].Value.ToString());
            
            return objusuario;
        }

        public int AsignarAccesoUsuario(string cvusuario, int idtrab, string nombre, string passw, int stusuario, string usuumod,string prgmod, int opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_AdministracionAccesoUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CVUsuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@IdTrab", SqlDbType.Int).Value = idtrab;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = nombre;
            cmd.Parameters.Add("@Passw", SqlDbType.VarChar).Value = passw;
            cmd.Parameters.Add("@StUsuario", SqlDbType.Int).Value = stusuario;
            cmd.Parameters.Add("@UsuUmod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@PrguMod", SqlDbType.VarChar).Value = prgmod;
            cmd.Parameters.Add("@Opcion", SqlDbType.VarChar).Value = opcion;
            
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            int regreso = Convert.ToInt32(cmd.ExecuteScalar());

            objConexion.cerrarConexion();

            return regreso;
        }
        
        public DataTable ObtenerAccesosUsuario(string cvusuario,int idtrab,string nombre, string passw, int stusuario,string usuumod,string prgmod, int opcion)
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
            cmd.CommandText = "sp_AdministracionAccesoUsuario";
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
    }
}
