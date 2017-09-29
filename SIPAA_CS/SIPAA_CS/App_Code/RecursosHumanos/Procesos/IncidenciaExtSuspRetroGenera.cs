using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;


//***********************************************************************************************
//Autor: María de Lourdes Kassab Castillo
//Fecha creación: 17-Abril-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Desbloqueo de Incidencias de Trabajadores
//***********************************************************************************************


namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
{
    class DesbloqueoIncidencia
    {
        //variables
        public int iOpcion;
        public string sUsuumod;
        public string sPrgumodr;


        public DesbloqueoIncidencia()
        {
            iOpcion = 0;
            sUsuumod = "";
            sPrgumodr = "";

        } 



        public DataTable obtFechaIniFinPeriodo(int iOpcion, int iIdFormaPago)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtperiodoIT_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iIdFormaPago;
            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtFechaIniFinPeriodo = new DataTable();
            Adapter.Fill(dtFechaIniFinPeriodo);
            return dtFechaIniFinPeriodo;

        }

        //metodo data table para llenar el grid/combo de busqueda
        public DataTable obtPeriodosProcesoIncidencia(int iOpcion, int iIdFormaPago, string sFechaInicioReg, string sFechaFinReg, string sDescripcion, int iStPeriodoProceso, string sUsuumod, string sPrgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtperiodopro_suid";
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

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtPeriodosProcesoIncidencia = new DataTable();
            Adapter.Fill(dtPeriodosProcesoIncidencia);
            return dtPeriodosProcesoIncidencia;
        }

        public DataTable obtNombreEmpleado(string idTrab, int iOpcion /*, int sStatus, int iEnc, string sNombre*/)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_acceusuario_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
            iOpcion = 14;

            cmd.Parameters.Add("@p_cvusuario", SqlDbType.Int).Value = idTrab; //No se ocupa
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = idTrab;
            cmd.Parameters.Add("@p_nombre", SqlDbType.Int).Value = idTrab; //No se ocupa
            cmd.Parameters.Add("@p_passw", SqlDbType.Int).Value = idTrab; //No se ocupa
            cmd.Parameters.Add("@p_stusuario", SqlDbType.Int).Value = idTrab; //No se ocupa
            cmd.Parameters.Add("@p_usuumod", SqlDbType.Int).Value = idTrab; //No se ocupa
            cmd.Parameters.Add("@p_prgumod", SqlDbType.Int).Value = idTrab; //No se ocupa
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtNombreEmpleado = new DataTable();
            Adapter.Fill(dtNombreEmpleado);
            return dtNombreEmpleado;
        }


        public DataTable obtObtieneSupyDir(int iOpcion, int idTrab, string dFini, string dFfin)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccalif_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
            iOpcion = 4;

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = idTrab;
            cmd.Parameters.Add("@p_fini", SqlDbType.NChar).Value = dFini;
            cmd.Parameters.Add("@p_ffin", SqlDbType.NChar).Value = dFfin;
            cmd.Parameters.Add("@p_idtrabsup", SqlDbType.Int).Value = idTrab;
            cmd.Parameters.Add("@p_idtrabdir", SqlDbType.Int).Value = idTrab;
            cmd.Parameters.Add("@p_idtrabsupn", SqlDbType.Int).Value = idTrab;
            cmd.Parameters.Add("@p_idtrabdirn", SqlDbType.Int).Value = idTrab;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.NChar).Value = "LKC";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.NChar).Value = "SQL";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtNombreEmpleado = new DataTable();
            Adapter.Fill(dtNombreEmpleado);
            return dtNombreEmpleado;
        }

        
        //metodo data table para llenar grid 
        public DataTable obtObtieneRechtinc(int iopcion, int idTrab, string dFini, string dFfin)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccalif_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
            iOpcion = 4;
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = idTrab;
            cmd.Parameters.Add("@p_fini", SqlDbType.NChar).Value = dFini;
            cmd.Parameters.Add("@p_ffin", SqlDbType.NChar).Value = dFfin;


            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtObtieneRechtinc = new DataTable();
            Adapter.Fill(dtObtieneRechtinc);
            return dtObtieneRechtinc;
        }
        


    }
}
