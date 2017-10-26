using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.Conexiones
{
    public class Conexion
    {
        SqlConnection cn;
        SqlConnection cns;
        SqlCommand cmd;
        //SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public Conexion()
        {
            try
            {
                //cn = new SqlConnection("Data Source=192.168.30.7;Initial Catalog=sipaa;User ID=sipaa;Password=sipaapru");
                cn = new SqlConnection("Data Source=192.168.9.77;Initial Catalog=sipaa;User ID=Desarrollo;Password=Desa17");
                cn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto con la BD SIPAA: " + ex.ToString());
            }
        }

        public SqlConnection conexionSonarh()
        {
            try
            {
                cns = new SqlConnection("Data Source=192.168.9.5;Initial Catalog=SonarhNet;User ID=webdesarrollo;Password=webdesarrollo");
                cns.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto con la BD sonarh: " + ex.ToString());
            }
            return cns;
        }

        //Asigna conexion sonarh
        public void asignarConexions(SqlCommand cmd)
        {

            cmd.Connection = cns;
        }

        //Cierra Conexion
        public void cerrarConexions()
        {

            cns.Close();
        }

        //Asigna conexion
        public void asignarConexion(SqlCommand cmd)
        {

            cmd.Connection = cn;
        }

        //Cierra Conexion
        public void cerrarConexion()
        {

            cn.Close();
        }

        //funcion para crear Acceso Usuario
        public void crearAccesoUsuario(string cvusuario, int idtrab, string nombre, string passw, int stusuario, string usumod, DateTime fhumod, string prgumod)
        {
            int dr;
            try
            {
                cmd = new SqlCommand("insert into ACCECUSUARIO(CVUSUARIO,IDTRAB,NOMBRE,PASSW,STUSUARIO,USUUMOD,FHUMOD,PRGUMOD) values" +
                                    "('" + cvusuario + "','" + idtrab + "', '" + nombre + "','" + passw + "', '" + stusuario + "','" + usumod + "','" + fhumod + "','" + prgumod + "')", cn);
                dr = cmd.ExecuteNonQuery();

                if (dr == 1)
                {
                    MessageBox.Show("Se ha creado correctamente");
                }
                else
                {
                    MessageBox.Show("No se puede crear");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("No pudo crear: " + ex);
            }
        }


        //funcion para crear catalogo de modulo
        public void crearModulo(string cvmodulo, string descripcion, string cvmodpad, int orden, string ambiente, string modulo, string usuumod, DateTime fhumod, string prgumod)
        {
            int dr;
            try
            {
                cmd = new SqlCommand("insert into ACCECMODULO(CVMODULO,DESCRIPCION,CVMODPAD,ORDEN,AMBIENTE,MODULO,USUUMOD,FHUMOD,PRGUMOD) values" +
                                   "('" + cvmodulo + "','" + descripcion + "', '" + cvmodpad + "','" + orden + "', '" + ambiente + "','" + modulo + "','" + usuumod + "','" + fhumod + "','" + prgumod + "')", cn);
                dr = cmd.ExecuteNonQuery();

                if (dr == 1)
                {
                    //MessageBox.Show("Se ha creado correctamente");
                }
                else
                {
                    //MessageBox.Show("No se puede crear");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("No se pudo crear el modulo: " + ex);
            }
        }

        //funcion para mostrar modulos en el data view
        //public void mostrarModulo(DataGridView dgv)
        //{
        //    try
        //    {
        //        da = new SqlDataAdapter("select CVMODULO,DESCRIPCION,CVMODPAD,ORDEN,AMBIENTE,MODULO,USUUMOD,PRGUMOD from ACCECMODULO where STATUS = 1 and CVMODULO = @cvmodulo", cn);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        dgv.DataSource = dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("No se pudo mostrar el modulo: " + ex);
        //    }
        //}

        public DataTable mostrarModulo(string cvmodulo)
        {

            try
            {
                cmd = new SqlCommand("select CVMODULO,DESCRIPCION,CVMODPAD,ORDEN,AMBIENTE,MODULO,USUUMOD,PRGUMOD from ACCECMODULO where CVMODULO = @cvmodulo", cn);
                cmd.Parameters.Add("@cvmodulo", SqlDbType.VarChar).Value = cvmodulo;
                //cmd = new SqlCommand("select * from ACCECMODULO where CVMODULO like '%rh%'", cn);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontro: " + ex);
            }

            return dt;
        }

        //funcion para buscar catalogo
        public DataTable buscarModulo(string cvmodulo)
        {

            try
            {
                cmd = new SqlCommand("select CVMODULO,DESCRIPCION,CVMODPAD,ORDEN,AMBIENTE,MODULO,USUUMOD,PRGUMOD from ACCECMODULO where CVMODULO like '%'+@cvmodulo+'%'", cn);
                cmd.Parameters.Add("@cvmodulo", SqlDbType.VarChar).Value = cvmodulo;
                //cmd = new SqlCommand("select * from ACCECMODULO where CVMODULO like '%rh%'", cn);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontro: " + ex);
            }

            return dt;
        }

        //funcion para actualizar catalogo
        public void actualizarCatalogo(string cvmodulo, string descripcion, string cvmodpad, int orden, string ambiente, string modulo, string usuumod, DateTime fhumod, string prgumod)
        {
            int dr;
            try
            {
                cmd = new SqlCommand("update ACCECMODULO set CVMODULO='" + cvmodulo + "', DESCRIPCION='" + descripcion + "', CVMODPAD='" + cvmodpad + "', ORDEN='" + orden + "',AMBIENTE= '" + ambiente + "', MODULO='" + modulo + "', USUUMOD='" + usuumod + "', FHUMOD='" + fhumod + "', PRGUMOD='" + prgumod + "' WHERE CVMODULO = '" + cvmodulo + "'", cn);
                dr = cmd.ExecuteNonQuery();
                if (dr == 1)
                {
                    MessageBox.Show("Se actualizó modulo");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se actualizo: " + ex);
            }
        }

        //funcion para borrar Catalogo
        public void eliminarCatalogo(string cvmodulo)
        {
            int dr;
            try
            {
                cmd = new SqlCommand("update ACCECMODULO SET STATUS = 1 where CVMODULO = '" + cvmodulo + "'", cn);
                dr = cmd.ExecuteNonQuery();
                if (dr == 1)
                {
                    MessageBox.Show("Se elimino modulo");
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar modulo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se elimino: " + ex);
            }
        }
    }
}

