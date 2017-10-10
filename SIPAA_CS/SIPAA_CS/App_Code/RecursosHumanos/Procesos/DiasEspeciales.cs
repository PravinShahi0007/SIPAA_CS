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

        public string sIdTrab = "0";
        public int iCvIncidencia = 0;
        public int iCvTipo = 0;
        public DateTime fFechaInicio = DateTime.Now;
        public DateTime fFechaFin = DateTime.Now;
        public int iDias = 0;
        public TimeSpan tpHoraentrada = DateTime.Now.TimeOfDay;
        public TimeSpan tpHoraSalida = DateTime.Now.TimeOfDay;
        public string sReferencia = String.Empty;
        public int iOrden = 1;
        public int iSubsidio = 0;
        public int iIdCompania = 0;
        public int iIPlanta = 0;
        public int iIdtrabrys = 0;
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
            cmd.Parameters.Add("@P_Orden", SqlDbType.Int).Value = objDias.iOrden;
            cmd.Parameters.Add("@P_Subsidio", SqlDbType.Int).Value = objDias.iSubsidio;
            cmd.Parameters.Add("@P_idtrabrys", SqlDbType.Int).Value = objDias.iIdtrabrys;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = objDias.sUsuumod;
            cmd.Parameters.Add("@P_prgumon", SqlDbType.VarChar).Value = objDias.sPrgumod;
            cmd.Parameters.Add("@P_IdCompania", SqlDbType.Int).Value = objDias.iIdCompania;
            cmd.Parameters.Add("@P_IdPlanta", SqlDbType.Int).Value = objDias.iIPlanta;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();
            DataTable dt = new DataTable();
            Adapter.Fill(dt);

            return dt;
        }

        //metodo para insertar registro Dias Esp
        public int InsertarDiasEspecialesxTrabajador(int sIdTrab, int iOpcion, int iCvIncidencia, int iCvTipo, string fFechaInicio, string fFechaFin, int iDias, 
            string tpHoraentrada, string tpHoraSalida, string sReferencia, int iOrden, int iSubsidio, int iIdtrabrys, string spusuumod, string spprgumod, int iIdCompania, int iIPlanta)
        {
            
            int iprespuesta = 0;
            ////////////////////////////////
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtrabdiasesp_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sIdTrab;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@P_cvincidencia", SqlDbType.Int).Value = iCvIncidencia;
            cmd.Parameters.Add("@P_cvtipo", SqlDbType.Int).Value = iCvTipo;
            cmd.Parameters.Add("@P_FechaInicio", SqlDbType.VarChar).Value = fFechaInicio;
            cmd.Parameters.Add("@P_FechaFin", SqlDbType.VarChar).Value = fFechaFin;
            cmd.Parameters.Add("@P_Dias", SqlDbType.Int).Value = iDias;
            cmd.Parameters.Add("@P_HoraEntrada", SqlDbType.VarChar).Value = tpHoraentrada;
            cmd.Parameters.Add("@P_HoraSalida", SqlDbType.VarChar).Value = tpHoraSalida;
            cmd.Parameters.Add("@P_Referencia", SqlDbType.VarChar).Value = sReferencia;
            cmd.Parameters.Add("@P_Orden", SqlDbType.Int).Value = iOrden;
            cmd.Parameters.Add("@P_Subsidio", SqlDbType.Int).Value = iSubsidio;
            cmd.Parameters.Add("@P_idtrabrys", SqlDbType.Int).Value = iIdtrabrys;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = spusuumod;
            cmd.Parameters.Add("@P_prgumon", SqlDbType.VarChar).Value = spprgumod;
            cmd.Parameters.Add("@P_IdCompania", SqlDbType.Int).Value = iIdCompania;
            cmd.Parameters.Add("@P_IdPlanta", SqlDbType.Int).Value = iIPlanta;
            objConexion.asignarConexion(cmd);

            iprespuesta = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();

            return (iprespuesta);
        }
    } 
}
