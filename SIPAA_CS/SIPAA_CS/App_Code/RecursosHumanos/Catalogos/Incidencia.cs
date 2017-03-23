using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPAA_CS.Conexiones;

namespace SIPAA_CS.App_Code.RecursosHumanos
{
    class Incidencia
    {
        //SE DECLARAN VARIABLES
        public int p_opcion;
        public int CVIncidencia;
        public string Descripcion;
        public int p_orden;
        public int CVRepresenta;
        public string Representa;
        public string UsuuMod;
        public DateTime FhuMod;
        public string PrguMod;
        public int TipoID;
        public string TipoIncidencia;
        public int p_respuesta;

        //CONTRUCTOR
        public Incidencia()
        {
            p_opcion = 0;
            CVIncidencia = 0;
            Descripcion = "";
            p_orden = 0;
            UsuuMod = "";
            PrguMod = "";
            p_respuesta = 0;
        }


        public DataTable ObtenerRepresentaxIncidencia(Incidencia objIncidencia,int Opcion) {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincapacidadrepresenta_SUID";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_incidencia", SqlDbType.VarChar).Value = objIncidencia.Descripcion;
            cmd.Parameters.Add("P_representa", SqlDbType.VarChar).Value = objIncidencia.Representa;
            cmd.Parameters.Add("cvrepresenta", SqlDbType.Int).Value = objIncidencia.CVRepresenta;
            cmd.Parameters.Add("Opcion", SqlDbType.VarChar).Value = Opcion;
            cmd.Parameters.Add("usuumod", SqlDbType.VarChar).Value = objIncidencia.UsuuMod;
            cmd.Parameters.Add("prgumod", SqlDbType.VarChar).Value = objIncidencia.PrguMod;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);

            return dtIncidencia;
        }


        public DataTable ObtenerIncidenciaxTipo(Incidencia objIncidencia, int Opcion,string TipoAnterior)
        {

            Conexion objConexion = new Conexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtipoincidencia_suid";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_incidencia", SqlDbType.VarChar).Value = objIncidencia.Descripcion;
            cmd.Parameters.Add("P_tipo", SqlDbType.VarChar).Value = objIncidencia.TipoIncidencia;
            cmd.Parameters.Add("P_tipoAnterior", SqlDbType.VarChar).Value = objIncidencia.TipoIncidencia;
            cmd.Parameters.Add("P_Opcion", SqlDbType.Int).Value = Opcion;
            cmd.Parameters.Add("usuumod", SqlDbType.VarChar).Value = objIncidencia.UsuuMod;
            cmd.Parameters.Add("prgumod", SqlDbType.VarChar).Value = objIncidencia.PrguMod;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtIncidencia = new DataTable();
            Adapter.Fill(dtIncidencia);

            return dtIncidencia;
        }
        //METODO FORMAS DE REGISTRO LLENA GRID MTD
        public DataTable Incidencia_S(int p_opcion, int p_cvincidencia, string p_descripcion, int p_orden, string p_usuumod, string p_prgumodr)
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

            DataTable Incidencia_S = new DataTable();
            Adapter.Fill(Incidencia_S);
            return Incidencia_S;

        }
        //metodo actualizar, insertar, eliminar registro MTD
        public int Incidencia_UID(int p_opcion, int p_cvincidencia, string p_descripcion, int p_orden, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechformareg_suid";
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
        public int Incidencia_V(int p_opcion, int p_cvincidencia, string p_descripcion, int p_orden, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechformareg_suid";
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
