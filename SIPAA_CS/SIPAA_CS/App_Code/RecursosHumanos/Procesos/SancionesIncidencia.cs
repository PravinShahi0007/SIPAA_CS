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
    class SancionesIncidencia

    { 
     public SancionesIncidencia()
    {

    }

    //llena dgv,cb
    public DataTable dtdatos(int iopcion, int icvpolitica, string sdescpolitica, int icvnivel, int icvtiponomina, 
                             int inumeventos, int icvincrepresenta, int icvevaluacion, int isancgenera, int iordenejec,
                             int istpolitica, string susuumod, string sprgumod)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = @"usp_rechcsanciones_suid";
        cmd.CommandType = CommandType.StoredProcedure;
        Conexion objConexion = new Conexion();
        objConexion.asignarConexion(cmd);

        cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
        cmd.Parameters.Add("@p_cvpolitica", SqlDbType.Int).Value = icvpolitica;
        cmd.Parameters.Add("@p_descpolitica", SqlDbType.VarChar).Value = sdescpolitica;
        cmd.Parameters.Add("@p_cvnivel", SqlDbType.Int).Value = icvnivel;
        cmd.Parameters.Add("@p_cvtiponomina", SqlDbType.Int).Value = icvtiponomina;
        cmd.Parameters.Add("@p_numeventos", SqlDbType.Int).Value = inumeventos;
        cmd.Parameters.Add("@p_cvincrepresenta", SqlDbType.Int).Value = icvincrepresenta;
        cmd.Parameters.Add("@p_cvevaluacion", SqlDbType.Int).Value = icvevaluacion;
        cmd.Parameters.Add("@p_sancgenera", SqlDbType.Int).Value = isancgenera;
        cmd.Parameters.Add("@p_ordenejec", SqlDbType.Int).Value = iordenejec;
        cmd.Parameters.Add("@p_stpolitica", SqlDbType.Int).Value = istpolitica;
        cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
        cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
        objConexion.asignarConexion(cmd);

        SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
        objConexion.cerrarConexion();

        DataTable dtdgvcboscorreo = new DataTable();
        Adapter.Fill(dtdgvcboscorreo);
        return dtdgvcboscorreo;
    }

    //crud correos
    public int cruddatos(int iopcion, int icvpolitica, string sdescpolitica, int icvnivel, int icvtiponomina,
                         int inumeventos, int icvincrepresenta, int icvevaluacion, int isancgenera, int iordenejec,
                         int istpolitica, string susuumod, string sprgumod)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = @"usp_rechcsanciones_suid";
        cmd.CommandType = CommandType.StoredProcedure;
        Conexion objConexion = new Conexion();
        objConexion.asignarConexion(cmd);

        cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
        cmd.Parameters.Add("@p_cvpolitica", SqlDbType.Int).Value = icvpolitica;
        cmd.Parameters.Add("@p_descpolitica", SqlDbType.VarChar).Value = sdescpolitica;
        cmd.Parameters.Add("@p_cvnivel", SqlDbType.Int).Value = icvnivel;
        cmd.Parameters.Add("@p_cvtiponomina", SqlDbType.Int).Value = icvtiponomina;
        cmd.Parameters.Add("@p_numeventos", SqlDbType.Int).Value = inumeventos;
        cmd.Parameters.Add("@p_cvincrepresenta", SqlDbType.Int).Value = icvincrepresenta;
        cmd.Parameters.Add("@p_cvevaluacion", SqlDbType.Int).Value = icvevaluacion;
        cmd.Parameters.Add("@p_sancgenera", SqlDbType.Int).Value = isancgenera;
        cmd.Parameters.Add("@p_ordenejec", SqlDbType.Int).Value = iordenejec;
        cmd.Parameters.Add("@p_stpolitica", SqlDbType.Int).Value = istpolitica;
        cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
        cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
        objConexion.asignarConexion(cmd);

        int iverifact = Convert.ToInt32(cmd.ExecuteScalar());
        objConexion.cerrarConexion();
        return iverifact;
    }
 }
}
