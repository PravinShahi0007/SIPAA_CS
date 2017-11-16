using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class IncCalificacion
    {
        public string sIdtrab = "";
        public DateTime fFechaRegistro = DateTime.Today;
        public int iCvincidencia = 0;
        public double dTiempoEmp = 0;
        public double dTiempoPro = 0;
        public int iIdTrabSupervisor = 0;
        public int iStSupervisor = 0;
        public DateTime fFhautSupervisor = DateTime.Today;
        public int iIdTrabDirector = 0;
        public int iStDirector = 0;
        public DateTime fFhautDirector = DateTime.Today;
        public int iPremioPuntualidad = 0;
        public int iPremioAsistencia = 0;
        public int iStinc = 0;
        public string sArchivo = "";
        public DateTime fFechaInicio = DateTime.Today;
        public DateTime fFechaTermino = DateTime.Today;
        public string sUsuumod = "";
        public string sPrgumod = "";

        public DataTable ObtenerCalificacionIncidenciaDetalle(IncCalificacion objIncidencia , int iOpcion) {


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccalif_d_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();

            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = objIncidencia.sIdtrab;
            cmd.Parameters.Add("@P_FechaReg", SqlDbType.DateTime).Value = objIncidencia.fFechaRegistro;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@P_cvincidencia", SqlDbType.Int).Value = objIncidencia.iCvincidencia;
            cmd.Parameters.Add("@P_tiempoemp", SqlDbType.Int).Value = objIncidencia.dTiempoEmp;
            cmd.Parameters.Add("@P_tiempoprof", SqlDbType.Int).Value = objIncidencia.dTiempoPro;
            cmd.Parameters.Add("@P_idtrabsup", SqlDbType.VarChar).Value = objIncidencia.iIdTrabSupervisor;
            cmd.Parameters.Add("@P_stsup", SqlDbType.Int).Value = objIncidencia.iStSupervisor;
            cmd.Parameters.Add("@P_fhautsup", SqlDbType.DateTime).Value = objIncidencia.fFhautSupervisor;
            cmd.Parameters.Add("@P_idtrabdir", SqlDbType.VarChar).Value = objIncidencia.iIdTrabDirector;
            cmd.Parameters.Add("@P_stdir", SqlDbType.Int).Value = objIncidencia.iStDirector;
            cmd.Parameters.Add("@P_premiopunt", SqlDbType.Int).Value = objIncidencia.iPremioAsistencia;
            cmd.Parameters.Add("@P_premioasis", SqlDbType.Int).Value = objIncidencia.iPremioAsistencia;
            cmd.Parameters.Add("@P_stinc", SqlDbType.VarChar).Value = objIncidencia.iStinc;
            cmd.Parameters.Add("@P_archivo", SqlDbType.VarChar).Value = objIncidencia.sArchivo;
            cmd.Parameters.Add("@P_fhautdir", SqlDbType.DateTime).Value = objIncidencia.fFhautDirector;
            cmd.Parameters.Add("@P_FechaInicio", SqlDbType.DateTime).Value = objIncidencia.fFechaInicio;
            cmd.Parameters.Add("@P_FechaFin", SqlDbType.DateTime).Value = objIncidencia.fFechaTermino;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = objIncidencia.sUsuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = objIncidencia.sPrgumod;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dt = new DataTable();
            dadapter.Fill(dt);
            return (dt);
        }

        public int ActualizaStatusInc(string sIdtrab, DateTime fFechaRegistro, int iOpcion, int iCvincidencia, double dTiempoEmp, double dTiempoPro,
            int iIdTrabSupervisor, int iStSupervisor, DateTime fFhautSupervisor, int iIdTrabDirector, int iStDirector, DateTime fFhautDirector,
            int iPremioPuntualidad, int iPremioAsistencia, int iStinc, string sArchivo, DateTime fFechaInicio, DateTime fFechaTermino, string sUsuumod, string sPrgumod)
        {
            int iprespuesta = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccalif_d_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();

            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sIdtrab;
            cmd.Parameters.Add("@P_FechaReg", SqlDbType.DateTime).Value = fFechaRegistro;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@P_cvincidencia", SqlDbType.Int).Value = iCvincidencia;
            cmd.Parameters.Add("@P_tiempoemp", SqlDbType.Int).Value = dTiempoEmp;
            cmd.Parameters.Add("@P_tiempoprof", SqlDbType.Int).Value = dTiempoPro;
            cmd.Parameters.Add("@P_idtrabsup", SqlDbType.VarChar).Value = iIdTrabSupervisor;
            cmd.Parameters.Add("@P_stsup", SqlDbType.Int).Value = iStSupervisor;
            cmd.Parameters.Add("@P_fhautsup", SqlDbType.DateTime).Value = fFhautSupervisor;
            cmd.Parameters.Add("@P_idtrabdir", SqlDbType.VarChar).Value = iIdTrabDirector;
            cmd.Parameters.Add("@P_stdir", SqlDbType.Int).Value = iStDirector;
            cmd.Parameters.Add("@P_premiopunt", SqlDbType.Int).Value = iPremioAsistencia;
            cmd.Parameters.Add("@P_premioasis", SqlDbType.Int).Value = iPremioAsistencia;
            cmd.Parameters.Add("@P_stinc", SqlDbType.VarChar).Value = iStinc;
            cmd.Parameters.Add("@P_archivo", SqlDbType.VarChar).Value = sArchivo;
            cmd.Parameters.Add("@P_fhautdir", SqlDbType.DateTime).Value = fFhautDirector;
            cmd.Parameters.Add("@P_FechaInicio", SqlDbType.DateTime).Value = fFechaInicio;
            cmd.Parameters.Add("@P_FechaFin", SqlDbType.DateTime).Value = fFechaTermino;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = sUsuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sPrgumod;

            objConexion.asignarConexion(cmd);
            iprespuesta = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();

            return (iprespuesta);
        }
    }  
}
