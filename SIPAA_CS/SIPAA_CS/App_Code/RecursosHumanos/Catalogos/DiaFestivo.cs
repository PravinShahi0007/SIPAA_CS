using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: Benjamin Huizar Barajas
//Fecha creación: 15-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra Días Festivos
//***********************************************************************************************

namespace SIPAA_CS.Recursos_Humanos.App_Code
{
    class DiaFestivo
    {
        //variables
        public int p_opcion;
        public string p_fecha;
        public string p_descripcion;
        public string p_usuumod;
        public string p_prgumodr;
        public int p_respuesta;

        public DiaFestivo()
        {
            //inician variables
            p_opcion = 0;
            p_fecha = "";
            p_descripcion = "";
            p_usuumod = "";
            p_prgumodr = "";
            p_respuesta = 0;
        } // public DiasFestivos()

        //metodo data table para llenar grid de busqueda
        public DataTable obtdiasfestivos(int p_opcion, string p_fecha, string p_descripcion, string p_usuumod, string p_prgumodr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechcdfestivo_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_fecha", SqlDbType.VarChar).Value = p_fecha;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtdiasfestivos = new DataTable();
            Adapter.Fill(dtdiasfestivos);
            return dtdiasfestivos;
        } // public DataTable obtdiasfestivos(int p_opcion, string p_fecha, string p_descripcion, string p_usuumod, string p_prgumodr)

        //metodo insertar, actualizar, eliminar registro, listar x descripción, listar x fecha
        public int udidiasfestivos(int p_opcion, string p_fecha, string p_descripcion, string p_usuumod, string p_prgumodr)
        {
            int p_respuesta = 0;
            int p_respuestaValidacion = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechcdfestivo_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
            //
            // Valida que no exista el registro (Valida Llave duplicada - Fecha)
            if (p_opcion == 1)
            {
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = 5; // búsqueda de registro por fecha
                cmd.Parameters.Add("@p_fecha", SqlDbType.VarChar).Value = p_fecha;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;
                objConexion.asignarConexion(cmd);
                //
                p_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
                if (p_respuesta>0)
                {
                    p_respuestaValidacion = 99; // Intento de duplicar un registro
                }
                //
                //objConexion.cerrarConexion();
            }
            if(p_respuesta==0)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
                cmd.Parameters.Add("@p_fecha", SqlDbType.VarChar).Value = p_fecha;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = p_descripcion;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumodr;
                objConexion.asignarConexion(cmd);
                //
                p_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
                //
                objConexion.cerrarConexion();
            }
            if(p_respuestaValidacion == 99)
            {
                p_respuesta = p_respuestaValidacion;
            }
            //
            objConexion.cerrarConexion();
            //
            return p_respuesta;
        } // public int uditipohr(int p_opcion, int p_fecha, string p_descripcion, string p_usuumod, string p_prgumodr)

    } // class DiasFestivos
} // namespace SIPAA_CS.Recursos_Humanos.App_Code
