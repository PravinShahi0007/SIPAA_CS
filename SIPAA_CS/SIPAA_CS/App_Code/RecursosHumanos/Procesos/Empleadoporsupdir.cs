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
    class Empleadoporsupdir
    {

        public Empleadoporsupdir()
        {

        }

        //llena dgv,cb
        public DataTable dtdatos(string sidtrab, int iopcion, string scheca, string sactivo, int icvtipohr,
                                string susuumod, string sprgumod, string sfhinireg, string sfhfinreg)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtrabperfilrh_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sidtrab;
            cmd.Parameters.Add("@P_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@P_checa", SqlDbType.VarChar).Value = scheca;
            cmd.Parameters.Add("@P_activo", SqlDbType.VarChar).Value = sactivo;
            cmd.Parameters.Add("@P_cvtipohr", SqlDbType.Int).Value = icvtipohr;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = sprgumod;
            cmd.Parameters.Add("@P_fhinireg", SqlDbType.VarChar).Value = sfhinireg;
            cmd.Parameters.Add("@P_fhfinreg", SqlDbType.VarChar).Value = sfhfinreg;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtdgvcbo = new DataTable();
            Adapter.Fill(dtdgvcbo);
            return dtdgvcbo;
        }


    }
}
