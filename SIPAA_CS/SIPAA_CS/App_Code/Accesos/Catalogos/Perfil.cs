using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.App_Code
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


        public DataTable ObtenerPerfilesxBusqueda(string sCvPerfil, string sDescripcion,string sEstatus)
        {


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceperfil_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@P_CvPerfil", SqlDbType.VarChar).Value = sCvPerfil;
            cmd.Parameters.Add("@P_Descripcion", SqlDbType.VarChar).Value = sDescripcion;
            cmd.Parameters.Add("@P_Estatus", SqlDbType.VarChar).Value = sEstatus;
            cmd.Parameters.Add("@p_usuarioumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_programaumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = 8;

       

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtPerfiles = new DataTable();
            Adapter.Fill(dtPerfiles);
            return dtPerfiles;

        }

      

        public List<int> ObtenerPerfilesxUsuario(string scvUsuario,int iCvPerfil,int iOpcion)
        {

            List<int> ltPerfilesxUsuario = new List<int>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceusuper_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@P_cvUsuario", SqlDbType.VarChar).Value = scvUsuario;
            cmd.Parameters.Add("@P_cvPerfil", SqlDbType.VarChar).Value = iCvPerfil;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@P_Opcion", SqlDbType.VarChar).Value = iOpcion;
            

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

            cmd.Parameters.Add("@p_CvPerfil", SqlDbType.Int).Value = objPerfil.CVPerfil;
            cmd.Parameters.Add("@p_Descripcion", SqlDbType.VarChar).Value = objPerfil.Descripcion;
            cmd.Parameters.Add("@p_estatus", SqlDbType.VarChar).Value = objPerfil.Estatus;
            cmd.Parameters.Add("@p_UsuarioUMod", SqlDbType.VarChar).Value = objPerfil.UsuuMod;
            cmd.Parameters.Add("@p_ProgramaUMod", SqlDbType.VarChar).Value = objPerfil.PrguMod;
            cmd.Parameters.Add("@p_Opcion", SqlDbType.Int).Value = iOpcion;



            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());


            objConexion.cerrarConexion();

            return iResponse;

        }

        public int AsignarModuloAPerfil(Modulo objModulo, int CVPerfil, int Opcion)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "usp_accepermod_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@P_CVModulo", SqlDbType.VarChar).Value = objModulo.CVModulo;
            cmd.Parameters.Add("@P_CvPerfil", SqlDbType.Int).Value = CVPerfil;
            cmd.Parameters.Add("@P_USUUMOD", SqlDbType.VarChar).Value = objModulo.UsuuMod;
            cmd.Parameters.Add("@P_PRGUMOD", SqlDbType.VarChar).Value = objModulo.PrguMod;
            cmd.Parameters.Add("@P_stact", SqlDbType.Int).Value = objModulo.stact;
            cmd.Parameters.Add("@P_steli", SqlDbType.Int).Value = objModulo.steli;
            cmd.Parameters.Add("@P_stcre", SqlDbType.Int).Value = objModulo.stcre;
            cmd.Parameters.Add("@P_stimp", SqlDbType.Int).Value = objModulo.stimp;
            cmd.Parameters.Add("@P_stlec", SqlDbType.Int).Value = objModulo.stlec;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Int).Value = Opcion;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

               int response = Convert.ToInt32(cmd.ExecuteScalar());

            objConexion.cerrarConexion();

            return response;

        }


        public DataTable ReportePerfiles(int cvperfil, string descripcion, string estatus,string usuarioumod, string programaumod, int opcion)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceperfil_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvperfil", SqlDbType.Int).Value = cvperfil;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = descripcion;
            cmd.Parameters.Add("@p_estatus", SqlDbType.VarChar).Value = estatus;
            cmd.Parameters.Add("@p_usuarioumod", SqlDbType.VarChar).Value = usuarioumod;
            cmd.Parameters.Add("@p_programaumod", SqlDbType.VarChar).Value = programaumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;
            
            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtPerfiles = new DataTable();
            Adapter.Fill(dtPerfiles);

            return dtPerfiles;
        }

        public DataTable ReportePerfilesUsuarios(string cvusuario, string cvperfil, string usuumod, string prgumod, int opcion)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceusuper_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = cvusuario;
            cmd.Parameters.Add("@p_cvperfil", SqlDbType.VarChar).Value = cvperfil;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtPerfilesUsuarios = new DataTable();
            Adapter.Fill(dtPerfilesUsuarios);

            return dtPerfilesUsuarios;
        }

        public DataTable ObtenerPerfiles(string sCvPerfil, string sDescripcion, string sEstatus, int opcion)
        {


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceperfil_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@P_CvPerfil", SqlDbType.VarChar).Value = sCvPerfil;
            cmd.Parameters.Add("@P_Descripcion", SqlDbType.VarChar).Value = sDescripcion;
            cmd.Parameters.Add("@P_Estatus", SqlDbType.VarChar).Value = sEstatus;
            cmd.Parameters.Add("@p_usuarioumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_programaumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;



            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtPerfiles = new DataTable();
            Adapter.Fill(dtPerfiles);
            return dtPerfiles;

        }

        public DataTable ReportePerfilesModulos(string cvmodulo, string cvperfil, string usuumod, string prgumod, int stact, int steli, int stcre, int stimp, int stlec, int opcion)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_accepermod_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@P_cvmodulo", SqlDbType.VarChar).Value = cvmodulo;
            cmd.Parameters.Add("@P_cvperfil", SqlDbType.VarChar).Value = cvperfil;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = usuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = prgumod;
            cmd.Parameters.Add("@P_stact", SqlDbType.Int).Value = stact;
            cmd.Parameters.Add("@P_steli", SqlDbType.Int).Value = steli;
            cmd.Parameters.Add("@P_stcre", SqlDbType.Int).Value = stcre;
            cmd.Parameters.Add("@P_stimp", SqlDbType.Int).Value = stimp;
            cmd.Parameters.Add("@P_stlec", SqlDbType.Int).Value = stlec;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Int).Value = opcion;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtPerfilesUsuarios = new DataTable();
            Adapter.Fill(dtPerfilesUsuarios);

            return dtPerfilesUsuarios;
        }


    }
}
