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
        public DataTable dgvregistros(int iopcion, int iiformapago, int idtrab, string sfechainicial, string sfechafinal, 
                                      int cveperiodo, int iidtrabrp, int iidproceso, string susuumod, string prgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechinccalif_p";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iiformapago;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = idtrab;
            cmd.Parameters.Add("@p_fechainicial", SqlDbType.VarChar).Value = sfechainicial;
            cmd.Parameters.Add("@p_fechafinal", SqlDbType.VarChar).Value = sfechafinal;
            cmd.Parameters.Add("@p_cveperiodo", SqlDbType.Int).Value = cveperiodo;
            cmd.Parameters.Add("@p_idtrabrp", SqlDbType.Int).Value = iidtrabrp;
            cmd.Parameters.Add("@p_idproceso", SqlDbType.Int).Value = iidproceso;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = prgumod;

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

        //procesa incidencias
        public int vuidprocesainc(int iopcion, int iiformapago, int idtrab, string sfechainicial, string sfechafinal,
                                      int cveperiodo, int iidtrabrp, int iidproceso, string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechinccalif_p";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iiformapago;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = idtrab;
            cmd.Parameters.Add("@p_fechainicial", SqlDbType.VarChar).Value = sfechainicial;
            cmd.Parameters.Add("@p_fechafinal", SqlDbType.VarChar).Value = sfechafinal;
            cmd.Parameters.Add("@p_cveperiodo", SqlDbType.Int).Value = cveperiodo;
            cmd.Parameters.Add("@p_idtrabrp", SqlDbType.Int).Value = iidtrabrp;
            cmd.Parameters.Add("@p_idproceso", SqlDbType.Int).Value = iidproceso;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);
            cmd.CommandTimeout = 120;
            cmd.ExecuteScalar();
            
            objConexion.cerrarConexion();
            return 1;
        }

        //combo tipo de nomina
        public DataTable cbincrp(int iopcion, int cvperiodo)
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

        //re-procesa incidencias
        public int vuidreprocesainc(int iopcion, int iformapago, int iidtrab, string sfecini, string sfecfin, int icveperiodo, int iidtradrpinc, int idproceso, string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechinccalif_p";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iformapago;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = iidtrab;
            cmd.Parameters.Add("@p_fechainicial", SqlDbType.VarChar).Value = sfecini;
            cmd.Parameters.Add("@p_fechafinal", SqlDbType.VarChar).Value = sfecfin;
            cmd.Parameters.Add("@p_cveperiodo", SqlDbType.Int).Value = icveperiodo;
            cmd.Parameters.Add("@p_idtrabrp", SqlDbType.Int).Value = iidtradrpinc;
            cmd.Parameters.Add("@p_idproceso", SqlDbType.Int).Value = idproceso;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);
            cmd.CommandTimeout = 120;
            cmd.ExecuteScalar();

            objConexion.cerrarConexion();
            return 1;
        }
    }
}
