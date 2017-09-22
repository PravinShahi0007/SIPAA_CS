using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: Jaime Avendaño Vargas
//Fecha creación: 04-Abril-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Reasignacion de Supervisor y Director
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
{
    class ReasignaSupyDirector
    {
        //variables
        public int iOpcion;
        public string sUsuumod;
        public string sPrgumodr;

        public ReasignaSupyDirector()
        {
            iOpcion = 0;
            sUsuumod = "";
            sPrgumodr = "";
        }

        public DataTable dtfecperiodo(int iopcion, int icveperiodo, string susumod, string sprultmodid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccalif_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = icveperiodo;
            cmd.Parameters.Add("@p_fini", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_ffin", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_idtrabsup", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_idtrabdir", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_idtrabsupn", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_idtrabdirn", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprultmodid;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtfecperiod = new DataTable();
            Adapter.Fill(dtfecperiod);
            return dtfecperiod;
        }

        public DataTable dttrabsupdir(int iopcion, string sfecini, string fecfin, int itrab, string susumod, string sprultmodid, int iformapago)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccalif_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = itrab;
            cmd.Parameters.Add("@p_fini", SqlDbType.VarChar).Value = sfecini;
            cmd.Parameters.Add("@p_ffin", SqlDbType.VarChar).Value = fecfin;
            cmd.Parameters.Add("@p_idtrabsup", SqlDbType.Int).Value = iformapago;
            cmd.Parameters.Add("@p_idtrabdir", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_idtrabsupn", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_idtrabdirn", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprultmodid;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtfecperiod = new DataTable();
            Adapter.Fill(dtfecperiod);
            return dtfecperiod;
        }

        //actualiza supervisor y director nuevo
        public int actsupdir(int iopcion, int itrab, string sfecini, string fecfin, int iidtrabsupn, int iidtrabdirn, string susumod, string sprultmodid, int iformapago)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccalif_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = itrab;
            cmd.Parameters.Add("@p_fini", SqlDbType.VarChar).Value = sfecini;
            cmd.Parameters.Add("@p_ffin", SqlDbType.VarChar).Value = fecfin;
            cmd.Parameters.Add("@p_idtrabsup", SqlDbType.Int).Value = iformapago;
            cmd.Parameters.Add("@p_idtrabdir", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_idtrabsupn", SqlDbType.Int).Value = iidtrabsupn;
            cmd.Parameters.Add("@p_idtrabdirn", SqlDbType.Int).Value = iidtrabdirn;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprultmodid;
            objConexion.asignarConexion(cmd);

            int iverifact = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifact;
        }
       
    }
}
