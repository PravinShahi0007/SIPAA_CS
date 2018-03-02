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
    class IncExtraSuspRetro
    {        
        public DataTable ExtraSuspRetroxTrabajador(int iOpcion, string sIdTrab, DateTime sfereget, int iCvIncidencia, int iCvTipo, 
            int iCvIncidencia2, int iCvTipo2, DateTime sFeIni, DateTime sFeFin, string spusuumod, string spprgumod, int iAplica)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtinccaptura_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();

            cmd.Parameters.Add("@P_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sIdTrab;
            cmd.Parameters.Add("@P_fecharegistro", SqlDbType.DateTime).Value = sfereget;
            cmd.Parameters.Add("@P_cvincidencia", SqlDbType.Int).Value = iCvIncidencia;
            cmd.Parameters.Add("@P_cvtipo", SqlDbType.Int).Value = iCvTipo;
            cmd.Parameters.Add("@P_cvincidencia2", SqlDbType.Int).Value = iCvIncidencia2;
            cmd.Parameters.Add("@P_cvtipo2", SqlDbType.Int).Value = iCvTipo2;
            cmd.Parameters.Add("@P_fechainicio", SqlDbType.DateTime).Value = sFeIni;
            cmd.Parameters.Add("@P_fechafin", SqlDbType.DateTime).Value = sFeFin;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = spusuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = spprgumod;
            cmd.Parameters.Add("@P_aplica", SqlDbType.Int).Value = iAplica;
                        
            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();
            DataTable dt = new DataTable();
            Adapter.Fill(dt);

            return (dt);
        }
    }
}
