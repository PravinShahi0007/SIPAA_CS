using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: Benjamin Huizar Barajas
//Fecha creación: 28-Mar-2017       
//Última Modificacion: 12-09-2017 Karla Torres Ramírez
//Descripción: Administra Períodos Proceso Incidencias
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
{
    class PeriodoProcesoIncidencia
    {
        //variables
        public int iOpcion;
        public int iIdFormaPago;
        public string sFechaInicioReg;
        public string sFechaFinReg;
        public string sDescripcion;
        public int iStPeriodoProceso;
        public string sUsuumod;
        public string sPrgumodr;
        public int iRespuesta;
        public int iRespuestaValidacion;
        public string sFechaInicioProcInc;
        public string sFechaFinProcInc;
        public string sFechaInicioConsInc;
        public string sFechaFinConsInc;
        public string sFechaInicioCalifSup;
        public string sFechaFinCalifSup;
        public string sFechaInicioCalifDir;
        public string sFechaFinCalifDir;
        public string sFechaInicioUpdate;
        public string sFechaFinUpdate;


        public PeriodoProcesoIncidencia()
        {
            iOpcion = 0;
            iIdFormaPago = 0;
            sFechaInicioReg = "";
            sFechaFinReg = "";
            sDescripcion = "";
            iStPeriodoProceso = 0;
            sUsuumod = "";
            sPrgumodr = "";
            iRespuesta = 0;
            iRespuestaValidacion = 0;
            sFechaInicioProcInc = "";
            sFechaFinProcInc = "";
            sFechaInicioConsInc = "";
            sFechaFinConsInc = "";
            sFechaInicioCalifSup = "";
            sFechaFinCalifSup = "";
            sFechaInicioCalifDir = "";
            sFechaFinCalifDir = "";
            sFechaInicioUpdate = "";
            sFechaFinUpdate = "" ;


    } // PeriodoProcesoIncidencia()

        //metodo data table para llenar el grid/combo de busqueda
        public DataTable obtPeriodosProcesoIncidencia(int iOpcion, int iIdFormaPago, string sFechaInicioReg, string sFechaFinReg, string sDescripcion, 
            int iStPeriodoProceso, string sUsuumod, string sPrgumodr, string sFechaInicioProcInc, string sFechaFinProcInc, string sFechaInicioConsInc, string
            sFechaFinConsInc, string sFechaInicioCalifSup, string sFechaFinCalifSup, string sFechaInicioCalifDir, string sFechaFinCalifDir, string sFechaInicioUpdate, string sFechaFinUpdate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechperiodoproinc_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iIdFormaPago;
            cmd.Parameters.Add("@p_fhinireg", SqlDbType.VarChar).Value = sFechaInicioReg;
            cmd.Parameters.Add("@p_fhfinreg", SqlDbType.VarChar).Value = sFechaFinReg;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sDescripcion;
            cmd.Parameters.Add("@p_stperiodopro", SqlDbType.Int).Value = iStPeriodoProceso;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = sUsuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sPrgumodr;
            cmd.Parameters.Add("@p_fhiniprocinc", SqlDbType.VarChar).Value = sFechaInicioProcInc;
            cmd.Parameters.Add("@p_fhfinprocinc", SqlDbType.VarChar).Value = sFechaFinProcInc;
            cmd.Parameters.Add("@p_fhiniconsinc", SqlDbType.VarChar).Value = sFechaInicioConsInc;
            cmd.Parameters.Add("@p_fhfinconsinc", SqlDbType.VarChar).Value = sFechaFinConsInc;
            cmd.Parameters.Add("@p_fhinicalifsup", SqlDbType.VarChar).Value = sFechaInicioCalifSup;
            cmd.Parameters.Add("@p_fhfincalifsup", SqlDbType.VarChar).Value = sFechaFinCalifSup;
            cmd.Parameters.Add("@p_fhinicalifdir", SqlDbType.VarChar).Value = sFechaInicioCalifDir;
            cmd.Parameters.Add("@p_fhfincalifdir", SqlDbType.VarChar).Value = sFechaFinCalifDir;
            cmd.Parameters.Add("@p_fhinireg_update", SqlDbType.VarChar).Value = sFechaInicioUpdate;
            cmd.Parameters.Add("@p_fhfinreg_update", SqlDbType.VarChar).Value = sFechaFinUpdate;
            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();
            DataTable dtPeriodosProcesoIncidencia = new DataTable();
            Adapter.Fill(dtPeriodosProcesoIncidencia);
            return dtPeriodosProcesoIncidencia;
        }

        //metodo insertar, actualizar, eliminar registro, listar
        public int udiPeriodosProcesoIncidencia(int iOpcion, int iIdFormaPago, string sFechaInicioReg, string sFechaFinReg, string sDescripcion,
            int iStPeriodoProceso, string sUsuumod, string sPrgumodr, string sFechaInicioProcInc, string sFechaFinProcInc, string sFechaInicioConsInc, string
            sFechaFinConsInc, string sFechaInicioCalifSup, string sFechaFinCalifSup, string sFechaInicioCalifDir, string sFechaFinCalifDir, string sFechaInicioUpdate, string sFechaFinUpdate)
        {
            iRespuesta = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechperiodoproinc_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
           
            // Valida que no exista el registro (Valida Llave duplicada)
            if (iOpcion == 1)
            {
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = 5; // búsqueda de registro por IdFormaPago/FeInicio/FeFin
                cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iIdFormaPago;
                cmd.Parameters.Add("@p_fhinireg", SqlDbType.VarChar).Value = sFechaInicioReg;
                cmd.Parameters.Add("@p_fhfinreg", SqlDbType.VarChar).Value = sFechaFinReg;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sDescripcion;
                cmd.Parameters.Add("@p_stperiodopro", SqlDbType.Int).Value = iStPeriodoProceso;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = sUsuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sPrgumodr;
                cmd.Parameters.Add("@p_fhiniprocinc", SqlDbType.VarChar).Value = sFechaInicioProcInc;
                cmd.Parameters.Add("@p_fhfinprocinc", SqlDbType.VarChar).Value = sFechaFinProcInc;
                cmd.Parameters.Add("@p_fhiniconsinc", SqlDbType.VarChar).Value = sFechaInicioConsInc;
                cmd.Parameters.Add("@p_fhfinconsinc", SqlDbType.VarChar).Value = sFechaFinConsInc;
                cmd.Parameters.Add("@p_fhinicalifsup", SqlDbType.VarChar).Value = sFechaInicioCalifSup;
                cmd.Parameters.Add("@p_fhfincalifsup", SqlDbType.VarChar).Value = sFechaFinCalifSup;
                cmd.Parameters.Add("@p_fhinicalifdir", SqlDbType.VarChar).Value = sFechaInicioCalifDir;
                cmd.Parameters.Add("@p_fhfincalifdir", SqlDbType.VarChar).Value = sFechaFinCalifDir;
                cmd.Parameters.Add("@p_fhinireg_update", SqlDbType.VarChar).Value = sFechaInicioUpdate;
                cmd.Parameters.Add("@p_fhfinreg_update", SqlDbType.VarChar).Value = sFechaFinUpdate;
                objConexion.asignarConexion(cmd);
                //
                iRespuesta = Convert.ToInt32(cmd.ExecuteScalar());
                if (iRespuesta > 0)
                {
                    iRespuestaValidacion = 99; // Intento de duplicar un registro
                }
            }
            if (iRespuesta == 0)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
                cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iIdFormaPago;
                cmd.Parameters.Add("@p_fhinireg", SqlDbType.VarChar).Value = sFechaInicioReg;
                cmd.Parameters.Add("@p_fhfinreg", SqlDbType.VarChar).Value = sFechaFinReg;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sDescripcion;
                cmd.Parameters.Add("@p_stperiodopro", SqlDbType.Int).Value = iStPeriodoProceso;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = sUsuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sPrgumodr;
                cmd.Parameters.Add("@p_fhiniprocinc", SqlDbType.VarChar).Value = sFechaInicioProcInc;
                cmd.Parameters.Add("@p_fhfinprocinc", SqlDbType.VarChar).Value = sFechaFinProcInc;
                cmd.Parameters.Add("@p_fhiniconsinc", SqlDbType.VarChar).Value = sFechaInicioConsInc;
                cmd.Parameters.Add("@p_fhfinconsinc", SqlDbType.VarChar).Value = sFechaFinConsInc;
                cmd.Parameters.Add("@p_fhinicalifsup", SqlDbType.VarChar).Value = sFechaInicioCalifSup;
                cmd.Parameters.Add("@p_fhfincalifsup", SqlDbType.VarChar).Value = sFechaFinCalifSup;
                cmd.Parameters.Add("@p_fhinicalifdir", SqlDbType.VarChar).Value = sFechaInicioCalifDir;
                cmd.Parameters.Add("@p_fhfincalifdir", SqlDbType.VarChar).Value = sFechaFinCalifDir;
                cmd.Parameters.Add("@p_fhinireg_update", SqlDbType.VarChar).Value = sFechaInicioUpdate;
                cmd.Parameters.Add("@p_fhfinreg_update", SqlDbType.VarChar).Value = sFechaFinUpdate;
                objConexion.asignarConexion(cmd);
                //
                iRespuesta = Convert.ToInt32(cmd.ExecuteScalar());
                //
                objConexion.cerrarConexion();
            }
            if (iRespuestaValidacion == 99)
            {
                iRespuesta = iRespuestaValidacion;
            } // if (iRespuestaValidacion == 99)
            //
            objConexion.cerrarConexion();
            //
            return iRespuesta;
        } // public int udiPeriodosProcesoIncidencia(int iOpcion, int iIdFormaPago, string sFechaInicioReg, string sFechaFinReg, string sDescripcion, int iStPeriodoProceso, string sUsuumod, string sPrgumodr)
    } // class PeriodoProcesoIncidencia
} // namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
