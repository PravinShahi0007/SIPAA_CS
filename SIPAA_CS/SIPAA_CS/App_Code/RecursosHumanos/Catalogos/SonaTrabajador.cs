using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIPAA_CS.App_Code
{
    class SonaTrabajador
    {
        //declaracion de variables
        public int popcion;
        public string prespuesta;
        public string ptextoabuscar;

        public SonaTrabajador()
        {
            popcion = 0;
            prespuesta = "";
            ptextoabuscar = "";
        }

        //se crea un datatable (trae los registros de la BD
        //del tipo DT el metodo se llama obtenerempleados y recibe una opcion y un texto a buscar
        public DataTable obtenerempleados(int popcion, string ptextoabuscar)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_trabajador_s"; 
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            //SqlConnection sqlcn = objConexion.coconexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value=ptextoabuscar;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtEmpleados = new DataTable();
            dadapter.Fill(dtEmpleados);
            return (dtEmpleados);
        }


        public DataTable obtenerempleadosxfiltros(int popcion, string pidtrab, string pidcompania, string pidarea, string pidpuesto, 
            string piddepartamento, string pidubicacion, string pidtiponomina, int pactivo)
        {
            //Filtros Multiples JLA 21 sep 2017
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_trabajador_fm";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            //SqlConnection sqlcn = objConexion.conexionSonarh();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_IdTrab", SqlDbType.VarChar).Value = pidtrab;
            cmd.Parameters.Add("@p_IdCompania", SqlDbType.VarChar).Value = pidcompania;
            cmd.Parameters.Add("@p_IdArea", SqlDbType.VarChar).Value = pidarea;
            cmd.Parameters.Add("@p_IdPuesto", SqlDbType.VarChar).Value = pidpuesto;
            cmd.Parameters.Add("@p_IdDepartamento", SqlDbType.VarChar).Value = piddepartamento;
            cmd.Parameters.Add("@p_IdUbicacion", SqlDbType.VarChar).Value = pidubicacion;
            cmd.Parameters.Add("@p_IdTipoNomina", SqlDbType.VarChar).Value = pidtiponomina;
            cmd.Parameters.Add("@p_Activo", SqlDbType.Int).Value = pactivo;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtEmpleados = new DataTable();
            dadapter.Fill(dtEmpleados);
            return (dtEmpleados);
        }

        public DataTable obtenerempleadosydiasesp(/*int popcion, */string pidtrab, string pidcompania, string pidarea, string pidpuesto,
            string piddepartamento, string pidubicacion, string pidtiponomina, string pfechainicial, string pfechafinal, int pactivo)
        {
            //Filtros Multiples JLA 21 sep 2017
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_trabajadordiasesp_s";
            cmd.CommandType = CommandType.StoredProcedure;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            //cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = popcion;
            cmd.Parameters.Add("@p_IdTrab", SqlDbType.VarChar).Value = pidtrab;
            cmd.Parameters.Add("@p_IdCompania", SqlDbType.VarChar).Value = pidcompania;
            cmd.Parameters.Add("@p_IdArea", SqlDbType.VarChar).Value = pidarea;
            cmd.Parameters.Add("@p_IdPuesto", SqlDbType.VarChar).Value = pidpuesto;
            cmd.Parameters.Add("@p_IdDepartamento", SqlDbType.VarChar).Value = piddepartamento;
            cmd.Parameters.Add("@p_IdUbicacion", SqlDbType.VarChar).Value = pidubicacion;
            cmd.Parameters.Add("@p_IdTipoNomina", SqlDbType.VarChar).Value = pidtiponomina;
            cmd.Parameters.Add("@p_FechaInicial", SqlDbType.VarChar).Value = pfechainicial;
            cmd.Parameters.Add("@p_FechaFinal", SqlDbType.VarChar).Value = pfechafinal;
            cmd.Parameters.Add("@p_Activo", SqlDbType.Int).Value = pactivo;

            objConexion.asignarConexion(cmd);
            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();

            DataTable dtEmpleadosDiasEsp = new DataTable();
            dadapter.Fill(dtEmpleadosDiasEsp);
            return (dtEmpleadosDiasEsp);
        }

        public DataTable ObtenerPerfilTrabajador(string sIdtrab,int iOpcion,string sCheca,string sEstatus,int iCvtipohr,string sUsuumod,string sPrgumod)
        {
            SqlCommand cmd = new SqlCommand();
            Conexion objConexion = new Conexion();
            Usuario objusuario = new Usuario();
            
            cmd.CommandText = "usp_rechtrabperfil_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sIdtrab;
            cmd.Parameters.Add("@P_opcion", SqlDbType.VarChar).Value = iOpcion;
            cmd.Parameters.Add("@P_checa", SqlDbType.VarChar).Value = sCheca;
            cmd.Parameters.Add("@P_activo", SqlDbType.VarChar).Value = sEstatus;
            cmd.Parameters.Add("@P_cvtipohr", SqlDbType.Int).Value = iCvtipohr;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = sUsuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = sPrgumod;
            
            objConexion.asignarConexion(cmd);
            
            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtTrabajador = new DataTable();
            dadapter.Fill(dtTrabajador);
            return (dtTrabajador);
        }
        //Fecha de Modificación: 26-Abr-2017
        //Autor: Marco Dupont
        //Descripción: Lista de Trabajadores por Esttus
        public DataTable ObtenerListaTrabajador(int iOpcion, int iSt)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonatrabajador_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@Nom", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@Sta", SqlDbType.Int).Value = iSt;
            cmd.Parameters.Add("@Enc", SqlDbType.Int).Value = 0;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtLisTrab = new DataTable();
            Adapter.Fill(dtLisTrab);
            return (dtLisTrab);
        }



        public DataTable GestionHuella(string sIdtrab,string huellaTmp,int iHuella,string sUsuumod,string sPrgmod,int iOpcion)
        {

            SqlCommand cmd = new SqlCommand();
            Conexion objConexion = new Conexion();
            Usuario objusuario = new Usuario();

            

            cmd.CommandText = "usp_rechchuellatrab_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sIdtrab;
            cmd.Parameters.Add("@P_huellaTmp", SqlDbType.NVarChar).Value = huellaTmp;
            cmd.Parameters.Add("@P_indicehuella", SqlDbType.Int).Value = iHuella;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = sUsuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = sPrgmod;
            cmd.Parameters.Add("@P_opcion", SqlDbType.Int).Value = iOpcion;

 
            objConexion.asignarConexion(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtTrabajador = new DataTable();
            dadapter.Fill(dtTrabajador);
            return (dtTrabajador);


        }


        public DataTable GestionIdentidad(string sIdtrab,string sPass, string RostroTmp, string rostrolong, string sUsuumod, string sPrgmod, int iOpcion)
        {

            SqlCommand cmd = new SqlCommand();
            Conexion objConexion = new Conexion();
            Usuario objusuario = new Usuario();



            cmd.CommandText = "usp_rechctrabidentidad_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@P_idtrab", SqlDbType.VarChar).Value = sIdtrab;
            cmd.Parameters.Add("@P_pass", SqlDbType.NVarChar).Value = sPass;
            cmd.Parameters.Add("@P_rostroTmp", SqlDbType.NVarChar).Value = RostroTmp;
            cmd.Parameters.Add("@P_rostrolong", SqlDbType.Int).Value =Convert.ToInt32( rostrolong);
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = sUsuumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = sPrgmod;
            cmd.Parameters.Add("@P_opcion", SqlDbType.Int).Value = iOpcion;
           


            objConexion.asignarConexion(cmd);

            SqlDataAdapter dadapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtTrabajador = new DataTable();
            dadapter.Fill(dtTrabajador);
            return (dtTrabajador);


        }
        public DataTable ObtenerRegistroDetalle(string sidtrab, string dtfechainicio, string dtfechafin, string scompania, string subicacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechregistrodetalle_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();

            cmd.Parameters.Add("@p_idtrab", SqlDbType.VarChar).Value = sidtrab;
            cmd.Parameters.Add("@P_fechainicio", SqlDbType.VarChar).Value = dtfechainicio;
            cmd.Parameters.Add("@P_fechafin", SqlDbType.VarChar).Value = dtfechafin;
            cmd.Parameters.Add("@P_Compania", SqlDbType.VarChar).Value = scompania;
            cmd.Parameters.Add("@P_Ubicacion", SqlDbType.VarChar).Value = subicacion;

            objConexion.asignarConexion(cmd);


            SqlDataAdapter daRegistroDetalle = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();
            DataTable dtRegistroDetalle = new DataTable();
            daRegistroDetalle.Fill(dtRegistroDetalle);
            return (dtRegistroDetalle);
        }

        public DataTable MasDe3Faltas(int opcion, string sIdTrab, string sCompania, string sUbicacion, DateTime dtFechaBase)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechmasde3faltas_S";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = opcion;
            cmd.Parameters.Add("@p_IdTrab", SqlDbType.VarChar).Value = sIdTrab;
            cmd.Parameters.Add("@P_FechaBase", SqlDbType.DateTime).Value = dtFechaBase;
            cmd.Parameters.Add("@P_Compania", SqlDbType.VarChar).Value = sCompania;
            cmd.Parameters.Add("@P_Ubicacion", SqlDbType.VarChar).Value = sUbicacion;

            objConexion.asignarConexion(cmd);


            SqlDataAdapter daRegistroDetalle = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();
            DataTable dtRegistroDetalle = new DataTable();
            daRegistroDetalle.Fill(dtRegistroDetalle);
            return (dtRegistroDetalle);
        }


        public DataTable Relojchecador(string sidtrab,int iOpcion,DateTime dtFechaRegistro, int iTipoCheck ,TimeSpan tpHoraRegistro,int iCvReloj
                                       ,int iTraspaso, string sUsumod,string sPrgmod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechregistro_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();

            cmd.Parameters.Add("@p_idtrab", SqlDbType.VarChar).Value = sidtrab;
            cmd.Parameters.Add("@P_Opcion", SqlDbType.Int).Value = iOpcion;
            cmd.Parameters.Add("@P_feregistro", SqlDbType.DateTime).Value = dtFechaRegistro;
            cmd.Parameters.Add("@P_cvtipoasistencia", SqlDbType.Int).Value = iTipoCheck;
            cmd.Parameters.Add("@P_horaregistro", SqlDbType.Time).Value = tpHoraRegistro;
            cmd.Parameters.Add("@P_cvreloj", SqlDbType.Int).Value = iCvReloj;
            cmd.Parameters.Add("@P_traspaso", SqlDbType.Int).Value = iTraspaso;
            cmd.Parameters.Add("@P_usuumod", SqlDbType.VarChar).Value = sUsumod;
            cmd.Parameters.Add("@P_prgumod", SqlDbType.VarChar).Value = sPrgmod;

            objConexion.asignarConexion(cmd);
     
            SqlDataAdapter daRegistroDetalle = new SqlDataAdapter(cmd);
            objConexion.cerrarConexion();
            DataTable dt = new DataTable();
            daRegistroDetalle.Fill(dt);
            return (dt);
        }

    }
}
