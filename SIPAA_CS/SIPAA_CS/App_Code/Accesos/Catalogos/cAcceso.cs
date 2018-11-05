using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

using SIPAA_CS.Accesos;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:30-oct-2018       Última Modificacion: dd-mm-aaaa
//Descripción: clase acceso valida usuario
//***********************************************************************************************

namespace SIPAA_CS.App_Code.Accesos.Catalogos
{
    class cAcceso
    {

        public cAcceso()
        {

        }

        //llena dgv,cb
        public DataTable dtdatos(int iopcion, string susuario, string spassw)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acce_acceso";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_usuario", SqlDbType.VarChar).Value = susuario;
            cmd.Parameters.Add("@p_passw", SqlDbType.VarChar).Value = spassw;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtdatos = new DataTable();
            Adapter.Fill(dtdatos);
            return dtdatos;
        }

    }
}
