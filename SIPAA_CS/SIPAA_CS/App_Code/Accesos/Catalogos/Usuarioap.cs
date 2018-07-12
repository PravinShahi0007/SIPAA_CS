using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

namespace SIPAA_CS.App_Code.Accesos.Catalogos
{
    class Usuarioap
    {

        public Usuarioap()
        {

        }

        //llena dgv,cb
        public DataTable dtdatos(int iopcion, string scvusuario, int iidtrab, string snombre, string scorreo,
                                 int icvdominio, string spassw, int icontpass, int istpassw, string sfhsesionant,
                                 string seqsesionant, string sappsesionant, string  sfhsesionult, string seqsesionult, string sappsesionult,
                                 int istusuario, int icvumod, string susuumod, string sfhumod, string sprgumod,
                                 string sequmod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceusuariopa_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = scvusuario;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = iidtrab;
            cmd.Parameters.Add("@p_nombre", SqlDbType.VarChar).Value = snombre;
            cmd.Parameters.Add("@p_correo", SqlDbType.VarChar).Value = scorreo;
            cmd.Parameters.Add("@p_cvdominio", SqlDbType.Int).Value = icvdominio;
            cmd.Parameters.Add("@p_passw", SqlDbType.VarChar).Value = spassw;
            cmd.Parameters.Add("@p_contpass", SqlDbType.Int).Value = icontpass;
            cmd.Parameters.Add("@p_stpassw", SqlDbType.Int).Value = istpassw;
            cmd.Parameters.Add("@p_fhsesionant", SqlDbType.VarChar).Value = sfhsesionant;
            cmd.Parameters.Add("@p_eqsesionant", SqlDbType.VarChar).Value = seqsesionant;
            cmd.Parameters.Add("@p_appsesionant", SqlDbType.VarChar).Value = sappsesionant;
            cmd.Parameters.Add("@p_fhsesionult", SqlDbType.VarChar).Value = sfhsesionult;
            cmd.Parameters.Add("@p_eqsesionult", SqlDbType.VarChar).Value = seqsesionult;
            cmd.Parameters.Add("@p_appsesionult", SqlDbType.VarChar).Value = sappsesionult;
            cmd.Parameters.Add("@p_stusuario", SqlDbType.Int).Value = istusuario;
            cmd.Parameters.Add("@p_cvumod", SqlDbType.Int).Value = icvumod;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_fhumod", SqlDbType.VarChar).Value = sfhumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            cmd.Parameters.Add("@p_equmod", SqlDbType.VarChar).Value = sequmod;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtdgvcboscorreo = new DataTable();
            Adapter.Fill(dtdgvcboscorreo);
            return dtdgvcboscorreo;
        }

        //crud correos
        public int cruddatos(int iopcion, string scvusuario, int iidtrab, string snombre, string scorreo,
                             int icvdominio, string spassw, int icontpass, int istpassw, string sfhsesionant,
                             string seqsesionant, string sappsesionant, string sfhsesionult, string seqsesionult, string sappsesionult,
                             int istusuario, int icvumod, string susuumod, string sfhumod, string sprgumod,
                             string sequmod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceusuariopa_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvusuario", SqlDbType.VarChar).Value = scvusuario;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = iidtrab;
            cmd.Parameters.Add("@p_nombre", SqlDbType.VarChar).Value = snombre;
            cmd.Parameters.Add("@p_correo", SqlDbType.VarChar).Value = scorreo;
            cmd.Parameters.Add("@p_cvdominio", SqlDbType.Int).Value = icvdominio;
            cmd.Parameters.Add("@p_passw", SqlDbType.VarChar).Value = spassw;
            cmd.Parameters.Add("@p_contpass", SqlDbType.Int).Value = icontpass;
            cmd.Parameters.Add("@p_stpassw", SqlDbType.Int).Value = istpassw;
            cmd.Parameters.Add("@p_fhsesionant", SqlDbType.VarChar).Value = sfhsesionant;
            cmd.Parameters.Add("@p_eqsesionant", SqlDbType.VarChar).Value = seqsesionant;
            cmd.Parameters.Add("@p_appsesionant", SqlDbType.VarChar).Value = sappsesionant;
            cmd.Parameters.Add("@p_fhsesionult", SqlDbType.VarChar).Value = sfhsesionult;
            cmd.Parameters.Add("@p_eqsesionult", SqlDbType.VarChar).Value = seqsesionult;
            cmd.Parameters.Add("@p_appsesionult", SqlDbType.VarChar).Value = sappsesionult;
            cmd.Parameters.Add("@p_stusuario", SqlDbType.Int).Value = istusuario;
            cmd.Parameters.Add("@p_cvumod", SqlDbType.Int).Value = icvumod;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_fhumod", SqlDbType.VarChar).Value = sfhumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            cmd.Parameters.Add("@p_equmod", SqlDbType.VarChar).Value = sequmod;
            objConexion.asignarConexion(cmd);

            int iverifact = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifact;
        }

    }
}
