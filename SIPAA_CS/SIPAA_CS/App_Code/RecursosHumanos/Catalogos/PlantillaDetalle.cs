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
//Fecha creación: 28-mar-2017      Última Modificacion: dd-mm-aaaa
//Descripción: administra plantilla detalle
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class PlantillaDetalle
    {

        public PlantillaDetalle()
        {
            
        }

        //combo plantilla
        public DataTable cbplantilla(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechcplantilla_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvplantilla", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtplantilla = new DataTable();
            Adapter.Fill(dtplantilla);
            return dtplantilla;
        }

        //grid busqueda por plantilla
        public DataTable dgvplantillaDet(int iopcion, int icvplantilla)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechplantilla_d_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvplantilla", SqlDbType.Int).Value = icvplantilla;
            cmd.Parameters.Add("@p_cvdia", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_hrenttur", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_cvddiasaltur", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_hrsaltur", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_tcomida", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_cvdiasalcom", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_hrsalcom", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_cvdiaregcom", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_hrregcom", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_tothjor", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtplantg = new DataTable();
            Adapter.Fill(dtplantg);
            return dtplantg;
        }

        //combo dias
        public DataTable cbdias(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechcdsemana_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvdia", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtdias = new DataTable();
            Adapter.Fill(dtdias);
            return dtdias;
        }




    }
}
