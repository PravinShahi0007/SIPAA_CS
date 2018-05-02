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
    class TrabajadorHorario
    {
        public string sIdTrab;
        public int iCvPlantilla;
        public int iCvDia;
        public string sHoraEntrada;
        public int iCvdiaSalidaTurno;
        public string sHoraSalidaTurno;
        public double iTiempoComida;
        public int iCvdiaComidaInicio;
        public string sHoraComidaInicio;
        public int iCvdiaComidaFin;
        public string sHoraComidaFin;
        public double iHorasTotalTrabajo;
        public string sUsuumod;
        public string sPrgumod;


         
        public DataTable GestionHorario(int iOpcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtrabhorario_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            

            cmd.Parameters.Add("@P_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = this.sIdTrab;
            cmd.Parameters.Add("@P_idplantilla", SqlDbType.Int).Value = this.iCvPlantilla;
            cmd.Parameters.Add("@P_cvdia", SqlDbType.Int).Value = this.iCvDia;
            cmd.Parameters.Add("@P_hrentrada", SqlDbType.Time).Value = this.sHoraEntrada;

            if (this.iCvdiaSalidaTurno > 0)
                cmd.Parameters.Add("@P_cvdiaSalidaTurno", SqlDbType.Int).Value = this.iCvdiaSalidaTurno;
            else
                cmd.Parameters.Add("@P_cvdiaSalidaTurno", SqlDbType.Int).Value = DBNull.Value;

            if (this.sHoraSalidaTurno.Trim().Equals(":"))
                cmd.Parameters.Add("@P_hrSalidaTurno", SqlDbType.Time).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@P_hrSalidaTurno", SqlDbType.Time).Value = this.sHoraSalidaTurno;

            if (this.iTiempoComida == 0)
                cmd.Parameters.Add("@P_tiempocomida", SqlDbType.Int).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@P_tiempocomida", SqlDbType.Int).Value = this.iTiempoComida;

            cmd.Parameters.Add("@P_cvdiaComidaInicio", SqlDbType.Int).Value = this.iCvdiaComidaInicio;
            
            if (this.sHoraComidaInicio.Trim().Equals(":"))
                cmd.Parameters.Add("@P_hrComidaInicio", SqlDbType.Time).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@P_hrComidaInicio", SqlDbType.Time).Value = this.sHoraComidaInicio;

            cmd.Parameters.Add("@P_cvdiaComidaFin", SqlDbType.Int).Value = this.iCvdiaComidaFin;
            
            if (this.sHoraComidaFin.Trim().Equals(":"))
                cmd.Parameters.Add("@P_hrComidaFin", SqlDbType.Time).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@P_hrComidaFin", SqlDbType.Time).Value = this.sHoraComidaFin;

            if (this.iHorasTotalTrabajo == 0)
                cmd.Parameters.Add("@P_noTotalhoras", SqlDbType.VarChar).Value = DBNull.Value;
            else
                cmd.Parameters.Add("@P_noTotalhoras", SqlDbType.VarChar).Value = this.iHorasTotalTrabajo;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = this.sUsuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = this.sPrgumod;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtResp = new DataTable();
            Adapter.Fill(dtResp);
            return dtResp;


        }

    }

   

}
