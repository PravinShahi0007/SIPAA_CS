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
        public string sIdtrab;
        public int iCvincidencia;
        public double dTiempoEmp;
        public double dTiempoPro;
        public int iIdTrabSupervisor;
        public int iStSupervisor;
        public DateTime fFhautSupervisor;
        public int iIdTrabDirector;
        public int iStDirector;
        public DateTime fFhautDirector;
        public int iPremioPuntualidad;
        public int iPremioAsistencia;
        public int iStinc;
        public string sArchivo;
        public DateTime fFechaInicio;
        public DateTime fFechaTermino;
        public string sUsuumod;
        public string sPrgumod;



        public DataTable ObtenerCalificacionIncidenciaDetalle(IncCalificacion objIncidencia , int iOpcion) {


            SqlCommand cmd = new SqlCommand();
            Conexion objConexion = new Conexion();
            Usuario objusuario = new Usuario();



            cmd.CommandText = "usp_rechhuella_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = objIncidencia.sIdtrab;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Image).Value = iOpcion;
            cmd.Parameters.Add("@P_cvincidencia", SqlDbType.VarChar).Value = objIncidencia.iCvincidencia;
            cmd.Parameters.Add("@P_tiempoemp", SqlDbType.VarChar).Value = objIncidencia.dTiempoEmp;
            cmd.Parameters.Add("@P_tiempoprof", SqlDbType.Int).Value = objIncidencia.dTiempoPro;
            cmd.Parameters.Add("@P_idtrabsup", SqlDbType.VarChar).Value = objIncidencia.iIdTrabSupervisor;
            cmd.Parameters.Add("@P_stsup", SqlDbType.Image).Value = objIncidencia.iStSupervisor;
            cmd.Parameters.Add("@P_fhautsup", SqlDbType.VarChar).Value = objIncidencia.fFhautSupervisor;
            cmd.Parameters.Add("@P_idtrabdir", SqlDbType.VarChar).Value = objIncidencia.iIdTrabDirector;
            cmd.Parameters.Add("@P_stdir", SqlDbType.Int).Value = objIncidencia.iStDirector;
            cmd.Parameters.Add("@P_fhautdir", SqlDbType.VarChar).Value = objIncidencia.fFhautDirector;
            cmd.Parameters.Add("@P_premiopunt", SqlDbType.Image).Value = objIncidencia.iPremioAsistencia;
            cmd.Parameters.Add("@P_premioasis", SqlDbType.VarChar).Value = objIncidencia.iPremioAsistencia;
            cmd.Parameters.Add("@P_stinc", SqlDbType.VarChar).Value = objIncidencia.iStinc;
            cmd.Parameters.Add("@P_archivo", SqlDbType.Int).Value = objIncidencia.sIdtrab;
            cmd.Parameters.Add("@P_FechaInicio", SqlDbType.Image).Value = objIncidencia.fFechaInicio;
            cmd.Parameters.Add("@P_FechaFin", SqlDbType.VarChar).Value = objIncidencia.fFechaTermino;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = objIncidencia.sUsuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.Int).Value = objIncidencia.sPrgumod;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dt = new DataTable();
            dadapter.Fill(dt);
            return (dt);





        }
    }

    

  
}
