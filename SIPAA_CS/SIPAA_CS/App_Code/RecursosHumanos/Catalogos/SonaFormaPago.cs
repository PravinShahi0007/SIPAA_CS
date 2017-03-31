using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPAA_CS.Conexiones;

using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: Marco Dupont
//Fecha creación: 17-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra Formas de Registro Trabajador
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class SonaFormaPago
    {
        //SE DECLARAN VARIABLES
        public int iOpcion;
        public int iIdFormaPago;
        public string sDescripcion;

        public SonaFormaPago()
        {
            iOpcion = 0;
            iIdFormaPago = 0;
            sDescripcion = "";
        }
        //METODO FORMAS DE REGISTRO LLENA GRID
        public DataTable FormaPago_S(int iOpcion, int iIdFormaPago, string sDescripcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonaformapago_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.VarChar).Value = iIdFormaPago;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sDescripcion;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable FormaPago_S = new DataTable();
            Adapter.Fill(FormaPago_S);
            return FormaPago_S;
        }
    }
}
