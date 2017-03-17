using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.Recursos_Humanos.App_Code
{
    class Perfil
    {

        public int CVPerfil;
        public string Descripcion;
        public int Estatus;
        public string UsuuMod;
        public DateTime FhumMod;
        public string PrguMod;


        public Dictionary<int, string> ObtenerListPerfiles(int CvPerfil,string Descripcion,int Estatus)
        {

            Dictionary<int, string> dcPerfiles = new Dictionary<int, string>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"sp_BuscarPerfil";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CvPerfil", SqlDbType.Int).Value = CvPerfil;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;
            cmd.Parameters.Add("@Estatus", SqlDbType.Int).Value = Estatus;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);



            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                Perfil objPerfiles = new Perfil();
                objPerfiles.CVPerfil = reader.GetInt32(reader.GetOrdinal("CVPERFIL"));
                objPerfiles.Descripcion = reader.GetString(reader.GetOrdinal("DESCRIPCION"));


                dcPerfiles.Add(objPerfiles.CVPerfil, objPerfiles.Descripcion);
            }

            objConexion.cerrarConexion();
            return dcPerfiles;

        }


        public DataTable ObtenerPerfilesxBusqueda(string CvPerfil, string Descripcion,string Estatus)
        {


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_accecperfil_S";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CvPerfil", SqlDbType.VarChar).Value = CvPerfil;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;
            cmd.Parameters.Add("@Estatus", SqlDbType.VarChar).Value = Estatus;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtPerfiles = new DataTable();
            Adapter.Fill(dtPerfiles);
            return dtPerfiles;

        }

      

        public List<int> ObtenerPerfilesxUsuario(string cvUsuario)
        {

            List<int> ltPerfilesxUsuario = new List<int>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"sp_BuscarPerfilxUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@cvUsuario", SqlDbType.VarChar).Value = cvUsuario;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                CVPerfil = reader.GetInt32(reader.GetOrdinal("CVPERFIL"));
                ltPerfilesxUsuario.Add(CVPerfil);
            }

            objConexion.cerrarConexion();

            return ltPerfilesxUsuario;

        }



        public int GestionarPerfiles(Perfil objPerfil, int iOpcion)
        {



            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceperfil_SUID";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@CvPerfil", SqlDbType.Int).Value = objPerfil.CVPerfil;
            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = objPerfil.Descripcion;
            cmd.Parameters.Add("@UsuarioUMod", SqlDbType.VarChar).Value = objPerfil.UsuuMod;
            cmd.Parameters.Add("@ProgramaUMod", SqlDbType.VarChar).Value = objPerfil.PrguMod;
            cmd.Parameters.Add("@Opcion", SqlDbType.Int).Value = iOpcion;



            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());


            objConexion.cerrarConexion();

            return iResponse;

        }

        public int AsignarModuloAPerfil(Modulo objModulo, int CVPerfil, int Opcion)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_AsignarModulo";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@P_CVModulo", SqlDbType.VarChar).Value = objModulo.CVModulo;
            cmd.Parameters.Add("@P_CvPerfil", SqlDbType.Int).Value = CVPerfil;
            cmd.Parameters.Add("@P_USUUMOD", SqlDbType.VarChar).Value = objModulo.UsuuMod;
            cmd.Parameters.Add("@P_PRGUMOD", SqlDbType.VarChar).Value = objModulo.PrguMod;
            cmd.Parameters.Add("@P_stact", SqlDbType.Int).Value = objModulo.stact;
            cmd.Parameters.Add("@P_steli", SqlDbType.Int).Value = objModulo.steli;
            cmd.Parameters.Add("@P_stcre", SqlDbType.Int).Value = objModulo.stcre;
            cmd.Parameters.Add("@P_stimp", SqlDbType.Int).Value = objModulo.stimp;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Int).Value = Opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

               int response = Convert.ToInt32(cmd.ExecuteScalar());

            objConexion.cerrarConexion();

            return response;

        }
    }
}
