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
        public DataTable dtdatos(int iopcion, int icvsancion, int iidtrab, int iidtrabdir, int icvincidenciagen,
                                 int icvtipogen, int icvperiodo, int icvpolitica, string sfsuspension, int istsanción,
                                 string sobssanción, string susumodifst, string sfhumodst, string sprgumodst, string sequmodst)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sancionesprocesos_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvsancion", SqlDbType.Int).Value = icvsancion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = iidtrab;
            cmd.Parameters.Add("@p_idtrabdir", SqlDbType.Int).Value = iidtrabdir;
            cmd.Parameters.Add("@p_cvincidenciagen", SqlDbType.Int).Value = icvincidenciagen;
            cmd.Parameters.Add("@p_cvtipogen", SqlDbType.Int).Value = icvtipogen;
            cmd.Parameters.Add("@p_cvperiodo", SqlDbType.Int).Value = icvperiodo;
            cmd.Parameters.Add("@p_cvpolitica", SqlDbType.Int).Value = icvpolitica;
            cmd.Parameters.Add("@p_fsuspension", SqlDbType.VarChar).Value = sfsuspension;
            cmd.Parameters.Add("@p_stsanción", SqlDbType.Int).Value = istsanción;
            cmd.Parameters.Add("@p_obssanción", SqlDbType.VarChar).Value = sobssanción;
            cmd.Parameters.Add("@p_usumodifst", SqlDbType.VarChar).Value = susumodifst;
            cmd.Parameters.Add("@p_fhumodst", SqlDbType.VarChar).Value = sfhumodst;
            cmd.Parameters.Add("@p_prgumodst", SqlDbType.VarChar).Value = sprgumodst;
            cmd.Parameters.Add("@p_equmodst", SqlDbType.VarChar).Value = sequmodst;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtdgvcboscorreo = new DataTable();
            Adapter.Fill(dtdgvcboscorreo);
            return dtdgvcboscorreo;
        }

        //crud correos
        public int cruddatos(int iopcion, int icvsancion, int iidtrab, int iidtrabdir, int icvincidenciagen,
                             int icvtipogen, int icvperiodo, int icvpolitica, string sfsuspension, int istsanción,
                             string sobssanción, string susumodifst, string sfhumodst, string sprgumodst, string sequmodst)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sancionesprocesos_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvsancion", SqlDbType.Int).Value = icvsancion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = iidtrab;
            cmd.Parameters.Add("@p_idtrabdir", SqlDbType.Int).Value = iidtrabdir;
            cmd.Parameters.Add("@p_cvincidenciagen", SqlDbType.Int).Value = icvincidenciagen;
            cmd.Parameters.Add("@p_cvtipogen", SqlDbType.Int).Value = icvtipogen;
            cmd.Parameters.Add("@p_cvperiodo", SqlDbType.Int).Value = icvperiodo;
            cmd.Parameters.Add("@p_cvpolitica", SqlDbType.Int).Value = icvpolitica;
            cmd.Parameters.Add("@p_fsuspension", SqlDbType.VarChar).Value = sfsuspension;
            cmd.Parameters.Add("@p_stsanción", SqlDbType.Int).Value = istsanción;
            cmd.Parameters.Add("@p_obssanción", SqlDbType.VarChar).Value = sobssanción;
            cmd.Parameters.Add("@p_usumodifst", SqlDbType.VarChar).Value = susumodifst;
            cmd.Parameters.Add("@p_fhumodst", SqlDbType.VarChar).Value = sfhumodst;
            cmd.Parameters.Add("@p_prgumodst", SqlDbType.VarChar).Value = sprgumodst;
            cmd.Parameters.Add("@p_equmodst", SqlDbType.VarChar).Value = sequmodst;
            objConexion.asignarConexion(cmd);

            int iverifact = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifact;
        }


    }
}
