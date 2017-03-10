using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;


using System.Data;
using System.Data.SqlClient;

namespace SIPAA_CS.Recursos_Humanos.App_Code
{
    class FormReg
    {

        //SE DECLARAN VARIABLE
        public int vOpcion;
        public string sResp;

        //CONTRUCTOR
        public FormReg()
        {

            vOpcion = 0;
            sResp = "";

        }

        //DATA TABLE FORMAS DE REGISTRO BUSQUEDA
        public DataTable cObtFormRegBusqueda(int Opcion, string Desc)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_FormRegistro_Grid";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);


            cmd.Parameters.Add("@VLI_OPCION", SqlDbType.Int).Value = Opcion;
            cmd.Parameters.Add("@VLI_DATBUSQ", SqlDbType.VarChar).Value = Desc;

            objConexion.asignarConexion(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            objConexion.cerrarConexion();

            DataTable dtFormReg = new DataTable();
            Adapter.Fill(dtFormReg);
            return dtFormReg;

        }

        public int sGuardaModifReg(int iOpc, int sCve, string sDesc, string sUsu, string sProg)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_FormRegistro_Act";
            cmd.CommandType = CommandType.StoredProcedure;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);


            cmd.Parameters.Add("@VLI_OPCION", SqlDbType.Int).Value = iOpc;
            cmd.Parameters.Add("@VLI_CVFR", SqlDbType.Int).Value = sCve;
            cmd.Parameters.Add("@VLI_DESC", SqlDbType.VarChar).Value = sDesc;
            cmd.Parameters.Add("@VLI_USUUM", SqlDbType.VarChar).Value = sUsu;
            cmd.Parameters.Add("@VLI_PGR", SqlDbType.VarChar).Value = sProg;

            objConexion.asignarConexion(cmd);

            int iResponse = Convert.ToInt32(cmd.ExecuteScalar());


            objConexion.cerrarConexion();

            return iResponse;
        }



    }




}
