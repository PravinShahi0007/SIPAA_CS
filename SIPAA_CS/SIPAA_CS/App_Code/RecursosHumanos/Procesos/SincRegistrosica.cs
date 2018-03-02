using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación: 15-nov-2017      Última Modificacion: dd-mm-aaaa
//Descripción: sincroniza registros de sica
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
{
    class SincRegistrosica
    {

        Conexion conex = new Conexion();

        public SincRegistrosica()
        {
            
        }

        //grid
        public DataTable sincregsica(string sfecini, string sfecfin, string sidtrab)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select b.paterno + ' ' + b.materno + ' ' + b.nombre + '   (' + cast(a.idtrab as varchar(10)) + ')' as Trabajador, " +
                              "convert(varchar(10), fechareg, 103) as Fecha, convert(varchar, horareg, 108) as Registo " +
                              "from dbo.rhregistro a inner join dbo.rhtrabajador b on a.idtrab = b.idtrab " +
                              "where a.fechareg between '" + sfecini + "' and '" + sfecfin + "' " +
                              "and a.idreloj not in (23,21,20,15,13,1,16,5,25,14,22,11,2) " +
                              "and a.idtrab in (" + sidtrab + ") " +
                              "order by 1 asc ";
            SqlConnection sqlcn = conex.conexsica();
            conex.asignarconexsica(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            conex.cerrarconexsica();

            DataTable sicaregsic = new DataTable();
            Adapter.Fill(sicaregsic);
            return sicaregsic;
        }

        //grid
        public DataTable dtsincsicasipa(string sfecini, string sfecfin, string sidtrab)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select idtrab, " +
                              "convert(varchar(10), fechareg, 103) as fecha, convert(varchar, horareg, 108) as registro " +
                              "from dbo.rhregistro " +
                              "where fechareg between '" + sfecini + "' and '" + sfecfin + "' " +
                              "and idreloj not in (23,21,20,15,13,1,16,5,25,14,22,11,2) " +
                              "and idtrab in (" + sidtrab + ") ";
            SqlConnection sqlcn = conex.conexsica();
            conex.asignarconexsica(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            conex.cerrarconexsica();

            DataTable sicaregsic = new DataTable();
            Adapter.Fill(sicaregsic);
            return sicaregsic;
        }

        //combo tipo de nomina
        public DataTable cbsincsica(int iopcion, int cvperiodo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtperiodopro_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = cvperiodo;
            cmd.Parameters.Add("@p_fhinireg", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_fhfinreg", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_stperiodopro", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable tiponomina = new DataTable();
            Adapter.Fill(tiponomina);
            return tiponomina;
        }

    }
}
