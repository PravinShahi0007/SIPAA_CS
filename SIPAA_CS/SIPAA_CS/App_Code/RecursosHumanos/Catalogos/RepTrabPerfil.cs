using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: Marco Dupont
//Fecha creación: 13-Abr-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Reporte Perfil Trabajador
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class RepTrabPerfil
    {
        //SE DECLARAN VARIABLES
        public int p_opcion;
        public int p_idtrab;
        public int p_idcompania;
        public int p_idubicacion;
        public int p_Activo;

        //CONTRUCTOR
        public RepTrabPerfil()
        {
            p_opcion = 0;
            p_idtrab = 0;
            p_idcompania = 0;
            p_idubicacion = 0;
            p_Activo = 0;
        }
        //METODO REPORTE PERFIL TRABAJADOR
        public DataTable PerfilTrab_S(int p_opcion, int p_idtrab, int p_idcompania, int p_idubicacion, int p_Activo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechrtrabperfil_S";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.Int).Value = p_idtrab;
            cmd.Parameters.Add("@p_idcompania", SqlDbType.Int).Value = p_idcompania;
            cmd.Parameters.Add("@p_idubicacion", SqlDbType.Int).Value = p_idubicacion;
            cmd.Parameters.Add("@p_Activo", SqlDbType.Int).Value = p_Activo;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable PerfilTrab_S = new DataTable();
            Adapter.Fill(PerfilTrab_S);
            return PerfilTrab_S;
        }

    }
}
