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
    class ArchNomina4
    {
        //se declaran variables

        public string spdescripcion = "";
        public string sprespuesta = "";
        public int ipopcion = 0;
        public int ipidtrab = 0;
        public int ipidcompania = 0;
        public int ipidtiponomina = 0;
        public int ipidubicacion = 0;
        public string spfecinicio = "";
        public string spfecfin = "";
        public string spusuumod = "";
        public string spprgumod = "";
        
        //Metodo data table para el llenado del grid
        public DataTable ObtenerArchivoNomina4(int ipopcion, int ipidtrab, int ipidcompania, int ipidtiponomina, int ipidubicacion,
            string spfecinicio, string spfecfin, string spusuumod, string spprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_archivonomina4_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = ipopcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = ipidtrab;
            cmd.Parameters.Add("@p_idcompania", SqlDbType.Int).Value = ipidcompania;
            cmd.Parameters.Add("@p_idtiponomina", SqlDbType.VarChar).Value = ipidtiponomina;
            cmd.Parameters.Add("@p_idubicacion", SqlDbType.VarChar).Value = ipidubicacion;
            cmd.Parameters.Add("@p_fecinicio", SqlDbType.VarChar).Value = spfecinicio;
            cmd.Parameters.Add("@p_fecfin", SqlDbType.VarChar).Value = spfecfin;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = spusuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = spprgumod;
            objConexion.asignarConexion(cmd);

            //Ejecutar el SP con el SqlAdapter
            SqlDataAdapter daarchivonomina4 = new SqlDataAdapter(cmd);

            //Cerrar conexion
            objConexion.cerrarConexion();
            DataTable dtarchivonomina4 = new DataTable();
            daarchivonomina4.Fill(dtarchivonomina4);
            return dtarchivonomina4;
        }
    }
}
