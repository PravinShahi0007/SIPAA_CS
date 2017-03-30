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
//Fecha creación: 29-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra Valores de los Status
//***********************************************************************************************

namespace SIPAA_CS.App_Code.Generales
{
    class status
    {
        //SE DECLARAN VARIABLES
        public int p_opcion;
        public string p_cvtabla;
        public int p_stvalor;
        public string p_descripcion;
        public int p_ststatus;
        public string p_usuumod;
        public string p_prgumodr;
        public int p_respuesta;

        //CONTRUCTOR
        public status()
        {
            p_opcion = 0;
            p_cvtabla = "";
            p_stvalor = 0;
            p_descripcion = "";
            p_ststatus = 0;
            p_usuumod = "";
            p_prgumodr = "";
            p_respuesta = 0;
        }
        //METODO FORMAS DE REGISTRO LLENA GRID
        public DataTable status_S(int p_opcion, string p_cvtabla, int p_stvalor, string p_descripcion, int p_ststatus, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_genestatus_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvtabla", SqlDbType.VarChar).Value = p_cvtabla;
            cmd.Parameters.Add("@p_stvalor", SqlDbType.VarChar).Value = p_stvalor;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_ststatus", SqlDbType.VarChar).Value = p_ststatus;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable status_S = new DataTable();
            Adapter.Fill(status_S);
            return status_S;

        }
        //metodo actualizar, insertar, eliminar registro
        public int status_UID(int p_opcion, string p_cvtabla, int p_stvalor, string p_descripcion, int p_ststatus, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_genestatus_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvtabla", SqlDbType.VarChar).Value = p_cvtabla;
            cmd.Parameters.Add("@p_stvalor", SqlDbType.VarChar).Value = p_stvalor;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_ststatus", SqlDbType.VarChar).Value = p_ststatus;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iResponse;
        }
        //METODO FORMAS DE REGISTRO VALIDA CREA REGISTRO
        public int status_V(int p_opcion, string p_cvtabla, int p_stvalor, string p_descripcion, int p_ststatus, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_genestatus_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvtabla", SqlDbType.VarChar).Value = p_cvtabla;
            cmd.Parameters.Add("@p_stvalor", SqlDbType.VarChar).Value = p_stvalor;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_ststatus", SqlDbType.VarChar).Value = p_ststatus;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iResponse;
        }
    }
}
