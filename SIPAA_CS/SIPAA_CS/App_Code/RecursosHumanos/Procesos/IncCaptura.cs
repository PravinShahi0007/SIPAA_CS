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


        public DataTable ExtrañamientoRetroactivo(IncCaptura objIncidencia, int iOpcion)
        {


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccaptura_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();

            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = objIncidencia.iIdtrab;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.DateTime).Value = iOpcion;
            cmd.Parameters.Add("@P_FechaRegistro", SqlDbType.Int).Value = fFecharegistro;
            cmd.Parameters.Add("@P_cvincidencia", SqlDbType.Int).Value = iCvIncidencia;
            cmd.Parameters.Add("@P_cvtipo", SqlDbType.Int).Value = objIncidencia.iCvTipo;
            cmd.Parameters.Add("@P_cvincidencia2", SqlDbType.Int).Value = objIncidencia.iCvIncidencia2;
            cmd.Parameters.Add("@P_cvtipo2", SqlDbType.VarChar).Value = objIncidencia.iCvTipo2;
            cmd.Parameters.Add("@P_FechaInicio", SqlDbType.Int).Value = objIncidencia.fFechaInicio;
            cmd.Parameters.Add("@P_FechaFin", SqlDbType.DateTime).Value = objIncidencia.fFechaFin;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = objIncidencia.sUsuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.Int).Value = objIncidencia.sPrgumod;
            
            objConexion.asignarConexion(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dt = new DataTable();
            dadapter.Fill(dt);
            return (dt);
        }



    }
}
