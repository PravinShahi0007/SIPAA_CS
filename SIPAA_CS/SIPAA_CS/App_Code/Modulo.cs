using SIPAA_CS.Conexiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPAA_CS.Recursos_Humanos.App_Code
{
    class Modulo
    {

        public string CVModulo;
        public string Descripcion;
        public string CVModPadre;
        public int steli;
        public int stcre;
        public int stact;
        public int stimp;
        public int stlec;
        public int Orden;
        public string Ambiente;
        public string strModulo;
        public string UsuuMod;
        public DateTime FhuMod;
        public string PrguMod;
        public int Estatus;
        

        public List<Modulo> ObtenerListModulos(string CVModulo,string Descripcion,string Ambiente,string Modulo,string Estatus)
        {

            List<Modulo> ltModulos = new List<Modulo>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_accemodulo_S";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("CVModulo",SqlDbType.VarChar).Value = CVModulo;
            cmd.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = Descripcion;
            cmd.Parameters.Add("Ambiente", SqlDbType.VarChar).Value = Ambiente;
            cmd.Parameters.Add("Modulo", SqlDbType.VarChar).Value = Modulo;
            cmd.Parameters.Add("Estatus", SqlDbType.VarChar).Value = Estatus;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                Modulo objModulo = new Modulo();
                objModulo.CVModulo = reader.GetString(reader.GetOrdinal("CVMODULO"));
                objModulo.Descripcion = reader.GetString(reader.GetOrdinal("DESCRIPCION"));
                objModulo.CVModPadre = reader.GetString(reader.GetOrdinal("CVMODPAD"));
                objModulo.Orden = reader.GetInt32(reader.GetOrdinal("ORDEN"));
                objModulo.Ambiente = reader.GetString(reader.GetOrdinal("AMBIENTE"));
                objModulo.strModulo = reader.GetString(reader.GetOrdinal("MODULO"));
                objModulo.UsuuMod = reader.GetString(reader.GetOrdinal("USUUMOD"));
                objModulo.FhuMod = reader.GetDateTime(reader.GetOrdinal("FHUMOD"));
                objModulo.PrguMod = reader.GetString(reader.GetOrdinal("PRGUMOD"));
                objModulo.Estatus = reader.GetInt32(reader.GetOrdinal("stModulo"));

                ltModulos.Add(objModulo);
            }

            objConexion.cerrarConexion();

            return ltModulos;

        }


        public Modulo ObtenerPermisosxModulo(string CVModulo,int CVPerfil)
        { 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechpermisos_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cv", SqlDbType.VarChar).Value = CVPerfil.ToString();
            cmd.Parameters.Add("CVModulo", SqlDbType.VarChar).Value = CVModulo;
            cmd.Parameters.Add("Opcion", SqlDbType.VarChar).Value = 2;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();
            Modulo objModulo = new Modulo();

            while (reader.Read())
            {

                
                objModulo.CVModulo = reader.GetString(reader.GetOrdinal("CVMODULO"));
                objModulo.steli = reader.GetInt32(reader.GetOrdinal("steli"));
                objModulo.stact = reader.GetInt32(reader.GetOrdinal("stact"));
                objModulo.stlec = reader.GetInt32(reader.GetOrdinal("stlec"));
                objModulo.stcre = reader.GetInt32(reader.GetOrdinal("stcre"));
                objModulo.stimp = reader.GetInt32(reader.GetOrdinal("stimp"));
            }

            objConexion.cerrarConexion();

            return objModulo;

        }

        public DataTable ObtenerDataTableModulo(List<Modulo> ltModulos)
        {


            DataTable dtModulos = new DataTable();
            dtModulos.Columns.Add("CVModulo");
            dtModulos.Columns.Add("Descripción");
            dtModulos.Columns.Add("Orden");
            dtModulos.Columns.Add("Ambiente");
            dtModulos.Columns.Add("Módulo");
            dtModulos.Columns.Add("Estatus");




            for (int iContador = 0; iContador < ltModulos.Count(); iContador++)
            {
                Modulo objModuloActual = new Modulo();
                objModuloActual = ltModulos.ElementAt(iContador);
                DataRow row = dtModulos.NewRow();
                row["CVModulo"] = objModuloActual.CVModulo.ToString();
                row["Descripción"] = objModuloActual.Descripcion.ToString();
                row["Orden"] = objModuloActual.Orden;
                row["Ambiente"] = objModuloActual.Ambiente.ToString();
                row["Módulo"] = objModuloActual.strModulo.ToString();
                if (objModuloActual.Estatus != 0)
                {
                    row["Estatus"] = "Activo";
                }
                else {
                    row["Estatus"] = "InActivo";
                }

             

                dtModulos.Rows.Add(row);

            }


            return dtModulos;

        }


      

        public List<string> obtenerModulosxPerfil(int CVPerfil)
        {

            List<string> ltModulosxPerfil = new List<string>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"sp_BuscarModuloxPerfil";
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@CVPerfil", SqlDbType.Int).Value = CVPerfil;

            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                Modulo objModulo = new Modulo();
                objModulo.CVModulo = reader.GetString(reader.GetOrdinal("CVMODULO"));
                objModulo.steli = reader.GetInt32(reader.GetOrdinal("steli"));
                objModulo.stact = reader.GetInt32(reader.GetOrdinal("stact"));
                objModulo.stlec = reader.GetInt32(reader.GetOrdinal("stlec"));
                objModulo.stimp = reader.GetInt32(reader.GetOrdinal("stimp"));
                objModulo.stcre = reader.GetInt32(reader.GetOrdinal("stcre"));


                ltModulosxPerfil.Add(objModulo.CVModulo);
            }

            objConexion.cerrarConexion();

            return ltModulosxPerfil;
        }

        public DataTable ObtenerPermisosxUsuario(string CVUsuario)
        {

            DataTable dtPermisos = new DataTable();
            dtPermisos.Columns.Add("CVModulo");
            dtPermisos.Columns.Add("Crear");
            dtPermisos.Columns.Add("Eliminar");
            dtPermisos.Columns.Add("Actualizar");
            dtPermisos.Columns.Add("Imprimir");
            dtPermisos.Columns.Add("Lectura");

            dtPermisos.PrimaryKey = new DataColumn[] { dtPermisos.Columns["CVModulo"] };

            List<Modulo> ltModulos = new List<Modulo>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"usp_rechpermisos_s";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@cv", SqlDbType.VarChar).Value = CVUsuario;
            cmd.Parameters.Add("@cvmodulo", SqlDbType.VarChar).Value = "";
            cmd.Parameters.Add("@Opcion", SqlDbType.VarChar).Value = 1;
            Conexion objConexion = new Conexion();
            objConexion.asignarConexion(cmd);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

               
                string cvModulo = reader.GetString(reader.GetOrdinal("cvmodulo"));
                int stcre = reader.GetInt32(reader.GetOrdinal("stcre"));
                int stact = reader.GetInt32(reader.GetOrdinal("stact"));
                int stlec = reader.GetInt32(reader.GetOrdinal("stlec"));
                int steli = reader.GetInt32(reader.GetOrdinal("steli"));
                int stimp = reader.GetInt32(reader.GetOrdinal("stimp"));
             


                DataRow row = dtPermisos.NewRow();

                if (dtPermisos.Rows.Contains(cvModulo))
                {

                    for (int icontador = 0; icontador < dtPermisos.Rows.Count; icontador++) {

                        DataRow rowSelect = dtPermisos.Rows[icontador];

                        if (rowSelect.Field<string>("CVModulo") == cvModulo) {


                            CambioEstatusPermiso(rowSelect, stact, "Actualizar");
                            CambioEstatusPermiso(rowSelect, stcre, "Crear");
                            CambioEstatusPermiso(rowSelect, stlec, "Lectura");
                            CambioEstatusPermiso(rowSelect, stimp, "Imprimir");
                            CambioEstatusPermiso(rowSelect, steli, "Eliminar");

                            break;
                        }
                    }


                }
                else
                {


                    row["cvModulo"] = cvModulo;
                    row["Lectura"] = stlec;
                    row["Crear"] = stcre;
                    row["Eliminar"] = steli;
                    row["Actualizar"] = stact;
                    row["Imprimir"] = stimp;
                    dtPermisos.Rows.Add(row);

                }
               
            }

            objConexion.cerrarConexion();

            return dtPermisos;

        }


        public void CambioEstatusPermiso(DataRow row,int valorEstatus ,string ColumnaDataTable) {
            if (valorEstatus == 1)
            {
                row[ColumnaDataTable] = 1;
            }
        }
    }
}
