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

namespace SIPAA_CS.App_Code
{
    class DiaFestivo
    {
        //variables
        public int iOpcion;
        public string sFecha;
        public string sDescripcion;
        public string sUsuumod;
        public string sPrgumod;
        public int iRespuesta;
        public int iRespuestaValidacion;

        public DiaFestivo()
        {
            //inician variables
            iOpcion = 0;
            sFecha = "";
            sDescripcion = "";
            sUsuumod = "";
            sPrgumod = "";
            iRespuesta = 0;
            iRespuestaValidacion = 0;
        } // public DiasFestivos()

        //metodo data table para llenar grid de busqueda
        public DataTable obtdiasfestivos(int iOpcion, string sFecha, string sDescripcion, string sUsuumod, string sPrgumod)
        {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"usp_rechcdfestivo_suid";
                cmd.CommandType = CommandType.StoredProcedure;
                Conexion objConexion = new Conexion();
                objConexion.asignarConexion(cmd);

                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
                cmd.Parameters.Add("@p_fecha", SqlDbType.VarChar).Value = sFecha;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sDescripcion;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = sUsuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sPrgumod;

                objConexion.asignarConexion(cmd);

                SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

                objConexion.cerrarConexion();

                DataTable dtdiasfestivos = new DataTable();
                Adapter.Fill(dtdiasfestivos);
                return dtdiasfestivos;
        } // public DataTable obtdiasfestivos(int p_opcion, string p_fecha, string p_descripcion, string p_usuumod, string p_prgumodr)

        //metodo insertar, actualizar, eliminar registro, listar x descripción, listar x fecha
        public int udidiasfestivos(int iOpcion, string sFecha, string sDescripcion, string sUsuumod, string sPrgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechcdfestivo_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);
            //
            // Valida que no exista el registro (Valida Llave duplicada - Fecha)
            if (iOpcion == 1)
            {
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = 5; // búsqueda de registro por fecha
                cmd.Parameters.Add("@p_fecha", SqlDbType.VarChar).Value = sFecha;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sDescripcion;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = sUsuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sPrgumod;
                objConexion.asignarConexion(cmd);
                //
                iRespuesta = Convert.ToInt32(cmd.ExecuteScalar());
                if (iRespuesta > 0)
                {
                    iRespuestaValidacion = 99; // Intento de duplicar un registro
                }
            }
            if(iRespuesta == 0)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
                cmd.Parameters.Add("@p_fecha", SqlDbType.VarChar).Value = sFecha;
                cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = sDescripcion;
                cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = sUsuumod;
                cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sPrgumod;
                objConexion.asignarConexion(cmd);
                //
                iRespuesta = Convert.ToInt32(cmd.ExecuteScalar());
                //
                objConexion.cerrarConexion();
            }
            if(iRespuestaValidacion == 99)
            {
                iRespuesta = iRespuestaValidacion;
            }
            //
            objConexion.cerrarConexion();
            //
            return iRespuesta;
        } // public int uditipohr(int p_opcion, int p_fecha, string p_descripcion, string p_usuumod, string p_prgumodr)

    } // class DiasFestivos
} // namespace SIPAA_CS.Recursos_Humanos.App_Code
