using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
{
    class SancionesProceso
    {

        public SancionesProceso()
        {

        }

        //llena dgv,cb
        public DataTable dtdatos(int iopcion, int iidtrab, string sfereget, int icvincidencia, int icvtipo,
                                 int icvincidenciagen, int icvtipogen, int icvperiodo, string susuvobo, string sfhvobo,
                                 string seqvobo, string susucancela, string sfhcancela, string sobscancela, int istsancion,
                                 string sfhgenera)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sancionesprocesos_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = iidtrab;
            cmd.Parameters.Add("@p_fereget", SqlDbType.VarChar).Value = sfereget;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = icvincidencia;
            cmd.Parameters.Add("@p_cvtipo", SqlDbType.Int).Value = icvtipo;
            cmd.Parameters.Add("@p_cvincidenciagen", SqlDbType.Int).Value = icvincidenciagen;
            cmd.Parameters.Add("@p_cvtipogen", SqlDbType.Int).Value = icvtipogen;
            cmd.Parameters.Add("@p_cvperiodo", SqlDbType.Int).Value = icvperiodo;
            cmd.Parameters.Add("@p_usuvobo", SqlDbType.VarChar).Value = susuvobo;
            cmd.Parameters.Add("@p_fhvobo", SqlDbType.VarChar).Value = sfhvobo;
            cmd.Parameters.Add("@p_eqvobo", SqlDbType.VarChar).Value = seqvobo;
            cmd.Parameters.Add("@p_usucancela", SqlDbType.VarChar).Value = susucancela;
            cmd.Parameters.Add("@p_fhcancela", SqlDbType.VarChar).Value = sfhcancela;
            cmd.Parameters.Add("@p_obscancela", SqlDbType.VarChar).Value = sobscancela;
            cmd.Parameters.Add("@p_stsancion", SqlDbType.Int).Value = istsancion;
            cmd.Parameters.Add("@p_fhgenera", SqlDbType.VarChar).Value = sfhgenera;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtdgvcboscorreo = new DataTable();
            Adapter.Fill(dtdgvcboscorreo);
            return dtdgvcboscorreo;
        }

        //crud correos
        public int cruddatos(int iopcion, int iidtrab, string sfereget, int icvincidencia, int icvtipo,
                             int icvincidenciagen, int icvtipogen, int icvperiodo, string susuvobo, string sfhvobo,
                             string seqvobo, string susucancela, string sfhcancela, string sobscancela, int istsancion,
                             string sfhgenera)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sancionesprocesos_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = iidtrab;
            cmd.Parameters.Add("@p_fereget", SqlDbType.VarChar).Value = sfereget;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = icvincidencia;
            cmd.Parameters.Add("@p_cvtipo", SqlDbType.Int).Value = icvtipo;
            cmd.Parameters.Add("@p_cvincidenciagen", SqlDbType.Int).Value = icvincidenciagen;
            cmd.Parameters.Add("@p_cvtipogen", SqlDbType.Int).Value = icvtipogen;
            cmd.Parameters.Add("@p_cvperiodo", SqlDbType.Int).Value = icvperiodo;
            cmd.Parameters.Add("@p_usuvobo", SqlDbType.VarChar).Value = susuvobo;
            cmd.Parameters.Add("@p_fhvobo", SqlDbType.VarChar).Value = sfhvobo;
            cmd.Parameters.Add("@p_eqvobo", SqlDbType.VarChar).Value = seqvobo;
            cmd.Parameters.Add("@p_usucancela", SqlDbType.VarChar).Value = susucancela;
            cmd.Parameters.Add("@p_fhcancela", SqlDbType.VarChar).Value = sfhcancela;
            cmd.Parameters.Add("@p_obscancela", SqlDbType.VarChar).Value = sobscancela;
            cmd.Parameters.Add("@p_stsancion", SqlDbType.Int).Value = istsancion;
            cmd.Parameters.Add("@p_fhgenera", SqlDbType.VarChar).Value = sfhgenera;
            objConexion.asignarConexion(cmd);

            int iverifact = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifact;
        }


    }
}
