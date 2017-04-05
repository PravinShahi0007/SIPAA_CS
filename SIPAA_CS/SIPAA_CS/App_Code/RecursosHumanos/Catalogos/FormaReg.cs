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

namespace SIPAA_CS.App_Code
{
    class FormaReg
    {

        //SE DECLARAN VARIABLES
        public int p_opcion;
        public int p_cvforma;
        public string p_descripcion;
        public int p_stforma;
        public string p_usuumod;
        public string p_prgumodr;
        public int p_respuesta;

        //CONTRUCTOR
        public FormaReg()
        {

            p_opcion = 0;
            p_cvforma = 0;
            p_descripcion = "";
            p_stforma = 0;
            p_usuumod = "";
            p_prgumodr = "";
            p_respuesta = 0;
        }

        //METODO FORMAS DE REGISTRO LLENA GRID
        public DataTable FormaReg_S(int p_opcion, int p_cvforma, string p_descripcion, int p_stforma, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechformareg_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvforma", SqlDbType.VarChar).Value = p_cvforma;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_stforma", SqlDbType.VarChar).Value = p_stforma;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable FormaReg_S = new DataTable();
            Adapter.Fill(FormaReg_S);
            return FormaReg_S;

        }
        //metodo actualizar, insertar, eliminar registro
        public int formaReg_UID(int p_opcion, int p_cvforma, string p_descripcion, int p_stforma, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechformareg_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvforma", SqlDbType.VarChar).Value = p_cvforma;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_stforma", SqlDbType.VarChar).Value = p_stforma;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iResponse;
        }
        //METODO FORMAS DE REGISTRO VALIDA CREA REGISTRO
        public int FormaReg_V(int p_opcion, int p_cvforma, string p_descripcion, int p_stforma, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechformareg_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvforma", SqlDbType.VarChar).Value = p_cvforma;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_stforma", SqlDbType.VarChar).Value = p_stforma;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iResponse;
        }


        public List<string> FormasxUsuario(string sIdTrab,int iCvforma,int iOpcion,string sUsuumod,string sPrgmod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtrabforeg_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@p_cvforma", SqlDbType.Int).Value = iCvforma;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = sUsuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sPrgmod;
            cmd.Parameters.Add("@p_idtrab", SqlDbType.VarChar).Value = sIdTrab;
           

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtForReg = new DataTable();
            Adapter.Fill(dtForReg);
            List<string> ltCVforma = new List<string>();
            foreach(DataRow row in dtForReg.Rows)
            {
                ltCVforma.Add(row["cvforma"].ToString());
            }

            return ltCVforma;

        }
    }
}
