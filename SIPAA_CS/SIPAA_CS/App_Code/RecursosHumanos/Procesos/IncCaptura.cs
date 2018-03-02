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
    class IncCaptura
    {
        public int iIdtrab = 0;
        public DateTime fFecharegistro = DateTime.Now;
        public int iCvIncidencia = 0;
        public int iCvTipo = 0;
        public int iCvIncidencia2 = 0;
        public int iCvTipo2 = 0;
        public DateTime fFechaInicio = DateTime.Now;
        public DateTime fFechaFin = DateTime.Now;
        public string sUsuumod = String.Empty;
        public DateTime fFhumod = DateTime.Now;
        public string sPrgumod = String.Empty;
        public int iAplica = 0;


        public DataTable ExtrañamientoRetroactivo(IncCaptura objIncidencia, int iOpcion)
        {


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccaptura_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();

            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = objIncidencia.iIdtrab;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@P_FechaRegistro", SqlDbType.DateTime).Value = fFecharegistro;
            cmd.Parameters.Add("@P_cvincidencia", SqlDbType.Int).Value = iCvIncidencia;
            cmd.Parameters.Add("@P_cvtipo", SqlDbType.Int).Value = objIncidencia.iCvTipo;
            cmd.Parameters.Add("@P_cvincidencia2", SqlDbType.Int).Value = objIncidencia.iCvIncidencia2;
            cmd.Parameters.Add("@P_cvtipo2", SqlDbType.Int).Value = objIncidencia.iCvTipo2;
            cmd.Parameters.Add("@P_FechaInicio", SqlDbType.DateTime).Value = objIncidencia.fFechaInicio;
            cmd.Parameters.Add("@P_FechaFin", SqlDbType.DateTime).Value = objIncidencia.fFechaFin;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = objIncidencia.sUsuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = objIncidencia.sPrgumod;
            cmd.Parameters.Add("@P_aplica", SqlDbType.Int).Value = objIncidencia.iAplica;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dt = new DataTable();
            dadapter.Fill(dt);
            return (dt);
        }



    }
}
