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
//Fecha creación: 28-abr-2017      Última Modificacion: dd-mm-aaaa
//Descripción: procesa incidencias
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
{
    class ProcesaIncidencia
    {
        public int iresp;

        public ProcesaIncidencia()
        {
            iresp = 0;
        }

        //combo tipo de nomina
        public DataTable cbtiponomina(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtperiodopro_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = 0;
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

        //grid muestra registros de checado
        public DataTable dgvregistros(int iopcion, int iiformapago, int idtrab)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechinccalif_p";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iiformapago;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = idtrab;
            cmd.Parameters.Add("@p_fechainicial", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_fechafinal", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtreg = new DataTable();
            Adapter.Fill(dtreg);
            return dtreg;
        }

        //dt tipo de nomina seleccionado
        public DataTable dttiponomina(int iopcion, int iformpago)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtperiodopro_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iformpago;
            cmd.Parameters.Add("@p_fhinireg", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_fhfinreg", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_stperiodopro", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dttiponom = new DataTable();
            Adapter.Fill(dttiponom);
            return dttiponom;
        }
    }
}
