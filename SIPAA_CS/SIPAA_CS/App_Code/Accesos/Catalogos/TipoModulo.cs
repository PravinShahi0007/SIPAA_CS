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
    class TipoModulo
    {

        public TipoModulo()
        {

        }

        //llena dgv,cb
        public DataTable dtdatos(int iopcion, int icvtipomodulo, string sdescripcion, int istmodulo, string susuumod, 
                                 string sfhumod, string sprgumod, string sequmod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_accetipomod_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvtipomodulo", SqlDbType.Int).Value = icvtipomodulo;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sdescripcion;
            cmd.Parameters.Add("@p_stmodulo", SqlDbType.Int).Value = istmodulo;
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

        //crud datos
        public int cruddatos(int iopcion, int icvtipomodulo, string sdescripcion, int istmodulo, string susuumod,
                             string sfhumod, string sprgumod, string sequmod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_accetipomod_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvtipomodulo", SqlDbType.Int).Value = icvtipomodulo;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sdescripcion;
            cmd.Parameters.Add("@p_stmodulo", SqlDbType.Int).Value = istmodulo;
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
