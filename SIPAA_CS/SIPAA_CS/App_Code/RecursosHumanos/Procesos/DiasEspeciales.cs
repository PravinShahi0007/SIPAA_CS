using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
{
    class DiasEspeciales
    {

        public string sIdTrab = String.Empty;
        public int iCvIncidencia = 0;
        public int iCvTipo = 0;
        public DateTime fFechaInicio = DateTime.Now;
        public DateTime fFechaFin = DateTime.Now;
        public int iDias = 0;
        public TimeSpan tpHoraentrada = DateTime.Now.TimeOfDay;
        public TimeSpan tpHoraSalida = DateTime.Now.TimeOfDay;
        public string sReferencia = String.Empty;
        public int iSubsidio = 0;
        public string sUsuumod = String.Empty;
        public string sPrgumod = String.Empty;




        public DataTable ObtenerDiasEspecialesxTrabajador(DiasEspeciales objDias, int iOpcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtrabdiasesp_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = objDias.sIdTrab;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@P_cvincidencia", SqlDbType.Int).Value = objDias.iCvIncidencia;
            cmd.Parameters.Add("@P_cvtipo", SqlDbType.Int).Value = objDias.iCvTipo;
            cmd.Parameters.Add("@P_FechaInicio", SqlDbType.DateTime).Value = objDias.fFechaInicio;
            cmd.Parameters.Add("@P_FechaFin", SqlDbType.DateTime).Value = objDias.fFechaFin;
            cmd.Parameters.Add("@P_Dias", SqlDbType.Int).Value = objDias.iDias;
            cmd.Parameters.Add("@P_HoraEntrada", SqlDbType.Time).Value = objDias.tpHoraentrada;
            cmd.Parameters.Add("@P_HoraSalida", SqlDbType.Time).Value = objDias.tpHoraSalida;
            cmd.Parameters.Add("@P_Referencia", SqlDbType.VarChar).Value = objDias.sReferencia;
            cmd.Parameters.Add("@P_Subsidio", SqlDbType.Int).Value = objDias.iSubsidio;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = objDias.sUsuumod;
            cmd.Parameters.Add("@P_prgumon", SqlDbType.VarChar).Value = objDias.sPrgumod;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();
            DataTable dt = new DataTable();
            Adapter.Fill(dt);

            return dt;


        }

    }

 
}
