using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;


namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class JustificaIncidencia
    {

        public JustificaIncidencia()
        {

        }

        //llena dgv,cb
        public DataTable dtdgvcb(int iopcion, int icvjustinc, int icvincidencia, string sdescripcion, int icvtipociclo,
                                 int inoeventos, int icvtipoevento, int icvtipoeval, int istjustinc, string susuumod,
                                 string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechjustinc_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvjustinc", SqlDbType.Int).Value = icvjustinc;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = icvincidencia;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sdescripcion;
            cmd.Parameters.Add("@p_cvtipociclo", SqlDbType.Int).Value = icvtipociclo;
            cmd.Parameters.Add("@p_noeventos", SqlDbType.Int).Value = inoeventos;
            cmd.Parameters.Add("@p_cvtipoevento", SqlDbType.Int).Value = icvtipoevento;
            cmd.Parameters.Add("@p_cvtipoeval", SqlDbType.Int).Value = icvtipoeval;
            cmd.Parameters.Add("@p_stjustinc", SqlDbType.Int).Value = istjustinc;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtdgvcbos = new DataTable();
            Adapter.Fill(dtdgvcbos);
            return dtdgvcbos;
        }

        //vuid justifica incidencias
        public int vuidjustinc(int iopcion, int icvjustinc, int icvincidencia, string sdescripcion, int icvtipociclo,
                                 int inoeventos, int icvtipoevento, int icvtipoeval, int istjustinc, string susuumod,
                                 string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechjustinc_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvjustinc", SqlDbType.Int).Value = icvjustinc;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = icvincidencia;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sdescripcion;
            cmd.Parameters.Add("@p_cvtipociclo", SqlDbType.Int).Value = icvtipociclo;
            cmd.Parameters.Add("@p_noeventos", SqlDbType.Int).Value = inoeventos;
            cmd.Parameters.Add("@p_cvtipoevento", SqlDbType.Int).Value = icvtipoevento;
            cmd.Parameters.Add("@p_cvtipoeval", SqlDbType.Int).Value = icvtipoeval;
            cmd.Parameters.Add("@p_stjustinc", SqlDbType.Int).Value = istjustinc;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);

            int iverifact = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifact;
        }


    }
}
