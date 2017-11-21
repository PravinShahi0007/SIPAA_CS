using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SIPAA_CS.Conexiones;
using System.Data;
using System.Data.SqlClient;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación: 15-nov-2017      Última Modificacion: dd-mm-aaaa
//Descripción: sincroniza registros de sica
//***********************************************************************************************

namespace SIPAA_CS.App_Code.RecursosHumanos.Procesos
{
    class SincRegistrosica
    {

        Conexion conex = new Conexion();

        public SincRegistrosica()
        {
            
        }

        //combo tipo de nomina
        public DataTable sincregsica()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select top 10 idtrab, convert(varchar(10), fechareg, 103) as fec, convert(varchar, horareg, 108) as hrregistro, 0 " +
                              "from dbo.rhregistro " +
                              "where fechareg between '01/09/2017' and '15/09/2017'";
            SqlConnection sqlcn = conex.conexsica();
            conex.asignarconexsica(cmd);

            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);

            conex.cerrarconexsica();

            DataTable sicaregsic = new DataTable();
            Adapter.Fill(sicaregsic);
            return sicaregsic;
        }


    }
}
