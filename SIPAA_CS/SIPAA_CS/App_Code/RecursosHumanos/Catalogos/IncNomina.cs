using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación: 15-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra incidencias de nomina
//***********************************************************************************************

namespace SIPAA_CS.App_Code
{
    class IncNomina
    {
        //variables
        public int iopcion;
        public int icvincidencia;
        public string sdescripcion;
        public string susuumod;
        public string sprgumodr;
        public int irespuesta;
        public int iverifpk;

        public IncNomina()
        {
            //inician variables
            iopcion = 0;
            icvincidencia = 0;
            sdescripcion = "";
            susuumod = "";
            sprgumodr = "";
            irespuesta = 0;
            iverifpk = 0;
        }

        //metodo llenar combo incidecnia
        public DataTable cboInc(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincidencia_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_orden", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stgenera", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_strepresenta", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stincidencia", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable incnomin = new DataTable();
            Adapter.Fill(incnomin);
            return incnomin;
        }
        //metodo llenar combo tipo horario
        public DataTable cboTipoHr(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechtipohr_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable TipoHr = new DataTable();
            Adapter.Fill(TipoHr);
            return TipoHr;
        }

        //metodo llenar combo combo estatus director, pasa nomina, premio
        public DataTable cboEsNoPr(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stdir", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stpremio", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_cvafecta", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stpasanom", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_campo", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_formula", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_stincnomina", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable EsNoPr = new DataTable();
            Adapter.Fill(EsNoPr);
            return EsNoPr;
        }

        //metodo llenar combo dias u horas
        public DataTable cboPeriodoTipo(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonaformapago_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_IdFormaPago", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_Descripcion", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable PeriodoTipo = new DataTable();
            Adapter.Fill(PeriodoTipo);
            return PeriodoTipo;
        }

        //metodo llenar combo afecta
        public DataTable cboAfec(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonaConcAfec_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_IdAfecta", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_Descripcion", SqlDbType.VarChar).Value = "";

            //objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable Afec = new DataTable();
            Adapter.Fill(Afec);
            return Afec;
        }


        //metodo data table para llenar grid de busqueda
        public DataTable obtincnomina(int iopcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_cvrepresenta", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stdir", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stpremio", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_cvafecta", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stpasanom", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_imphrsdias", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_campo", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_formula", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable gridbusoncnom = new DataTable();
            Adapter.Fill(gridbusoncnom);
            return gridbusoncnom;
        }

        //bsuqueda de registro por cvincidencia y cvrepresenta
        public DataTable obtincnominair(int iopcion, int icvincidencia)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = icvincidencia;
            cmd.Parameters.Add("@p_stdir", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stpremio", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_cvafecta", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_stpasanom", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_campo", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_formula", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_stincnomina", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable gridbusoncnomir = new DataTable();
            Adapter.Fill(gridbusoncnomir);
            return gridbusoncnomir;
        }

        //metodo insertar, actualizar, eliminar registro
        public int rechincnominasuid(int iopcion, int icvincidencia, int istdir, int iidformapago, 
                             int istpremio, int icvafecta, int istpasanom, int icvtipohr, string scampo,
                             string sformula, int istincnomina, string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = icvincidencia;
            cmd.Parameters.Add("@p_stdir", SqlDbType.Int).Value = istdir;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iidformapago;
            cmd.Parameters.Add("@p_stpremio", SqlDbType.Int).Value = istpremio;
            cmd.Parameters.Add("@p_cvafecta", SqlDbType.Int).Value = icvafecta;
            cmd.Parameters.Add("@p_stpasanom", SqlDbType.Int).Value = istpasanom;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.Int).Value = icvtipohr;
            cmd.Parameters.Add("@p_campo", SqlDbType.VarChar).Value = scampo;
            cmd.Parameters.Add("@p_formula", SqlDbType.VarChar).Value = sformula;
            cmd.Parameters.Add("@p_stincnomina", SqlDbType.Int).Value = istincnomina;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);

            int p_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return p_respuesta;

        }

        //verifica llave primaria
        public int rechincnominapk(int iopcion, int icvincidencia, int istdir, int iidformapago,
                             int istpremio, int icvafecta, int istpasanom, int icvtipohr, string scampo,
                             string sformula, int istincnomina, string susuumod, string sprgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = iopcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = icvincidencia;
            cmd.Parameters.Add("@p_stdir", SqlDbType.Int).Value = istdir;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = iidformapago;
            cmd.Parameters.Add("@p_stpremio", SqlDbType.Int).Value = istpremio;
            cmd.Parameters.Add("@p_cvafecta", SqlDbType.Int).Value = icvafecta;
            cmd.Parameters.Add("@p_stpasanom", SqlDbType.Int).Value = istpasanom;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.Int).Value = icvtipohr;
            cmd.Parameters.Add("@p_campo", SqlDbType.VarChar).Value = scampo;
            cmd.Parameters.Add("@p_formula", SqlDbType.VarChar).Value = sformula;
            cmd.Parameters.Add("@p_stincnomina", SqlDbType.Int).Value = istincnomina;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = susuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = sprgumod;
            objConexion.asignarConexion(cmd);

            iverifpk = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifpk;

        }
    }
}
