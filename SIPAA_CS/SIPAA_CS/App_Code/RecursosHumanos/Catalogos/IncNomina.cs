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
        public int p_opcion;
        public int p_cvincidencia;
        public string p_descripcion;
        public string p_usuumod;
        public string p_prgumodr;
        public int p_respuesta;
        public int iverifpk;

        public IncNomina()
        {
            //inician variables
            p_opcion = 0;
            p_cvincidencia = 0;
            p_descripcion = "";
            p_usuumod = "";
            p_prgumodr = "";
            p_respuesta = 0;
            iverifpk = 0;
        }

        //metodo llenar combo incidecnia
        public DataTable cboInc(int p_opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincidencia_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_descripcion", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@p_orden", SqlDbType.Int).Value = 0;
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
        public DataTable cboTipoHr(int p_opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechctipohr_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
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
        public DataTable cboEsNoPr(int p_opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@p_cvrepresenta", SqlDbType.Int).Value = 0;
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
        public DataTable cboPeriodoTipo(int p_opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonaformapago_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
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
        public DataTable cboAfec(int p_opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_sonaConcAfec_s";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;

            //objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable Afec = new DataTable();
            Adapter.Fill(Afec);
            return Afec;
        }


        //metodo data table para llenar grid de busqueda
        public DataTable obtincnomina(int p_opcion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
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
        public DataTable obtincnominair(int p_opcion, int p_cvincidencia, int p_cvrepresenta)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = p_cvincidencia;
            cmd.Parameters.Add("@p_cvrepresenta", SqlDbType.Int).Value = p_cvrepresenta;
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

        // combo representa "tabla rechcincrep", "usp_rechincapacidadrepresenta_SUID"
        public DataTable cboRep(int p_opcion, string p_incidencia)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincapacidadrepresenta_SUID";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@P_incidencia", SqlDbType.VarChar).Value = p_incidencia;
            cmd.Parameters.Add("@P_representa", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@cvrepresenta", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@Opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@usuumod", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@prgumod", SqlDbType.VarChar).Value = "";

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable cborepresenta = new DataTable();
            Adapter.Fill(cborepresenta);
            return cborepresenta;
        }

        //metodo insertar, actualizar, eliminar registro
        public int rechincnominasuid(int p_opcion, int p_cvincidencia, int p_cvrepresenta, int p_stdir, int p_idformapago, 
                             int p_stpremio, int p_cvafecta, int p_stpasanom, int p_cvtipohr, string p_campo,
                             string p_formula, int p_stincnomina, string p_usuumod, string p_prgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = p_cvincidencia;
            cmd.Parameters.Add("@p_cvrepresenta", SqlDbType.Int).Value = p_cvrepresenta;
            cmd.Parameters.Add("@p_stdir", SqlDbType.Int).Value = p_stdir;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = p_idformapago;
            cmd.Parameters.Add("@p_stpremio", SqlDbType.Int).Value = p_stpremio;
            cmd.Parameters.Add("@p_cvafecta", SqlDbType.Int).Value = p_cvafecta;
            cmd.Parameters.Add("@p_stpasanom", SqlDbType.Int).Value = p_stpasanom;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.Int).Value = p_cvtipohr;
            cmd.Parameters.Add("@p_campo", SqlDbType.VarChar).Value = p_campo;
            cmd.Parameters.Add("@p_formula", SqlDbType.VarChar).Value = p_formula;
            cmd.Parameters.Add("@p_stincnomina", SqlDbType.Int).Value = p_stincnomina;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumod;
            objConexion.asignarConexion(cmd);

            int p_respuesta = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return p_respuesta;

        }

        //verifica llave primaria
        public int rechincnominapk(int p_opcion, int p_cvincidencia, int p_cvrepresenta, int p_stdir, int p_idformapago,
                             int p_stpremio, int p_cvafecta, int p_stpasanom, int p_cvtipohr, string p_campo,
                             string p_formula, int p_stincnomina, string p_usuumod, string p_prgumod)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechincnomina_suid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            cmd.Parameters.Add("@p_opcion", SqlDbType.Int).Value = p_opcion;
            cmd.Parameters.Add("@p_cvincidencia", SqlDbType.Int).Value = p_cvincidencia;
            cmd.Parameters.Add("@p_cvrepresenta", SqlDbType.Int).Value = p_cvrepresenta;
            cmd.Parameters.Add("@p_stdir", SqlDbType.Int).Value = p_stdir;
            cmd.Parameters.Add("@p_idformapago", SqlDbType.Int).Value = p_idformapago;
            cmd.Parameters.Add("@p_stpremio", SqlDbType.Int).Value = p_stpremio;
            cmd.Parameters.Add("@p_cvafecta", SqlDbType.Int).Value = p_cvafecta;
            cmd.Parameters.Add("@p_stpasanom", SqlDbType.Int).Value = p_stpasanom;
            cmd.Parameters.Add("@p_cvtipohr", SqlDbType.Int).Value = p_cvtipohr;
            cmd.Parameters.Add("@p_campo", SqlDbType.VarChar).Value = p_campo;
            cmd.Parameters.Add("@p_formula", SqlDbType.VarChar).Value = p_formula;
            cmd.Parameters.Add("@p_stincnomina", SqlDbType.Int).Value = p_stincnomina;
            cmd.Parameters.Add("@p_usuumod", SqlDbType.VarChar).Value = p_usuumod;
            cmd.Parameters.Add("@p_prgumod", SqlDbType.VarChar).Value = p_prgumod;
            objConexion.asignarConexion(cmd);

            iverifpk = Convert.ToInt32(cmd.ExecuteScalar());
            objConexion.cerrarConexion();
            return iverifpk;

        }
    }
}
