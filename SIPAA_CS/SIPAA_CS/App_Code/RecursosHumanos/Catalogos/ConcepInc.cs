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
//Fecha creación: 17-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra Formas de Registro Trabajador
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Catalogos
{
    class ConcepInc
    {
        //SE DECLARAN VARIABLES
        public int p_opcion;
        public int p_cvincidencia;
        public string p_descripcion;
        public int p_orden;
        public string p_usuumod;
        public string p_prgumodr;
        public int p_respuesta;

        //CONTRUCTOR
        public ConcepInc()
        {

            p_opcion = 0;
            p_cvincidencia = 0;
            p_descripcion = "";
            p_orden = 0;
            p_usuumod = "";
            p_prgumodr = "";
            p_respuesta = 0;
        }

        //METODO FORMAS DE REGISTRO LLENA GRID
        public DataTable ConcepInc_S(int p_opcion, int p_cvincidencia, string p_descripcion, int p_orden, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincidencia_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = p_cvincidencia;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_orden", SqlDbType.Int).Value = p_orden;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable ConcepInc_S = new DataTable();
            Adapter.Fill(ConcepInc_S);
            return ConcepInc_S;

        }
        //metodo actualizar, insertar, eliminar registro MTD
        public int ConcepInc_UID(int p_opcion, int p_cvincidencia, string p_descripcion, int p_orden, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincidencia_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = p_cvincidencia;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_orden", SqlDbType.Int).Value = p_orden;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iResponse;
        }
        //METODO FORMAS DE REGISTRO VALIDA CREA REGISTRO MTD
        public int ConcepInc_V(int p_opcion, int p_cvincidencia, string p_descripcion, int p_orden, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincidencia_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = p_cvincidencia;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_orden", SqlDbType.Int).Value = p_orden;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iResponse;
        }
    }
}
