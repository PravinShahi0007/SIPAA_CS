﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIPAA_CS.Conexiones;

namespace SIPAA_CS.Recursos_Humanos.App_Code
{
    class Incidencia
    {
        public int CVIncidencia;
        public string Descripcion;
        public int CVRepresenta;
        public string Representa;
        public string UsuuMod;
        public DateTime FhuMod;
        public string PrguMod;



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


    }
}