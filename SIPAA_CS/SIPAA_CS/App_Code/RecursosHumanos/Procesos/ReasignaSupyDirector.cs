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

        public ReasignaSupyDirector()
        {
            iOpcion = 0;
        } // RasignaSupyDirector()

        // JAV public DataTable obtPeriodosProcesoIncidencia(int iOpcion, int iIdFormaPago, string sFechaInicioReg, string sFechaFinReg, string sDescripcion, int iStPeriodoProceso, string sUsuumod, string sPrgumodr)
        public DataTable obtFechaIniFinPeriodo(int iOpcion, int iIdFormaPago)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtperiodoIT_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iIdFormaPago;
            // JAV cmd.Parameters.Add("@p_fhinireg", SqlDbType.VarChar).Value = sFechaInicioReg;
            // JAV cmd.Parameters.Add("@p_fhfinreg", SqlDbType.VarChar).Value = sFechaFinReg;
            
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtFechaIniFinPeriodo = new DataTable();
            Adapter.Fill(dtFechaIniFinPeriodo);
            return dtFechaIniFinPeriodo;

        }

    }
}
